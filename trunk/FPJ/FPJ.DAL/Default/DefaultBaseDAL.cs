using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPJ.DAL.Default
{
    /// <summary>
    /// 默认的数据库基类（主要为了区分多库）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultBaseDAL<T> : BaseDAL<T> where T : class
    {
        public DefaultBaseDAL()
            : base(ConnectionStringNameConfig.DefaultDBName)
        {
        }
    }
}
