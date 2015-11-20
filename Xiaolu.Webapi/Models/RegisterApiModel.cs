using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xiaolu.Webapi.Models
{
    public class RegisterApiModel
    {
        public string UserId { set; get; }

        public string Mobile { set; get; }

        public string Password { set; get; }
    }
}