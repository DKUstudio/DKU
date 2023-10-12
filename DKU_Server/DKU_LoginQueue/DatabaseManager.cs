using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKU_ServerCore;
using MySql.Data.MySqlClient;
using System.Data;

namespace DKU_LoginQueue
{
    public class DatabaseManager
    {
        static string? connString;

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

            using (var conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = MySqlFormat.login_get_salt;
                        cmd.Parameters.AddWithValue("@ID", id);

                        // get db pw
                        string db_salt = "", db_pw = "";
                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                db_salt = rdr.GetString(0);
                                db_pw = rdr.GetString(1);
                            }
                        }
                        string hash_pw = Crypto.SHA256_Generate(db_salt, pw);

                        if (hash_pw != db_pw) // fail
                        {
                            Console.WriteLine($@"[Login] failed by different pw
salt : {db_salt}
hash_pw : {hash_pw}
db_pw : {db_pw}");
                            return null;
                        }
                    }
                    // get user data
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = MySqlFormat.login_get_user_data;
                        cmd.Parameters.AddWithValue("@ID", id);

                        long db_uid = 0;
                        string db_nick = "";
                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                db_uid = rdr.GetInt64(0);
                                db_nick = rdr.GetString(1);
                            }
                        }
                        UserData udata = new UserData();
                        udata.uid = db_uid;
                        udata.nickname = db_nick;
                        return udata;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return null;
                }
            }
        }

        public short Register(string id, string salt, string pw, string nickname)
        {
            using (var conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    using (MySqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.Transaction = tran;

                                cmd.CommandText = MySqlFormat.register_new_user;
                                cmd.Parameters.AddWithValue("@ID", id);
                                cmd.Parameters.AddWithValue("@SALT", salt);
                                cmd.Parameters.AddWithValue("@PW", pw);
                                cmd.Parameters.AddWithValue("@NICKNAME", nickname);
                                cmd.ExecuteNonQuery();

                                cmd.Parameters.Clear();
                                cmd.CommandText = MySqlFormat.register_get_user_count;
                                cmd.Parameters.AddWithValue("@ID", id);
                                using (MySqlDataReader rdr = cmd.ExecuteReader())
                                {
                                    rdr.Read();
                                    int res = rdr.GetInt32(0);
                                    if (res != 1)
                                    {
                                        throw new DuplicateNameException($"[Register] '{id}' already exists.");
                                    }
                                }

                                cmd.Parameters.Clear();
                                cmd.CommandText = MySqlFormat.register_get_nick_count;
                                cmd.Parameters.AddWithValue("@NICKNAME", nickname);
                                using (MySqlDataReader rdr = cmd.ExecuteReader())
                                {
                                    rdr.Read();
                                    int res = rdr.GetInt32(0);
                                    if (res != 1)
                                    {
                                        throw new DuplicateWaitObjectException($"[Register] '{nickname}' already exists.");
                                    }
                                }
                                tran.Commit();
                            }
                        }
                        catch (Exception e)
                        {
                                Console.WriteLine(e.ToString());
                            tran.Rollback();
                            if (e is DuplicateNameException)
                            {

                                return 1;
                            }
                            else
                            {
                                return 2;
                            }
                        }
                    }
                    return 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return 3;
                }
            }
        }

        public void Authentication(long uid, string email)
        {
            using (var conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        int res = 0;
                        cmd.Connection = conn;
                        cmd.CommandText = MySqlFormat.auth_get_count;
                        cmd.Parameters.AddWithValue("@UID", uid);

                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            rdr.Read();
                            res = rdr.GetInt32(0);
                        }

                        cmd.Parameters.Clear();
                        if (res > 0)
                        {
                            // update
                            cmd.CommandText = MySqlFormat.auth_update_email;
                        }
                        else
                        {
                            // insert
                            cmd.CommandText = MySqlFormat.auth_insert_email;
                        }
                        cmd.Parameters.AddWithValue("@UID", uid);
                        cmd.Parameters.AddWithValue("@EMAIL", email);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
