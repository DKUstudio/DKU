using DKU_Server.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Worlds.MiniGames
{
    public class MiniGame_NoGame : MiniGame
    {
        public override void CheckStartGame()
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


        public override void StartGame()
        {
            throw new NotImplementedException();
        }
    }
}
