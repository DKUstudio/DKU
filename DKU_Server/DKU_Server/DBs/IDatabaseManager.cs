using DKU_Server.Variants;
using DKU_Server.Worlds.MiniGames.OX_quiz;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.DBs
{
    public interface IDatabaseManager
    {
        public abstract void Init();
        public abstract bool Register(string id, string salt, string pw, string nickname);
        public abstract UserData Login(string id, string pw);

        public abstract void Authentication(long uid, string email);
        public CharaData CharaDataExists(long uid);
        public int GetOXProbsCount();
        public OXProbSheet GetProbAndAns(int idx);
    }
}
