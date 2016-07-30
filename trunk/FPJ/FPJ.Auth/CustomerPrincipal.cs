using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace FPJ.AuthCore
{
    public class CustomerPrincipal<T> : IPrincipal
    {
        private CustomerIdentity<T> _cidentity;
        public CustomerPrincipal(CustomerIdentity<T> cidentity)
        {
            this._cidentity = cidentity;
        }

        public IIdentity Identity
        {
            get
            {
                return _cidentity;
            }
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}
