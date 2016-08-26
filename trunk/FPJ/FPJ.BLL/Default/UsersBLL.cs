using System.Collections.Generic;
using System.Linq;
using FPJ.DAL.Default;
using FPJ.Model.Default;
using Redis;
using System;

namespace FPJ.BLL.Default
{
    public class UsersBLL : DefaultBaseBLL<User>
    {
        protected readonly UsersDAL _dalUsers = new UsersDAL();

        public void SetName(string key, string value)
        {
            RedisManager.Set(key, value);
        }

        public string GetName(string key)
        {
            return RedisManager.Get<string>(key);
        }
    }
}
