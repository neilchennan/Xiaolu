using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xiaolu.WeixinApi.Common;

namespace Xiaolu.WeixinApi.Message
{
    public abstract class EventMessage : BaseMessage
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public EventMessage()
        {
            this.MsgType = "event";
        }

        //事件不能回复只能接受
        public override XElement ToXML()
        {
            throw new NotImplementedException();
        }


        //接收消息时调用
        //<xml>
        //    <ToUserName><![CDATA[toUser]]></ToUserName>
        //    <FromUserName><![CDATA[FromUser]]></FromUserName>
        //    <CreateTime>123456789</CreateTime>
        //    <MsgType><![CDATA[event]]></MsgType>
        //    <Event><![CDATA[subscribe]]></Event>
        //</xml>
        public override void FromXML(XElement xmlObj)
        {
            ToUserName = xmlObj.Element(WeiXinConstants.TO_USERNAME).Value;
            FromUserName = xmlObj.Element(WeiXinConstants.FROM_USERNAME).Value;
            CreateTime = xmlObj.Element(WeiXinConstants.CREATE_TIME).Value;
            MsgType = xmlObj.Element(WeiXinConstants.MSG_TYPE).Value;
            Event = xmlObj.Element(WeiXinConstants.EVENT).Value;
        }

        /// <summary>
        /// 从xml数据加载文本消息
        /// </summary>
        /// <param name="xml"></param>
        public static EventMessage LoadFromXml(XElement rootElement)
        {
            EventMessage em = null;

            string eventValue = rootElement.Element(WeiXinConstants.EVENT).Value.ToLower();

            switch (eventValue)
            {
                case "click":
                case "view":
                    em = new MenuEventMessage();
                    break;
                case "location":
                    em = new LocationMessage();
                    break;
                default:
                    em = new MenuEventMessage();
                    break;
            }

            em.FromXML(rootElement);

            return em;
        }
    }
}
