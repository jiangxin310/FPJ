using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPJ.AuthCore;
using FPJ.Web.ViewModels;

namespace FPJ.Web.Controllers
{
    public class UserController : BaseController
    {

        public ActionResult Login()
        {
            return View();
        }

        //
        // GET: /User/
        [HttpPost]
        public ActionResult Login(string userName, string pwd)
        {
            UserLoginVM um = new UserLoginVM
            {
                UserId = 1,
                UserName = userName
            };
            CustomerFormsAuthentication.SignIn(userName, 7 * 24 * 60, um);

            return Redirect("/");
        }

        public ActionResult LoginOut()
        {
            CustomerFormsAuthentication.SignOut();
            return Redirect("/");
        }

    }
}
