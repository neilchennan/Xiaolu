using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Xiaolu.WeixinApi.Service;

namespace Xiaolu.MobileWeb.Controllers
{
    public class WeiXinPublicController : Controller
    {
        //
        // GET: /WeiXinPublic/

        public ActionResult Index()
        {
            HttpContext context = System.Web.HttpContext.Current;
            //由微信服务接收请求，具体处理请求
            IWeiXinService wxService = new WeiXinPublicService(context.Request);
            string responseMsg = wxService.Response();

            ContentResult res = new ContentResult();
            res.Content = responseMsg;
            res.ContentEncoding = Encoding.UTF8;
            res.ContentType = "text/plain";
            return res;
        }

    }
}
