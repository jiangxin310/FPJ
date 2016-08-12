using FPJ.DAL;
using FPJ.DAL.Base;
using FPJ.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using FPJ.BLL.Base;

namespace FPJ.BLL.Default
{
    public class DefaultBaseBLL<T> : BaseBLL<T> where T : class
    {
        public DefaultBaseBLL() : base(ConnectionStringNameConfig.DefaultDBName)
        {
        }
    }
}
