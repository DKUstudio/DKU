using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_LoginQueue
{
    public static class MySqlFormat
    {
        #region login
        public static string login_get_salt = "select salt, pw from userdb.login where id = @ID;";
        public static string login_get_user_data = "select uid, nickname from userdb.login where id = @ID;";
        #endregion

        #region register
        public static string register_new_user = "insert into userdb.login(id, salt, pw, nickname) values(@ID, @SALT, @PW, @NICKNAME)";
        public static string register_get_user_count = "select count(*) from userdb.login where id = @ID";
        public static string register_get_nick_count = "select count(*) from userdb.login where nickname = @NICKNAME";
        #endregion

        #region authentication
        public static string auth_get_count = "select count(*) from userdb.auth where uid = @UID;";
        public static string auth_insert_email = "insert into userdb.auth(uid, email) values (@UID, @EMAIL);";
        public static string auth_update_email = "update userdb.auth set email = @EMAIL where uid = @UID;";
        #endregion
    }
}
