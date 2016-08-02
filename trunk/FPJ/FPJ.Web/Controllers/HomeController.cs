using FPJ.BLL.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPJ.Web.Controllers
{
    public class HomeController : Controller
    {
        protected readonly UsersBLL _bllUsers = new UsersBLL();

        public ActionResult Index()
        {
            var list = _bllUsers.GetList();
            return View(list);
        }

        public ActionResult Detail(int id)
        {
            var model = _bllUsers.GetById(id);

            return View(model);
        }

    }
}
