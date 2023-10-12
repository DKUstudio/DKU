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
        public static string clientSrc = "../DKU_ServerCore/Packets/var/client";
        public static string serverSrc = "../DKU_ServerCore/Packets/var/server";
        public static string queueSrc = "../DKU_ServerCore/Packets/var/queue";
        public static string gServerSrc = "../DKU_ServerCore/Packets/var/gserver";
        public static string qClientSrc = "../DKU_ServerCore/Packets/var/qclient";

        static FileInfo[] c_file_infos;
        static FileInfo[] s_file_infos;
        static FileInfo[] q_file_infos;
        static FileInfo[] gs_file_infos;
        static FileInfo[] qc_file_infos;

        static string[] c_file_names;
        static string[] s_file_names;
        static string[] q_file_names;
        static string[] gs_file_names;
        static string[] qc_file_names;


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

            System.IO.DirectoryInfo di3 = new System.IO.DirectoryInfo(queueSrc);
            q_file_infos = di3.GetFiles();
            q_file_names = new string[q_file_infos.Length];
            for (int i = 0; i < q_file_infos.Length; i++)
            {
                q_file_names[i] = q_file_infos[i].Name.Replace(".cs", "");
            }

            System.IO.DirectoryInfo di4 = new System.IO.DirectoryInfo(gServerSrc);
            gs_file_infos = di4.GetFiles();
            gs_file_names = new string[gs_file_infos.Length];
            for (int i = 0; i < gs_file_infos.Length; i++)
            {
                gs_file_names[i] = gs_file_infos[i].Name.Replace(".cs", "");
            }

            System.IO.DirectoryInfo di5 = new System.IO.DirectoryInfo(qClientSrc);
            qc_file_infos = di5.GetFiles();
            qc_file_names = new string[qc_file_infos.Length];
            for (int i = 0; i < qc_file_infos.Length; i++)
            {
                qc_file_names[i] = qc_file_infos[i].Name.Replace(".cs", "");
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

            string queue_txt = "";
            foreach (string q in q_file_names)
            {
                queue_txt += "\t\t" + q + ",\n";
            }
            foreach (string qc in qc_file_names)
            {
                queue_txt += "\t\t" + qc + ",\n";
            }

            string gs_txt = "";
            foreach (string gs in gs_file_names)
            {
                gs_txt += "\t\t" + gs + ",\n";
            }

            string packet_type = String.Format(PacketFormat.ServerCore_PacketType, client_txt, server_txt, queue_txt, gs_txt);
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
                case_txt += String.Format(PacketFormat.Packet_Handler_Case, str);
                impl_txt += String.Format(PacketFormat.Packet_Handler_Func, str);
            }
            foreach (string str in q_file_names)
            {
                case_txt += String.Format(PacketFormat.Packet_Handler_Case, str);
                impl_txt += String.Format(PacketFormat.Packet_Handler_Func, str);
            }
            string handler_txt = String.Format(PacketFormat.Server_Packet_Handler, case_txt, impl_txt);
            System.IO.File.WriteAllText("./gen/server/GamePacketHandler.cs", handler_txt);


            // implements
            foreach (string str in c_file_names)
            {
                string handle_txt = String.Format(PacketFormat.Server_Packet_Handler_Handle, str);
                System.IO.File.WriteAllText("./gen/server/var/" + str + "_Handler.cs", handle_txt);
            }
            foreach (string str in q_file_names)
            {
                string handle_txt = String.Format(PacketFormat.Server_Packet_Handler_Handle, str);
                System.IO.File.WriteAllText("./gen/server/var/" + str + "_Handler.cs", handle_txt);
            }
        }

        public static void Gen_DummyClient_Packets()
        {
            System.IO.Directory.CreateDirectory("./gen/client");
            System.IO.Directory.CreateDirectory("./gen/client/var");

            string case_txt = "";
            string impl_txt = "";
            foreach (string str in s_file_names)
            {
                case_txt += String.Format(PacketFormat.Packet_Handler_Case, str);
                impl_txt += String.Format(PacketFormat.Packet_Handler_Func, str);
            }
            string handler_txt = String.Format(PacketFormat.DummyClient_Packet_Handler, case_txt, impl_txt);
            System.IO.File.WriteAllText("./gen/client/GamePacketHandler.cs", handler_txt);

            // implements
            foreach (string str in s_file_names)
            {
                string handle_txt = String.Format(PacketFormat.DummyClient_Packet_Handler_Handle, str);
                System.IO.File.WriteAllText("./gen/client/var/" + str + "_Handler.cs", handle_txt);
            }
        }

        public static void Gen_Unity_Packets()
        {
            System.IO.Directory.CreateDirectory("./gen/unity");
            System.IO.Directory.CreateDirectory("./gen/unity/var");

            string case_txt = "";
            string impl_txt = "";
            foreach (string str in s_file_names)
            {
                case_txt += String.Format(PacketFormat.unity_Packet_Handler_Case, str);
                impl_txt += String.Format(PacketFormat.unity_Packet_Handler_Func, str);
            }
            foreach (string str in q_file_names)
            {
                case_txt += String.Format(PacketFormat.unity_Packet_Handler_Case, str);
                impl_txt += String.Format(PacketFormat.unity_Packet_Handler_Func, str);
            }
            string handler_txt = String.Format(PacketFormat.unity_Packet_Handler, case_txt, impl_txt);
            System.IO.File.WriteAllText("./gen/unity/GamePacketHandler.cs", handler_txt);

            // implements
            foreach (string str in s_file_names)
            {
                string handle_txt = String.Format(PacketFormat.unity_Packet_Handler_Handle, str);
                System.IO.File.WriteAllText("./gen/unity/var/" + str + "_Handler.cs", handle_txt);
            }
            foreach (string str in q_file_names)
            {
                string handle_txt = String.Format(PacketFormat.unity_Packet_Handler_Handle, str);
                System.IO.File.WriteAllText("./gen/unity/var/" + str + "_Handler.cs", handle_txt);
            }
        }

        public static void Gen_Queue_Packets()
        {
            System.IO.Directory.CreateDirectory("./gen/q");
            System.IO.Directory.CreateDirectory("./gen/q/var");

            string case_txt = "";
            string impl_txt = "";
            foreach (string str in gs_file_names)
            {
                case_txt += String.Format(PacketFormat.unity_Packet_Handler_Case, str);
                impl_txt += String.Format(PacketFormat.unity_Packet_Handler_Func, str);
            }
            foreach (string str in qc_file_names)
            {
                case_txt += String.Format(PacketFormat.unity_Packet_Handler_Case, str);
                impl_txt += String.Format(PacketFormat.unity_Packet_Handler_Func, str);
            }
            string handler_txt = String.Format(PacketFormat.unity_Packet_Handler, case_txt, impl_txt);
            System.IO.File.WriteAllText("./gen/q/GamePacketHandler.cs", handler_txt);

            // implements
            foreach (string str in gs_file_names)
            {
                string handle_txt = String.Format(PacketFormat.q_Packet_Handler_Handle, str);
                System.IO.File.WriteAllText("./gen/q/var/" + str + "_Handler.cs", handle_txt);
            }
            foreach (string str in qc_file_names)
            {
                string handle_txt = String.Format(PacketFormat.q_Packet_Handler_Handle, str);
                System.IO.File.WriteAllText("./gen/q/var/" + str + "_Handler.cs", handle_txt);
            }
        }

        public static void Copy()
        {
            // packet type force copy
            string str = System.IO.File.ReadAllText("./gen/PacketType.cs");
            System.IO.File.WriteAllText("../DKU_ServerCore/Packets/PacketType.cs", str);

            // packet handler force copy (c/s)
            str = System.IO.File.ReadAllText("./gen/server/GamePacketHandler.cs");
            System.IO.File.WriteAllText("../DKU_Server/Packets/GamePacketHandler.cs", str);

            str = System.IO.File.ReadAllText("./gen/client/GamePacketHandler.cs");
            System.IO.File.WriteAllText("../DKU_DummyClient/Packets/GamePacketHandler.cs", str);

            str = System.IO.File.ReadAllText("./gen/unity/GamePacketHandler.cs");
            System.IO.File.WriteAllText("../../DKU_Client/Assets/Network/GamePacketHandler.cs", str);

            str = System.IO.File.ReadAllText("./gen/q/GamePacketHandler.cs");
            System.IO.File.WriteAllText("../DKU_LoginQueue/GamePacketHandler.cs", str);

            // packet handle copy... no override (c/s)
            FileInfo[] s_infos = new DirectoryInfo("../DKU_Server/Packets/var/").GetFiles();
            FileInfo[] auto_infos = new DirectoryInfo("./gen/server/var/").GetFiles();
            HashSet<string> list = new HashSet<string>();
            foreach (FileInfo info in s_infos)
            {
                list.Add(info.Name);
            }
            foreach (FileInfo info in auto_infos)
            {
                if (list.Contains(info.Name) == false)
                {
                    str = System.IO.File.ReadAllText("./gen/server/var/" + info.Name);
                    System.IO.File.WriteAllText("../DKU_Server/Packets/var/" + info.Name, str);
                }
            }

            FileInfo[] c_infos = new DirectoryInfo("../DKU_DummyClient/Packets/var/").GetFiles();
            auto_infos = new DirectoryInfo("./gen/client/var/").GetFiles();
            list.Clear();
            foreach (FileInfo info in c_infos)
            {
                list.Add(info.Name);
            }
            foreach (FileInfo info in auto_infos)
            {
                if (list.Contains(info.Name) == false)
                {
                    str = System.IO.File.ReadAllText("./gen/client/var/" + info.Name);
                    System.IO.File.WriteAllText("../DKU_DummyClient/Packets/var/" + info.Name, str);
                }
            }


            FileInfo[] u_infos = new DirectoryInfo("../../DKU_Client/Assets/Network/var/").GetFiles();
            auto_infos = new DirectoryInfo("./gen/unity/var/").GetFiles();
            list.Clear();
            foreach (FileInfo info in u_infos)
            {
                list.Add(info.Name);
            }
            foreach (FileInfo info in auto_infos)
            {
                if (list.Contains(info.Name) == false)
                {
                    str = System.IO.File.ReadAllText("./gen/unity/var/" + info.Name);
                    System.IO.File.WriteAllText("../../DKU_Client/Assets/Network/var/" + info.Name, str);
                }
            }

            FileInfo[] q_infos = new DirectoryInfo("../DKU_LoginQueue/var/").GetFiles();
            auto_infos = new DirectoryInfo("./gen/q/var/").GetFiles();
            list.Clear();
            foreach (FileInfo info in q_infos)
            {
                list.Add(info.Name);
            }
            foreach (FileInfo info in auto_infos)
            {
                if (list.Contains(info.Name) == false)
                {
                    str = System.IO.File.ReadAllText("./gen/q/var/" + info.Name);
                    System.IO.File.WriteAllText("../DKU_LoginQueue/var/" + info.Name, str);
                }
            }
        }
    }
}
