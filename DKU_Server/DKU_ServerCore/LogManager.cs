using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore
{
    public class LogManager
    {
        /*private static LogManager instance;
        public static LogManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new LogManager();
                return instance;
            }
        }*/
    
        public static void Log(string msg)
        {
            DateTime cur = DateTime.UtcNow;
            StringBuilder sb = new StringBuilder();
            sb.Append($"[{TimeZoneInfo.ConvertTimeBySystemTimeZoneId(cur, "Korea Standard Time").ToString()}] ");
            sb.Append(msg);
            Console.WriteLine(sb.ToString());
        }
    }
}
