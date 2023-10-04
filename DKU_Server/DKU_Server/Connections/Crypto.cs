using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace DKU_Server.Connections
{
    public class Crypto
    {
        static SHA256 _sha256 = new SHA256Managed();

        public static string SHA256_Generate(string salt, string str)
        {
            string salted_password = String.Concat(salt, str);

            Byte[] hash = _sha256.ComputeHash(Encoding.UTF8.GetBytes(salted_password));
            return Convert.ToBase64String(hash);
        }
    }
}
