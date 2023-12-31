﻿using DKU_Server.Variants;
using DKU_Server.Worlds.MiniGames.OX_quiz;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.DBs
{
    public class MemoryDatabase : IDatabaseManager
    {
        static long gen_uid = 0;
        public Dictionary<string, MemoryLoginDataStruct> repository;

        public void Init()
        {
            repository = new Dictionary<string, MemoryLoginDataStruct>();
        }

        public bool Register(string id, string salt, string pw, string nickname)
        {
            if(repository.ContainsKey(id))
                return false;

            MemoryLoginDataStruct memoryLoginDataStruct = new MemoryLoginDataStruct();
            memoryLoginDataStruct.id = id;
            memoryLoginDataStruct.pw = pw;

            UserData data = new UserData();
            data.uid = gen_uid++;
            data.nickname = nickname;
            memoryLoginDataStruct.data = data;

            lock(repository)
            {
                repository.Add(id, memoryLoginDataStruct);
            }

            return true;
        }

        public UserData Login(string id, string pw)
        {
            bool success = repository.TryGetValue(id, out var userData);
            if(success == false)
                return null;

            return userData.data;
        }

        public void Authentication(long uid, string email)
        {
            throw new NotImplementedException();
        }

        public CharaData CharaDataExists(long uid)
        {
            throw new NotImplementedException();
        }

        public int GetOXProbsCount()
        {
            throw new NotImplementedException();
        }

        public OXProbSheet GetProbAndAns(int idx)
        {
            throw new NotImplementedException();
        }

        public void UserCharaShiftChanged(long uid, short v_shift)
        {
            throw new NotImplementedException();
        }
    }
}
