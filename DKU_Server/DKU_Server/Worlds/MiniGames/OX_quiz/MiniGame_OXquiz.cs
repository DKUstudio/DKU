using DKU_Server.Connections;
using DKU_ServerCore.Packets;
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
        public int MAX_USERS = 10;

        public override void AddUid(long userId)
        {
            // TODO 참가 인원이 충분해졌는지 확인, 충분해졌으면 게임 시작
            if(world_block.cur_block_users_uid.Count >= MAX_USERS)
            {
                Task game = new Task(StartGame);
                game.Start();
                //StartGame();
            }
        }

        public override void FinishGame()
        {
            // TODO 게임이 끝남, 최종 스코어보드를 유저에게 보내주고 메인맵으로 돌아가도록 함
        }

        public override void PacketHandle(SPacket packet)
        {
            // TODO OX 퀴즈시 사용할 패킷들 처리
            switch((PacketType)packet.m_type)
            {
                
            }
        }

        public override void RemoveUid(long userId)
        {
            // TODO 해당 게임에서 유저가 나갔음... 따로 처리할게 있나?
        }

        short cur_round = 0;
        public List<List<long>> game_result = new List<List<long>>(5);
        // TODO 라운드별 답지 저장
        public Stack<OXAnswerSheet> oXAnswerSheets = new Stack<OXAnswerSheet>();

        public override void StartGame()
        {
            // TODO, 시작 인원 확인


            // 5라운드 까지
            for(cur_round = 0; cur_round < 5; cur_round++)
            {
                oXAnswerSheets.Clear();

                // TODO 문제 뿌리기
                string prob = "";
                bool ans = true;
                foreach (var item in world_block.cur_block_users_uid)
                {

                }

                // 대기....시간 20초 정도?
                Thread.Sleep(20000);

                // 수신된 유저들의 답을 확인, 맞으면 통과 틀리면 낙오
                while(oXAnswerSheets.Count > 0) 
                {
                    OXAnswerSheet sheet = oXAnswerSheets.Pop();

                    if(sheet.ans_req != ans)
                    {
                        // TODO 플레이어 퇴장
                        game_result[cur_round].Add(sheet.uid);
                    }
                }
            }

            FinishGame();
        }
    }
    public class OXAnswerSheet
    {
        public long uid;
        public bool ans_req;
    }
}
