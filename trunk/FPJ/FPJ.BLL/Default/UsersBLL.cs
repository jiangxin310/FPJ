using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FPJ.DAL.Default;
using FPJ.Model.Default;
using Redis;

namespace FPJ.BLL.Default
{
    public class UsersBLL : DefaultBaseBLL<User>
    {
        protected readonly UsersDAL _dalUsers = new UsersDAL();

        #region Operator

        public int Insert(User entity)
        {
            return dal.Insert(entity);
        }

        #endregion

        #region List

        public List<User> GetList()
        {
            return dal.GetList().ToList();
        }

        public User GetById(int id)
        {
            return dal.Get(id);
        }

        #endregion


    }
}
