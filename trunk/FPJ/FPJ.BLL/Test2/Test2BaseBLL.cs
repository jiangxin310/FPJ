using FPJ.BLL.Base;
using FPJ.DAL;
using FPJ.DAL.Base;
using FPJ.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FPJ.BLL.Test2
{
    public class Test2BaseBLL<T> : BaseBLL<T> where T : class
    {
        public Test2BaseBLL() : base(ConnectionStringNameConfig.Test2DBName)
        {
        }
    }
}
