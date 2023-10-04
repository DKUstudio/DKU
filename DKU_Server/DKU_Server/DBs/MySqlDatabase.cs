using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using DKU_ServerCore;

namespace DKU_Server.DBs
{
    public class MySqlDatabase : IDatabaseManager
    {
        public void Init()
        {
            //string certificationFile = "client-cert.pem";
            //Console.WriteLine(System.IO.File.Exists(certificationFile));
            //string connString = $"Server=34.139.119.85;Database=user_schema;User ID=root;Password=z1x2c3v4b5n6m7!!;SSL Mode=Required;CertificateFile={certificationFile};";
            Console.WriteLine(CommonDefine.MYSQL_IPv4_ADDRESS);
#if RELEASE
            string mysql_id = "dkuserver";
#else
            string mysql_id = "dku";
#endif
            Console.WriteLine($"using mysql id: {mysql_id}");

            string connString = $"Server={CommonDefine.MYSQL_IPv4_ADDRESS};Database=userdb;User ID={mysql_id};Password=z1x2c3v4b5n6m7!!;";

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                Console.WriteLine("[Database] MySql connected");
            }

        }

        public UserData Login(string id, string pw)
        {
            throw new NotImplementedException();
        }

        public bool Register(string id, string pw, string nickname)
        {
            throw new NotImplementedException();
        }
    }
}
