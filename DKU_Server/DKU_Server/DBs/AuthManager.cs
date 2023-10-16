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
using DKU_Server.Connections;

namespace DKU_Server.DBs
{
    class AuthData
    {
        public long uid;
        public string code;
        public string email;
        public DateTime dateTime;

        public AuthData(long uid, string code)
        {
            this.uid = uid;
            this.code = code;
        }

        public AuthData(long uid, string code, string email, DateTime dateTime)
        {
            this.uid = uid;
            this.code = code;
            this.email = email;
            this.dateTime = dateTime;
        }
    }
    public class AuthManager
    {
        private static AuthManager instance = new AuthManager();
        public static AuthManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new AuthManager();
                return instance;
            }
        }

        public AuthManager()
        {
            Console.WriteLine("[AuthManager] Start AuthManager");
            Task t1 = new Task(CheckTimeElapsed);
            t1.Start();

            Task t2 = new Task(CheckIncomePackets);
            t2.Start();
        }

        // 시간 제한용 큐
        List<AuthData> pre_q = new List<AuthData>();
        // 수신받은 코드 데이터
        List<AuthData> income_q = new List<AuthData>();

        // 확인용 Dictionary
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
                foreach (AuthData auth in pre_q)
                {
                    if (auth.uid == uid)
                    {
                        lock (pre_q)
                        {
                            pre_q.Remove(auth);
                        }
                        break;
                    }
                }
            }

            AuthData data = new AuthData(uid, new_code, to_email, DateTime.Now);
            pre_q.Add(data);
            waiting_list.Add(uid, data);

            return true;
        }

        public void PushVerifyQueue(long uid, string get_code)
        {
            income_q.Add(new AuthData(uid, get_code));
        }

        void VerifyEmailCode(long uid, string get_code)
        {
            S_TryAuthRes res = new S_TryAuthRes();

            lock (waiting_list)
            {
                if (waiting_list.TryGetValue(uid, out AuthData correct_code))
                {
                    if (correct_code.code == get_code)
                    {
                        // success
                        res.success = 0;
                        NetworkManager.Instance.m_database_manager.Authentication(uid, correct_code.email);
                        waiting_list.Remove(uid);
                        Console.WriteLine("[VerifyEmailCode] success");
                    }
                    else
                    {
                        // incorrect code
                        res.success = 1;
                        Console.WriteLine($"[VerifyEmailCode] incorrect code / server:{correct_code.code} != client:{get_code}");
                    }
                }
                else
                {
                    // time limit
                    res.success = 2;
                    Console.WriteLine("[VerifyEmailCode] time limit");
                }
            }
            byte[] body = res.Serialize();

            Packet packet = new Packet(PacketType.S_TryAuthRes, body, body.Length);
            UserToken token = NetworkManager.Instance.world.FindUserToken(uid);
            if (token != null)
                token.Send(packet);
            Console.WriteLine($"[VerifyEmailCode] dic count = {waiting_list.Count}");
        }


        #region auto

        void CheckTimeElapsed()
        {
            while (true)
            {
                while (pre_q.Count > 0)
                {
                    //Console.WriteLine($"[CheckTimeElapsed] {(int)DateTime.Now.Subtract(pre_q.First().dateTime).TotalSeconds} {pre_q.First().dateTime} {DateTime.Now}");
                    // 3분 경과시 해제
                    if ((int)DateTime.Now.Subtract(pre_q.First().dateTime).TotalSeconds > 180)
                    {
                        lock (waiting_list)  // 확인 리스트 해제
                        {
                            if (waiting_list.ContainsKey(pre_q.First().uid))
                            {
                                Console.WriteLine("[CheckTimeElapsed] erase data dur to time over: " + pre_q.First().uid);
                                waiting_list.Remove(pre_q.First().uid);
                            }
                            else
                            {
                                Console.WriteLine("[CheckTimeElapsed] pass");
                            }
                        }
                        lock (pre_q)
                        {
                            pre_q.RemoveAt(0);
                        }
                        Console.WriteLine($"[CheckTimeElapsed] dic count = {waiting_list.Count}");
                    }
                    else
                    {
                        break;
                    }
                }
                Thread.Sleep(5000);
            }
        }

        void CheckIncomePackets()
        {
            while (true)
            {
                while (income_q.Count > 0)
                {
                    Console.WriteLine($"[CheckIncomePackets] {income_q.First().uid} {income_q.First().code}");
                    VerifyEmailCode(income_q.First().uid, income_q.First().code);
                    lock (income_q)
                    {
                        income_q.RemoveAt(0);
                    }
                }
                Thread.Sleep(5000);
            }
        }
        #endregion






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
$"안녕하세요, " + Environment.NewLine +
$"단국대학교를 즐길수 있는 게임 플랫폼 DKUniverse 입니다!" + Environment.NewLine +
$"이메일 연동 절차를 마치려면 다음 번호를 인게임에 입력해주세요:" + Environment.NewLine + Environment.NewLine +
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
