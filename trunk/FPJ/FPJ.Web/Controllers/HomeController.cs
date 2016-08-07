using FPJ.BLL.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPJ.AuthCore;

namespace FPJ.Web.Controllers
{
    public class HomeController : BaseController
    {
        protected readonly UsersBLL _bllUsers = new UsersBLL();

        public ActionResult Index()
        {
            var list = _bllUsers.GetList();
            return View(list);
        }

        [CustomerAuthentication]
        public ActionResult Detail(int id)
        {
            var model = _bllUsers.GetById(id);

            return View(model);
        }

    }
}
