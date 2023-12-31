﻿using DKU_Server.Connections;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DKU_Server.Worlds.MiniGames.OX_quiz
{
    /// <summary>
    /// OX 게임 시뮬레이션 진행 필요!!!
    /// </summary>
    public class MiniGame_OXquiz : MiniGame
    {
        public int MAX_USERS = 2;

        public override void CheckStartGame()
        {
            // 플레이중이면 확인x
            if (isPlaying == true)
                return;

            LogManager.Log($"[OXgame] new user entered!! {world_block.cur_block_users_uid.Count} users in this room..");
            
            if (world_block.cur_block_users_uid.Count > MAX_USERS)
            {
                isPlaying = true;
                Task game = new Task(StartGame);

                // 모든 유저에게 게임 시작 알림
                S_OXGameStartRes res = new S_OXGameStartRes();
                res.success = true;
                byte[] body = res.Serialize();
                Packet pkt = new Packet(PacketType.S_OXGameStartRes, body, body.Length);
                foreach(var usr in world_block.cur_block_users_uid)
                {
                    try
                    {
                        NetworkManager.Instance.world.FindUserToken(usr)?.Send(pkt);
                    }
                    catch (Exception e) 
                    {
                    }
                }

                game.Start();
            }
        }

        int probs_cnt = 0;
        short cur_round = 0;
        public HashSet<long> survived_users;
        public List<List<long>> game_result = new List<List<long>>(5);
        // TODO OX 라운드별 답지 저장
        public Stack<OXAnswerSheet> oXAnswerSheets = new Stack<OXAnswerSheet>();

        public override void StartGame()
        {
            Random random = new Random();
            probs_cnt = NetworkManager.Instance.m_database_manager.GetOXProbsCount();
            survived_users = new HashSet<long>(world_block.cur_block_users_uid);

            // 5라운드까지
            for (cur_round = 0; cur_round < 5; cur_round++)
            {
                // 10초간 대기시간
                Thread.Sleep(10000);

                oXAnswerSheets.Clear();

                // TODO OX 문제 뿌리기
                OXProbSheet probSheet = NetworkManager.Instance.m_database_manager.GetProbAndAns(random.Next(0, probs_cnt) + 1);
                foreach (var item in world_block.cur_block_users_uid)
                {

                }

                // 대기....시간 20초 정도?
                Thread.Sleep(20000);

                // 수신된 유저들의 답을 확인, 맞으면 통과 틀리면 낙오


                // 10초간 대기시간
                Thread.Sleep(10000);

                // 남은 사람 없으면 바로 미니게임 종료
                if(survived_users.Count <= 0)
                {
                    FinishGame();
                    return;
                }
            }

            FinishGame();
        }

        public override void FinishGame()
        {
            // TODO 게임이 끝남, 최종 스코어보드를 유저에게 보내주고 메인맵으로 돌아가도록 함


            isPlaying = false;
        }

        public override void PacketHandle(SPacket packet)
        {
            // TODO OX 퀴즈시 사용할 패킷들 처리
        }

        private void UserGameOver(long userid)
        {

        }

    }

    public struct OXAnswerSheet
    {
        public long uid;
        public bool ans;
    }

    public struct OXProbSheet
    {
        public string prob;
        public bool ans;
    }
}
