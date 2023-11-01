using DKU_Server.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Worlds.MiniGames.Suika_Game
{
    /// <summary>
    /// 솔로게임임, 각 클라이언트에서 게임 진행하고 결과값을 db에 등록해주는 역할만 하면 됨
    /// </summary>
    public class MiniGame_SuikaGame : MiniGame
    {
        public override void AddUid(long userId)
        {
            throw new NotImplementedException();
        }

        public override void FinishGame()
        {
            throw new NotImplementedException();
        }

        public override void PacketHandle(SPacket packet)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUid(long userId)
        {
            throw new NotImplementedException();
        }

        public override void StartGame()
        {
            throw new NotImplementedException();
        }
    }
}
