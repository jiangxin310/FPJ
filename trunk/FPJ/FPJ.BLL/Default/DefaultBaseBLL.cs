using FPJ.DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPJ.BLL.Default
{
    public class DefaultBaseBLL<T> where T : class
    {
        /// <summary>
        /// 基类数据访问DAL
        /// </summary>
        protected readonly BaseDAL<T> dal = new BaseDAL<T>();
    }
}
