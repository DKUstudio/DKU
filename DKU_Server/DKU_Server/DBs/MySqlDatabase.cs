using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using DKU_ServerCore;
using DKU_Server.Connections;

namespace DKU_Server.DBs
{
    public class MySqlDatabase : IDatabaseManager
    {
        static string connString;
        public void Init()
        {
            Console.WriteLine(CommonDefine.MYSQL_IPv4_ADDRESS);
#if RELEASE
            string mysql_id = "dkuserver";
#else
            string mysql_id = "dku";
#endif
            string mysql_pw = "z1x2c3v4b5n6m7!!";
            Console.WriteLine($"using mysql id: {mysql_id}");

            connString = $"Server={CommonDefine.MYSQL_IPv4_ADDRESS};Database=userdb;User ID={mysql_id};Password={mysql_pw}";

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                Console.WriteLine("[Database] MySql connected");
            }

        }

        public UserData Login(string id, string pw)
        {
            UserData udata = null;
            using (var conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select lsalt, lpw from userdb.login where lid = '{id}';", conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    string db_pw = "", db_salt = "";
                    while (rdr.Read())
                    {
                        db_salt = rdr.GetString(0);
                        db_pw = rdr.GetString(1);
                    }
                    string hashed_pw = Crypto.SHA256_Generate(db_salt, pw);
                    if (hashed_pw == db_pw)
                    {
                        MySqlCommand cmd1 = new MySqlCommand($"select luid, lnickname from userdb.login where lid = '{id}';", conn);
                        rdr.Close();
                        MySqlDataReader rdr1 = cmd1.ExecuteReader();
                        long db_uid = 0;
                        string db_nick = "";
                        while (rdr1.Read())
                        {
                            db_uid = rdr1.GetInt64(0);
                            db_nick = rdr1.GetString(1);
                        }

                        udata = new UserData();
                        udata.uid = db_uid;
                        udata.nickname = db_nick;

                        rdr1.Close();
                    }
                    else
                    {
                        Console.WriteLine(hashed_pw + " / db: " + db_pw);
                    }
                    rdr.Close();
                    return udata;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return null;
                }
            }
        }

        public bool Register(string id, string salt, string pw, string nickname)
        {
            using (var conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    //Console.WriteLine($"select count(*) from userdb.login where id = '{id}';");
                    MySqlCommand cmd = new MySqlCommand($"select count(*) from userdb.login where lid = '{id}';", conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    int cnt = 0;
                    while (rdr.Read())
                    {
                        cnt = rdr.GetInt32(0);
                    }
                    rdr.Close();
                    if (cnt > 0)
                    {
                        Console.WriteLine($"{id} id exists");
                        return false;
                    }
                    MySqlCommand cmd2 = new MySqlCommand($"insert into userdb.login(lid, lsalt, lpw, lnickname) values('{id}','{salt}','{pw}','{nickname}')", conn);
                    int result = cmd2.ExecuteNonQuery();
                    if (result < 0)
                    {
                        Console.WriteLine("not inserted");
                        return false;
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return false;
                }
            }
        }
    }
}
