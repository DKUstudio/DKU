using DKU_Server.Connections;
using DKU_Server.Worlds.MiniGames.OX_quiz;
using DKU_Server.Worlds.MiniGames.Suika_Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Worlds.MiniGames
{
    public abstract class MiniGame
    {
        public static Dictionary<short, MiniGame> miniGame_dic = new Dictionary<short, MiniGame>() {
            { 0, new MiniGame_NoGame() },
            { 1, new MiniGame_SuikaGame() },
            { 2, new MiniGame_OXquiz() } 
        };

        public static MiniGame Gen_MiniGame(short world_type)
        {
            return miniGame_dic[world_type];
        }

        /// <summary>
        /// 현재 게임 상태를 나타내고 싶음 ex) 중지된 상태인지, 플레이중인 상태인지
        /// </summary>
        public short status;

        /// <summary>
        /// 현재 게임 참가중인 플레이어들
        /// </summary>
        public HashSet<long> ingame_players = new HashSet<long>();

        /// <summary>
        /// 미니게임에서 사용되는 패킷들이 어떻게 동작해야는지 매핑
        /// </summary>
        /// <param name="packet"></param>
        public abstract void PacketHandle(SPacket packet);

        /// <summary>
        /// 미니게임을 시작하자 (시뮬레이션)
        /// </summary>
        public abstract void StartGame();

        /// <summary>
        /// 미니게임 종료...승자발표!...모든유저 로비로
        /// </summary>
        public abstract void FinishGame();

        /// <summary>
        /// 미니게임 참가자 리스트에 추가
        /// </summary>
        /// <param name="userId"></param>
        public abstract void AddUserId(long userId);

        /// <summary>
        /// 미니게임 참가자 리스트에서 제거
        /// </summary>
        /// <param name="userId"></param>
        public abstract void RemoveUserId(long userId);

    }
}
