using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xiaolu.Utility.Helper;

namespace Xiaolu.Webapi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Vercode(string mobile, string vercode)
        {
            var result = SmsHelper.Sendvercode(mobile, vercode);
            return new JsonResult() { ContentType = "text/html", Data = new { IsSuccess = result.IsSuccess, Message = result.Message } };
        }
    }
}
