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
    public class LocationMessage : EventMessage
    {
        /// <summary>
        /// 地理位置纬度
        /// 是否必须： 是
        /// </summary>
        public string Latitude { set; get; }

        /// <summary>
        /// 地理位置经度
        /// 是否必须： 是
        /// </summary>
        public string Longitude { set; get; }

        /// <summary>
        /// 地理位置精度
        /// 是否必须： 是
        /// </summary>
        public string Precision { set; get; }

        //接受时候调用
        //<xml>
        //    <ToUserName><![CDATA[toUser]]></ToUserName>
        //    <FromUserName><![CDATA[fromUser]]></FromUserName>
        //    <CreateTime>123456789</CreateTime>
        //    <MsgType><![CDATA[event]]></MsgType>
        //    <Event><![CDATA[LOCATION]]></Event>
        //    <Latitude>23.137466</Latitude>
        //    <Longitude>113.352425</Longitude>
        //    <Precision>119.385040</Precision>
        //</xml>
        public override void FromXML(XElement xmlObj)
        {
            base.FromXML(xmlObj);
            Latitude = xmlObj.Element(WeiXinConstants.LATITUDE).Value;
            Longitude = xmlObj.Element(WeiXinConstants.LONGTITUDE).Value;
            Precision = xmlObj.Element(WeiXinConstants.PRECISION).Value;
        }

        //不能回复地理位置消息
        public override XElement ToXML()
        {
            throw new NotImplementedException();
        }

        public override string HandleAndGetResponse()
        {
            return new EventMessageHandler().Handle_LocationEventMessage(this);
        }
    }
}

