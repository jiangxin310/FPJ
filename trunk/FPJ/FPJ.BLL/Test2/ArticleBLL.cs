using System.Collections.Generic;
using System.Linq;
using FPJ.DAL.Default;
using FPJ.Model.Default;
using FPJ.DAL.Test2;
using FPJ.Model.Test2;

namespace FPJ.BLL.Test2
{
    public class ArticleBLL : Test2BaseBLL<Article>
    {
        protected readonly ArticleDAL _dalUsers = new ArticleDAL();

    }
}
