using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xiaolu.Utility.Model.Interface;
using Xiaolu.WeixinApi.Common;

namespace Xiaolu.WeixinApi.Message
{
    public abstract class BaseMessage : IXmlSupport<XElement>
    {
        /// <summary>
        /// 开发者微信号
        /// 是否必须： 是
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 接收方帐号（收到的OpenID）
        /// 是否必须： 是
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 消息类型
        /// 是否必须： 是
        /// </summary>
        public string MsgType { get; protected set; }

        /// <summary>
        /// 消息创建时间 （整型）
        /// 是否必须： 是
        /// </summary>
        public string CreateTime { get; set; }

        public abstract XElement ToXML();

        public abstract void FromXML(XElement xmlObj);

        public string GetResponseString()
        {
            CreateTime = WeiXinHelper.GetNowTime();
            string returnValue = this.ToXML().ToString().Replace("&lt;", "<").Replace("&gt;", ">");
            return returnValue;
        }

        public abstract string HandleAndGetResponse();

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseMessage()
        { }
    }
}
