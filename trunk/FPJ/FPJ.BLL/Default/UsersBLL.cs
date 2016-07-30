using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FPJ.DAL.Default;
using FPJ.Model.Default;
using Redis;

namespace FPJ.BLL.Default
{
    public class UsersBLL
    {
        protected readonly UsersDAL _dalUsers = new UsersDAL();

        public User Get(int id)
        {
            return _dalUsers.Get(id);
        }

        public int Insert(User entity)
        {
            return _dalUsers.Insert(entity);
        }

        public List<User> GetListByUserName(string userName)
        {
            return _dalUsers.GetListByUserName(userName);
        }
    }
}
