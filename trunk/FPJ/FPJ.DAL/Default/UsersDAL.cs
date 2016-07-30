using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FPJ.Model.Default;
using DapperExtensions;


namespace FPJ.DAL.Default
{
    public class UsersDAL : DefaultBaseDAL<User>
    {
        public List<User> GetListByUserName(string userName)
        {
            var pre = Predicates.Field<User>(p => p.UserName, Operator.Eq, userName);
            return GetList(predicate: pre).ToList();
        }
    }
}
