using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DKU_PacketGenerator
{
    public class PacketGenerator
    {
        public static string clientSrc = "C:\\Users\\PSG\\Desktop\\DKU\\DKU_Server\\DKU_ServerCore\\Packets\\var\\client\\";
        public static string serverSrc = "C:\\Users\\PSG\\Desktop\\DKU\\DKU_Server\\DKU_ServerCore\\Packets\\var\\server\\";

        static FileInfo[] c_file_infos;
        static FileInfo[] s_file_infos;

        static string[] c_file_names;
        static string[] s_file_names;


        static void ReadNames()
        {
            System.IO.DirectoryInfo di1 = new System.IO.DirectoryInfo(clientSrc);
            c_file_infos = di1.GetFiles();
            c_file_names = new string[c_file_infos.Length];
            for (int i = 0; i < c_file_infos.Length; i++)
            {
                c_file_names[i] = c_file_infos[i].Name.Replace(".cs", "");
            }

            System.IO.DirectoryInfo di2 = new System.IO.DirectoryInfo(serverSrc);
            s_file_infos = di2.GetFiles();
            s_file_names = new string[s_file_infos.Length];
            for (int i = 0; i < s_file_infos.Length; i++)
            {
                s_file_names[i] = s_file_infos[i].Name.Replace(".cs", "");
            }
        }

        public static void Gen_ServerCore_PacketType()
        {
            ReadNames();
            System.IO.Directory.CreateDirectory("./gen");
            // 덮어쓰기
            string client_txt = "";
            foreach (string s in c_file_names)
            {
                client_txt += "\t\t" + s + ",\n";
            }

            string server_txt = "";
            foreach (string s in s_file_names)
            {
                server_txt += "\t\t" + s + ",\n";
            }

            string packet_type = String.Format(PacketFormat.ServerCore_PacketType, client_txt, server_txt);
            System.IO.File.WriteAllText("./gen/PacketType.cs", packet_type);
        }

        public static void Gen_Server_Packets()
        {
            // game_packet_handler
            System.IO.Directory.CreateDirectory("./gen/server");
            System.IO.Directory.CreateDirectory("./gen/server/var");

            string case_txt = "";
            string impl_txt = "";
            foreach (string str in c_file_names)
            {
                case_txt += String.Format(PacketFormat.Server_Packet_Handler_Case, str);
                impl_txt += String.Format(PacketFormat.Server_Packet_Handler_Func, str);
            }
            string handler_txt = String.Format(PacketFormat.Server_Packet_Handler, case_txt, impl_txt);
            System.IO.File.WriteAllText("./gen/server/GamePacketHandler.cs", handler_txt);


            // implements
            foreach (string str in c_file_names)
            {
                string handle_txt = String.Format(PacketFormat.Server_Packet_Handler_Handler, str);
                System.IO.File.WriteAllText("./gen/server/var/" + str + "_Handler.cs", handle_txt);
            }
        }
    }
}
