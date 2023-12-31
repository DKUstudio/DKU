﻿using DKU_Server.Connections;
using DKU_Server.Worlds.MiniGames.Hexagon;
using DKU_Server.Worlds.MiniGames.OX_quiz;
using DKU_Server.Worlds.MiniGames.Suika_Game;
using DKU_ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Worlds.MiniGames
{
    public abstract class MiniGame
    {
        protected WorldBlock world_block;

        public static Dictionary<short, MiniGame> miniGame_dic = new Dictionary<short, MiniGame>() {
            { (short)CommonDefine.WorldBlockType.Dankook_University, new MiniGame_NoGame() },
            { (short)CommonDefine.WorldBlockType.Suika_Game, new MiniGame_SuikaGame() },
            { (short)CommonDefine.WorldBlockType.OX_quiz, new MiniGame_OXquiz() },
            { (short)CommonDefine.WorldBlockType.Hexagon, new MiniGame_Hexagon() }
        };

        public static MiniGame Gen_MiniGame(WorldBlock v_world_block, short world_type)
        {
            MiniGame ret = miniGame_dic[world_type];
            ret.world_block = v_world_block;

            // 월드 유저 입장 이벤트 등록
            v_world_block.user_entered_event = ret.CheckStartGame;

            return ret;
        }

        /// <summary>
        /// 현재 게임 상태를 나타내고 싶음 ex) 중지된 상태인지, 플레이중인 상태인지
        /// 0: 입장 가능
        /// 1: 플레이 중
        /// </summary>
        public bool isPlaying;

        /// <summary>
        /// 현재 게임 참가중인 플레이어들
        /// </summary>
        public HashSet<long> uids_ingame = new HashSet<long>();

        /// <summary>
        /// 해당 미니게임에 참여할 수 있는 최대 인원 수
        /// </summary>
        public int MAX_USERS = CommonDefine.MAX_CONNECTION;

        /// <summary>
        /// 미니게임에서 사용되는 패킷들이 어떻게 동작해야는지 매핑
        /// </summary>
        /// <param name="packet"></param>
        public abstract void PacketHandle(SPacket packet);

        public abstract void CheckStartGame();

        /// <summary>
        /// 미니게임을 시작하자 (시뮬레이션)
        /// </summary>
        public abstract void StartGame();

        /// <summary>
        /// 미니게임 종료...승자발표!...모든유저 로비로
        /// </summary>
        public abstract void FinishGame();
    }
}
