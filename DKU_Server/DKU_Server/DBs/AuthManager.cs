using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;
using System.Net;
using System.IO;
using System.Threading;
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using DKU_Server.Worlds;
using DKU_Server.Connections.Tokens;

namespace DKU_Server.DBs
{
    class AuthData
    {
        public string code;
        public Task task;
        public string email;
        public AuthData(string code, Task task, string email)
        {
            this.code = code;
            this.task = task;
            this.email = email;
        }
    }
    public class AuthManager
    {
        private static AuthManager instance;
        public static AuthManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new AuthManager();
                return instance;
            }
        }

        Dictionary<long, AuthData> waiting_list = new Dictionary<long, AuthData>();

        public bool StartAuth(long uid, string to_email)
        {
            string new_code = CreateCode();
            if (SendEmail(to_email, new_code) == false)
            {
                return false;
            }

            if (waiting_list.ContainsKey(uid))
            {
                waiting_list.Remove(uid);
            }

            Task t = new Task(() => WhileWaitingEmail(uid, DateTime.Now));
            waiting_list.Add(uid, new AuthData(code, t, to_email));
            t.Start();

            return true;
        }

        public void VerifyEmailCode(long uid, string get_code)
        {
            S_TryAuthRes res = new S_TryAuthRes();
            if (waiting_list.TryGetValue(uid, out AuthData correct_code))
            {
                if (correct_code.code == get_code)
                {
                    // success
                    res.success = 0;
                    NetworkManager.Instance.m_database_manager.Authentication(uid, correct_code.email);
                    waiting_list.Remove(uid);
                }
                else
                {
                    // incorrect code
                    res.success = 1;
                }
            }
            else
            {
                // time limit
                res.success = 2;
            }
            byte[] body = res.Serialize();

            Packet packet = new Packet(PacketType.S_TryAuthRes, body, body.Length);
            UserToken token = TheWorld.Instance.FindUserToken(uid);
            if(token != null)
                token.Send(packet);
        }

        void WhileWaitingEmail(long uid, DateTime stime)
        {
            while (DateTime.Compare(DateTime.Now, stime) < 180)
            {
                Thread.Yield();
            }
            if (waiting_list.ContainsKey(uid))
            {
                waiting_list.Remove(uid);
            }
        }

        string code = "000000";
        string CreateCode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(((code[0] - '0') + 3) % 10);
            sb.Append(((code[1] - '0') + 5) % 10);
            sb.Append(((code[2] - '0') + 7) % 10);
            sb.Append(((code[3] - '0') + 11) % 10);
            sb.Append(((code[4] - '0') + 13) % 10);
            sb.Append(((code[5] - '0') + 17) % 10);
            code = sb.ToString();
            return code;
        }

        const string pw = "xakf xloz hzpm dlit";
        bool SendEmail(string to_email, string pin_num)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress("32171607@dankook.ac.kr",
                    "DKUniverse", System.Text.Encoding.Unicode);
                mailMessage.To.Add(to_email);
                mailMessage.Subject = "DKUniverse 이메일 연동 절차를 완료해주세요.";
                mailMessage.SubjectEncoding = System.Text.Encoding.Unicode;
                mailMessage.Body =
$"안녕하세요! 단국대학교를 즐길수 있는 게임 플랫폼, DKUniverse 입니다!" + Environment.NewLine +
$"이메일 연동 절차를 마치려면 다음 번호를 인게임 팝업에 입력해주세요:" + Environment.NewLine + Environment.NewLine +
$"{pin_num}";
                mailMessage.IsBodyHtml = false;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new System.Net.NetworkCredential("32171607@dankook.ac.kr",
                    pw);

                smtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }
    }
}
