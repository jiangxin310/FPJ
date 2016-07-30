using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace FPJ.AuthCore
{
    public class CustomerIdentity<T> : IIdentity
    {
        public string AuthenticationType
        {
            get
            {
                return "Customer";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// 当前用户信息
        /// </summary>
        public T User { get; set; }
    }
}
