using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xiaolu.WeixinApi.Common;

namespace Xiaolu.WeixinApi.Message
{
    public class MessageFactory
    {
        private MessageFactory() { }

        public static BaseMessage GetCorpMessageByXml(string xmlStr)
        {
            if (string.IsNullOrEmpty(xmlStr))
            {
                //Log4netHelper.Write("xmlStr is null, return null");
                return null;
            }

            XDocument doc = XDocument.Parse(xmlStr);

            XElement rootElement = doc.Root;

            XElement msgTypeElement = rootElement.Element(WeiXinConstants.MSG_TYPE);

            if (msgTypeElement == null)
            {
                //Log4netHelper.Write("msgTypeElement is null, return null");
                return null;
            }

            string msgType = msgTypeElement.Value;
            //Log4netHelper.Write("msgType :" + msgType);

            BaseMessage returnValue;
            switch (msgType)
            {
                case "text":
                    returnValue = new TextMessage();
                    break;
                //case "event":
                //    returnValue = EventMessage.LoadFromXml(rootElement);
                //    break;
                //case "image":
                //    returnValue = new CorpImageMessage();
                //    break;
                //case "voice":
                //    returnValue = new CorpVoiceMessage();
                //    break;
                //case "video":
                //    returnValue = new CorpVideoMessage();
                //    break;
                //case "music":
                //    returnValue = new MusicMessage();
                //    break;
                case "news":
                    returnValue = new TuwenMessage();
                    break;
                default:
                    returnValue = null;
                    break;
            }

            if (returnValue != null)
                returnValue.FromXML(rootElement);

            return returnValue;
        }

        public static BaseMessage GetMessageByXml(string xmlStr)
        {
            if (string.IsNullOrEmpty(xmlStr))
            {
                //Log4netHelper.Write("xmlStr is null, return null");
                return null;
            }

            XDocument doc = XDocument.Parse(xmlStr);

            XElement rootElement = doc.Root;

            XElement msgTypeElement = rootElement.Element(WeiXinConstants.MSG_TYPE);

            if (msgTypeElement == null)
            {
                //Log4netHelper.Write("msgTypeElement is null, return null");
                return null;
            }

            string msgType = msgTypeElement.Value;
            //Log4netHelper.Write("msgType :" + msgType);

            BaseMessage returnValue;
            switch (msgType)
            {
                case "text":
                    returnValue = new TextMessage();
                    break;
                case "event":
                    returnValue = new MenuEventMessage();
                    break;
                //case "image":
                //    returnValue = new ImageMessage();
                //    break;
                //case "voice":
                //    returnValue = new VoiceMessage();
                //    break;
                //case "video":
                //    returnValue = new VideoMessage();
                //    break;
                //case "music":
                //    returnValue = new MusicMessage();
                //    break;
                case "news":
                    returnValue = new TuwenMessage();
                    break;
                default:
                    returnValue = null;
                    break;
            }

            if (returnValue != null)
                returnValue.FromXML(rootElement);

            return returnValue;
        }
    }
}