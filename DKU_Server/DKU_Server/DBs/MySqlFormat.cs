using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.DBs
{
    public class MySqlFormat
    {
        public static string login_get_salt = "select salt, pw from userdb.login where id = @ID;";

        public static string login_get_user_data = "select uid, nickname from userdb.login where id = @ID;";

        public static string register_new_user = "insert into userdb.login(id, salt, pw, nickname) values(@ID, @SALT, @PW, @NICKNAME)";

        public static string register_get_user_count = "select count(*) from userdb.login where id = @ID";
    }
}
