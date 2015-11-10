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
    public class MenuEventMessage : EventMessage
    {
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }

        //接收消息时调用
        //<xml>
        //    <ToUserName><![CDATA[toUser]]></ToUserName>
        //    <FromUserName><![CDATA[FromUser]]></FromUserName>
        //    <CreateTime>123456789</CreateTime>
        //    <MsgType><![CDATA[event]]></MsgType>
        //    <Event><![CDATA[CLICK]]></Event>
        //    <EventKey><![CDATA[EVENTKEY]]></EventKey>
        //</xml>
        public override void FromXML(XElement xmlObj)
        {
            base.FromXML(xmlObj);
            EventKey = xmlObj.Element(WeiXinConstants.EVENT_KEY).Value;
        }

        public override string HandleAndGetResponse()
        {
            return new EventMessageHandler().Handle_MenuEventMessage(this);
        }
    }
}

