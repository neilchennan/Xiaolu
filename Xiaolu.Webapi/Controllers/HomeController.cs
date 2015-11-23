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

        public ActionResult Genvercode(string mobile)
        {
            var result = SmsHelper.GenVercode(mobile);
            return new JsonResult() { ContentType = "text/html", Data = new { IsSuccess = result.IsSuccess, Message = result.Message } };
        }

        public ActionResult Checkvercode(string mobile, string vercode)
        {
            var result = SmsHelper.CheckVercode(mobile, vercode);
            return new JsonResult() { ContentType = "text/html", Data = new { IsSuccess = result.IsSuccess, Message = result.Message } };
        }

        public ActionResult Vercode(string mobile, string vercode)
        {
            var result = SmsHelper.SendMsg(mobile, vercode);
            return new JsonResult() { ContentType = "text/html", Data = new { IsSuccess = result.IsSuccess, Message = result.Message } };
        }
    }
}
