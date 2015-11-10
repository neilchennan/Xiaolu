using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xiaolu.WeixinApi.Common;
using Xiaolu.WeixinApi.Handlers;

namespace Xiaolu.WeixinApi.Message
{
    public class TextMessage : BaseMessage
    {
        /// <summary>
        /// 回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）
        /// 是否必须： 是
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息ID
        /// 是否必须： 是
        /// </summary>
        public string MsgId { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TextMessage()
        {
            this.MsgType = "text";
        }

        //返回消息时候调用
        //<xml>
        //    <ToUserName><![CDATA[{0}]]></ToUserName>
        //    <FromUserName><![CDATA[{1}]]></FromUserName>
        //    <CreateTime>{2}</CreateTime>
        //    <MsgType><![CDATA[{3}]]></MsgType>
        //    <Content><![CDATA[{4}]]></Content>
        //</xml>";
        public override XElement ToXML()
        {
            XElement rootElement = new XElement(WeiXinConstants.XML);
            rootElement.Add(new XElement(WeiXinConstants.TO_USERNAME, string.Format(@"<![CDATA[{0}]]>", ToUserName)));
            rootElement.Add(new XElement(WeiXinConstants.FROM_USERNAME, string.Format(@"<![CDATA[{0}]]>", FromUserName)));
            rootElement.Add(new XElement(WeiXinConstants.CREATE_TIME, CreateTime));
            rootElement.Add(new XElement(WeiXinConstants.MSG_TYPE, string.Format(@"<![CDATA[{0}]]>", MsgType)));
            rootElement.Add(new XElement(WeiXinConstants.CONTENT, string.Format(@"<![CDATA[{0}]]>", Content)));

            return rootElement;
        }

        //接收消息时调用
        //<xml>
        //    <ToUserName><![CDATA[toUser]]></ToUserName>
        //    <FromUserName><![CDATA[fromUser]]></FromUserName> 
        //    <CreateTime>1348831860</CreateTime>
        //    <MsgType><![CDATA[text]]></MsgType>
        //    <Content><![CDATA[this is a test]]></Content>
        //    <MsgId>1234567890123456</MsgId>
        //</xml>
        public override void FromXML(XElement xmlObj)
        {
            ToUserName = xmlObj.Element(WeiXinConstants.TO_USERNAME).Value;
            FromUserName = xmlObj.Element(WeiXinConstants.FROM_USERNAME).Value;
            CreateTime = xmlObj.Element(WeiXinConstants.CREATE_TIME).Value;
            Content = xmlObj.Element(WeiXinConstants.CONTENT).Value;
            MsgId = xmlObj.Element(WeiXinConstants.MSG_ID).Value;
            MsgType = xmlObj.Element(WeiXinConstants.MSG_TYPE).Value;
        }

        /// <summary>
        /// 从xml数据加载文本消息
        /// </summary>
        /// <param name="xml"></param>
        public static TextMessage LoadFromXml(string xml)
        {
            TextMessage tm = null;
            if (!string.IsNullOrEmpty(xml))
            {
                XElement element = XElement.Parse(xml);
                if (element != null)
                {
                    tm = new TextMessage();
                    tm.FromXML(element);
                }
            }

            return tm;
        }

        public override string HandleAndGetResponse()
        {
            return new TextMessageHandler().HandleAndGetResponse(this);
        }
    }
}
