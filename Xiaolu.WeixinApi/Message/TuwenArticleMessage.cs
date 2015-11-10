using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xiaolu.WeixinApi.Common;

namespace Xiaolu.WeixinApi.Message
{
    public class TuwenArticleMessage : BaseMessage
    {
        public TuwenMessage TuwenMessage { set; get; }

        /// <summary>
        /// 图文消息标题
        /// 是否必须： 否
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// 图文消息描述
        /// 是否必须： 否
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
        /// 是否必须： 否
        /// </summary>
        public string PicUrl { set; get; }

        /// <summary>
        /// 点击图文消息跳转链接
        /// 是否必须： 否
        /// </summary>
        public string Url { set; get; }

        public override XElement ToXML()
        {
            XElement rootElement = new XElement(WeiXinConstants.ITEM);
            rootElement.Add(new XElement(WeiXinConstants.TITLE, string.Format(@"<![CDATA[{0}]]>", Title)));
            rootElement.Add(new XElement(WeiXinConstants.DESCRIPTION, string.Format(@"<![CDATA[{0}]]>", Description)));
            rootElement.Add(new XElement(WeiXinConstants.PIC_URL, string.Format(@"<![CDATA[{0}]]>", PicUrl)));
            rootElement.Add(new XElement(WeiXinConstants.URL, string.Format(@"<![CDATA[{0}]]>", Url)));

            return rootElement;
        }

        public override void FromXML(XElement xmlObj)
        {
            Title = xmlObj.Element(WeiXinConstants.TITLE).Value;
            Description = xmlObj.Element(WeiXinConstants.DESCRIPTION).Value;
            PicUrl = xmlObj.Element(WeiXinConstants.PIC_URL).Value;
            Url = xmlObj.Element(WeiXinConstants.URL).Value;
        }

        public override string HandleAndGetResponse()
        {
            throw new NotImplementedException();
        }
    }
}

