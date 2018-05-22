using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace venteTest.Controllers
{
    public class MemberController : Controller
    {
        // Montrer profil public du membre si il existe avec sa cote, ses commentaires, etc.
        [Route("Member/Index/{email}")]
        public IActionResult Index(string email) 
        {
            return View();
        }
        //public IActionResult PartialListeUsers() {

        //    IList<Article> lArt = _articleManager.lstArticles;
        //    IList<ArticleViewModel> model = Mapper.Map<IList<Article>, IList<ArticleViewModel>>(lArt);
        //    return PartialView(model);
        //}
    }
}