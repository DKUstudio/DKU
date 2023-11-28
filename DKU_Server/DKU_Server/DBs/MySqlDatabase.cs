using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using DKU_ServerCore;
using DKU_Server.Connections;
using DKU_Server.Variants;
using System.Security.Cryptography;
using DKU_Server.Worlds.MiniGames.OX_quiz;

namespace DKU_Server.DBs
{
    public class MySqlDatabase : IDatabaseManager
    {
        static string connString;



        public void Init()
        {
            LogManager.Log(CommonDefine.MYSQL_IPv4_ADDRESS);
#if RELEASE
            string mysql_id = "dkuserver";
#else
            string mysql_id = "dku";
#endif
            string mysql_pw = "z1x2c3v4b5n6m7!!";
            LogManager.Log($"using mysql id: {mysql_id}");

            connString = $"Server={CommonDefine.MYSQL_IPv4_ADDRESS};Database=userdb;User ID={mysql_id};Password={mysql_pw}";

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                LogManager.Log("[Database] MySql connected");
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
                            LogManager.Log($@"[Login] failed by different pw
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
                    LogManager.Log(e.ToString());
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
                                        throw new Exception($"[Register] '{id}' already exists.");
                                    }
                                }
                                tran.Commit();
                            }
                        }
                        catch (Exception e)
                        {
                            LogManager.Log(e.ToString());
                            tran.Rollback();
                            return false;
                        }
                    }
                    return true;
                }
                catch (Exception e)
                {
                    LogManager.Log(e.ToString());
                    return false;
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
                    LogManager.Log(e.ToString());
                }
            }
        }

        public CharaData CharaDataExists(long uid)
        {
            CharaData ret = null;
            using (var conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        int res = 0;
                        cmd.Connection = conn;
                        cmd.CommandText = MySqlFormat.chara_exists;
                        cmd.Parameters.AddWithValue("@UID", uid);

                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            rdr.Read();
                            res = rdr.GetInt32(0);
                        }
                        cmd.Parameters.Clear();

                        // 캐릭터 정보 없음, 기본 정보 생성
                        if (res == 0)
                        {
                            // 기본 데이터를 테이블에 생성
                            try
                            {
                                cmd.CommandText = MySqlFormat.chara_setData;
                                cmd.Parameters.AddWithValue("@UID", uid);
                                cmd.Parameters.AddWithValue("@BITMASK", 262143);
                                cmd.Parameters.AddWithValue("@LASTLOGINSHIFT", 0);
                                cmd.ExecuteNonQuery();
                                ret = new CharaData();
                                ret.uid = uid;
                                ret.bitmask = 1;
                                ret.lastloginshift = 0;
                            }
                            catch (Exception ex)
                            {
                                LogManager.Log(ex.ToString());
                            }
                        }
                        // 캐릭터 정보 있음, 가져올것
                        else
                        {
                            // 기존 데이터를 가져옴
                            try
                            {
                                cmd.CommandText = MySqlFormat.chara_getData;
                                cmd.Parameters.AddWithValue("@UID", uid);
                                long sql_uid = 0;
                                int sql_bitmask = 0;
                                short sql_lastloginshift = 0;
                                using (MySqlDataReader rdr = cmd.ExecuteReader())
                                {
                                    rdr.Read();
                                    sql_uid = rdr.GetInt64(0);
                                    sql_bitmask = rdr.GetInt32(1);
                                    sql_lastloginshift = rdr.GetInt16(2);
                                }
                                ret = new CharaData();
                                ret.uid = sql_uid;
                                ret.bitmask = sql_bitmask;
                                ret.lastloginshift = sql_lastloginshift;
                            }
                            catch (Exception ex)
                            {
                                LogManager.Log(ex.ToString());
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    LogManager.Log($"[CharaData] get chara data \"{uid}\" failed");
                }
            }
            // null이 아닌게 정상
            return ret;
        }

        public void UserCharaShiftChanged(long v_uid, short v_shift)
        {
            using (var conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = MySqlFormat.chara_shiftChange;
                        cmd.Parameters.AddWithValue("@UID", v_uid);
                        cmd.Parameters.AddWithValue("@LASTLOGINSHIFT", v_shift);
                        cmd.ExecuteNonQuery();
                        LogManager.Log($"[Chara Changed] {v_uid} user {v_shift} shift.");
                    }
                }
                catch (Exception ex)
                {
                    LogManager.Log($"[Chara Change] Exception, {v_uid} user {v_shift} shift.");
                }
            }
        }

        public int GetOXProbsCount()
        {
            int res = 0;
            using (var conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = MySqlFormat.ox_check_prob_cnt;

                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            rdr.Read();
                            res = rdr.GetInt32(0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogManager.Log(ex.ToString());
                }
            }
            return res;
        }

        public OXProbSheet GetProbAndAns(int idx)
        {
            OXProbSheet res = new OXProbSheet();
            using (var conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = MySqlFormat.ox_check_prob_cnt;
                        cmd.Parameters.AddWithValue("@PID", idx);
                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            rdr.Read();
                            res.prob = rdr.GetString(0);
                            res.ans = rdr.GetBoolean(1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogManager.Log(ex.ToString());
                }
            }
            return res;
        }
    }
}
