using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xiaolu.Webapi.Models
{
    public class LoginApiModel
    {
        public string UserIdOrMobile { set; get; }

        public string Password { set; get; }

        public string WeixinId { set; get; }
    }
}