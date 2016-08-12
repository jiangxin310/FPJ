using System.Collections.Generic;
using System.Linq;
using FPJ.DAL.Default;
using FPJ.Model.Default;

namespace FPJ.BLL.Default
{
    public class UsersBLL : DefaultBaseBLL<User>
    {
        protected readonly UsersDAL _dalUsers = new UsersDAL();

    }
}
