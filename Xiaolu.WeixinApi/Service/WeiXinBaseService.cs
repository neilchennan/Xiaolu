using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xiaolu.Utility.Common;
using Xiaolu.WeixinApi.Message;

namespace Xiaolu.WeixinApi.Service
{
    public abstract class WeiXinBaseService : IWeiXinService
    {
        protected HttpRequest _request;
        public HttpRequest Request
        {
            get
            {
                return _request;
            }
        }

        public string Signature { set; get; }
        public string Timestamp { set; get; }
        public string Nonce { set; get; }
        protected string EchoStr { set; get; }

        protected abstract void GetConstParameters();

        protected abstract void GetParamsFromRequest();

        protected abstract string ResponseMsg();

        protected abstract bool CheckSignature();

        public WeiXinBaseService(HttpRequest request)
        {
            _request = request;
            GetConstParameters();
            GetParamsFromRequest();
        }

        /// <summary>
        /// 处理请求，产生响应
        /// </summary>
        /// <returns></returns>
        public string Response()
        {
            string method = Request.HttpMethod.ToUpper();
            //验证签名
            if (method == "GET")
            {
                if (CheckSignature())
                {
                    return EchoStr;
                }
                else
                {
                    return XiaoluResources.ERR_MSG_CHECK_SIGNATURE_FAIL;
                }
            }

            //处理消息
            if (method == "POST")
            {
                return ResponseMsg();
            }
            else
            {
                string errMsg = string.Format(XiaoluResources.ERR_MSG_HTTP_METHOD_NOT_SUPPORT, method);
                //Log4netHelper.WriteError(errMsg);
                return errMsg;
            }
        }

        public abstract void DoPushMessage(PushMessage message);
    }
}

