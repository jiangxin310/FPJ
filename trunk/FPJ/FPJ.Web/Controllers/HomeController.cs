using FPJ.BLL.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPJ.AuthCore;
using FPJ.BLL.Test2;

namespace FPJ.Web.Controllers
{
    public class HomeController : BaseController
    {
        protected readonly UsersBLL _bllUsers = new UsersBLL();
        protected readonly ArticleBLL _bllArticle = new ArticleBLL();

        public ActionResult Index()
        {
            var list = _bllUsers.GetList();
            var articleList = _bllArticle.GetList();
            ViewBag.articleList = articleList;

            return View(list);
        }

        [CustomerAuthentication]
        public ActionResult Detail(int id)
        {
            var model = _bllUsers.Get(id);

            return View(model);
        }

    }
}
