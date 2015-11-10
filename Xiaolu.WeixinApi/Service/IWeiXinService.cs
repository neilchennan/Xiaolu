using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xiaolu.WeixinApi.Message;

namespace Xiaolu.WeixinApi.Service
{
    public interface IWeiXinService
    {
        HttpRequest Request { get; }

        string Response();

        void DoPushMessage(PushMessage message);
    }
}

