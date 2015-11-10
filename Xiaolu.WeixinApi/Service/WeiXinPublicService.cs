using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xiaolu.Utility.Helper;
using Xiaolu.WeixinApi.Common;
using Xiaolu.WeixinApi.Message;
using Xiaolu.WeixinApi.Utility;

[assembly: log4net.Config.XmlConfigurator(Watch=true)]
namespace Xiaolu.WeixinApi.Service
{
    public sealed class WeiXinPublicService : WeiXinBaseService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WeiXinPublicService));
        public WeiXinPublicService(HttpRequest request)
            : base(request)
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public string PublicToken { set; get; }
        public string AppId { set; get; }
        public string AppSecret { set; get; }

        protected override void GetConstParameters()
        {
            string token = ConfigurationManager.AppSettings["PublicToken"];//从配置文件获取Token
            if (string.IsNullOrEmpty(token))
            {
                //Log4netHelper.WriteError(string.Format("PublicToken 配置项没有配置！"));
                log.Error("PublicToken 配置项没有配置！");
                PublicToken = string.Empty;
            }
            PublicToken = token;

            string appId = ConfigurationManager.AppSettings["AppId"];//从配置文件获取AppId
            if (string.IsNullOrEmpty(appId))
            {
                //Log4netHelper.WriteError(string.Format("AppId 配置项没有配置！"));
                log.Error("AppId 配置项没有配置！");
                AppId = string.Empty;
            }
            AppId = appId;

            string appSecret = ConfigurationManager.AppSettings["AppSecret"];//从配置文件获取AppSecret
            if (string.IsNullOrEmpty(appSecret))
            {
                //Log4netHelper.WriteError(string.Format("AppSecret 配置项没有配置！"));
                log.Error("AppSecret 配置项没有配置！");
                AppSecret = string.Empty;
            }
            AppSecret = appSecret;
        }

        protected override void GetParamsFromRequest()
        {
            string signature = Request.QueryString[WeiXinConstants.SIGNATURE];
            string timestamp = Request.QueryString[WeiXinConstants.TIMESTAMP];
            string nonce = Request.QueryString[WeiXinConstants.NONCE];
            string echoStr = Request.QueryString[WeiXinConstants.ECHOSTR];

            //Log4netHelper.Write("signature : " + signature);
            //Log4netHelper.Write("timestamp : " + timestamp);
            //Log4netHelper.Write("nonce : " + nonce);

            Signature = string.IsNullOrEmpty(signature) ? string.Empty : signature;
            Timestamp = string.IsNullOrEmpty(timestamp) ? string.Empty : timestamp;
            Nonce = string.IsNullOrEmpty(nonce) ? string.Empty : nonce;
            EchoStr = string.IsNullOrEmpty(echoStr) ? string.Empty : echoStr;

            log.Info("Signature : " + Signature);
            log.Info("Timestamp : " + Timestamp);
            log.Info("Nonce : " + Nonce);
            log.Info("Nonce : " + EchoStr);
        }

        protected override bool CheckSignature()
        {
            List<string> list = new List<string>();
            list.Add(PublicToken);
            list.Add(Timestamp);
            list.Add(Nonce);
            //排序
            list.Sort();
            //拼串
            string input = string.Empty;
            foreach (var item in list)
            {
                input += item;
            }
            //加密
            string new_signature = SecurityUtility.SHA1Encrypt(input);
            log.Info("new_signature : " + new_signature);
            //验证
            if (new_signature == Signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override string ResponseMsg()
        {
            try
            {
                string requestXml = WeiXinHelper.ReadRequest(this.Request);
                BaseMessage msg = MessageFactory.GetMessageByXml(requestXml);

                string handledMsgStr = msg.HandleAndGetResponse();
                return handledMsgStr;
            }
            catch (Exception ex)
            {
                string errMsg = ExceptionHelper.GetInnerExceptionInfo(ex);
                //Log4netHelper.Write(errMsg);
                return errMsg;
            }
        }

        public override void DoPushMessage(PushMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
