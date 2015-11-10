using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xiaolu.WeixinApi.Common;

namespace Xiaolu.WeixinApi.Message
{
    public class TuwenMessage : BaseMessage
    {
        /// <summary>
        /// 图文消息个数，限制为10条以内
        /// 是否必须： 是
        /// </summary>
        public string ArticleCount { set; get; }

        /// <summary>
        /// 多条图文消息信息，默认第一个item为大图,注意，如果图文数超过10，则将会无响应
        /// 是否必须： 是
        /// </summary>
        private List<TuwenArticleMessage> _articleList;
        public List<TuwenArticleMessage> ArticleList
        {
            set
            {
                _articleList = value;
            }
            get
            {
                if (_articleList == null)
                {
                    _articleList = new List<TuwenArticleMessage>();
                }
                return _articleList;
            }
        }
        public void AddArticle(TuwenArticleMessage value)
        {
            ArticleList.Add(value);
            ArticleCount = ArticleList.Count.ToString();
        }

        //<xml>
        //    <ToUserName><![CDATA[toUser]]></ToUserName>
        //    <FromUserName><![CDATA[fromUser]]></FromUserName>
        //    <CreateTime>12345678</CreateTime>
        //    <MsgType><![CDATA[news]]></MsgType>
        //    <ArticleCount>2</ArticleCount>
        //    <Articles>
        //    <item>
        //    <Title><![CDATA[title1]]></Title> 
        //    <Description><![CDATA[description1]]></Description>
        //    <PicUrl><![CDATA[picurl]]></PicUrl>
        //    <Url><![CDATA[url]]></Url>
        //    </item>
        //    <item>
        //    <Title><![CDATA[title]]></Title>
        //    <Description><![CDATA[description]]></Description>
        //    <PicUrl><![CDATA[picurl]]></PicUrl>
        //    <Url><![CDATA[url]]></Url>
        //    </item>
        //    </Articles>
        //</xml> 
        public override XElement ToXML()
        {
            XElement rootElement = new XElement(WeiXinConstants.XML);
            rootElement.Add(new XElement(WeiXinConstants.TO_USERNAME, string.Format(@"<![CDATA[{0}]]>", ToUserName)));
            rootElement.Add(new XElement(WeiXinConstants.FROM_USERNAME, string.Format(@"<![CDATA[{0}]]>", FromUserName)));
            rootElement.Add(new XElement(WeiXinConstants.CREATE_TIME, CreateTime));
            rootElement.Add(new XElement(WeiXinConstants.MSG_TYPE, string.Format(@"<![CDATA[{0}]]>", MsgType)));
            rootElement.Add(new XElement(WeiXinConstants.ARTICAL_COUNT, string.Format(@"<![CDATA[{0}]]>", ArticleCount)));

            XElement articlesElement = new XElement(WeiXinConstants.ARTICALS);
            rootElement.Add(articlesElement);

            ArticleList.ForEach(item => articlesElement.Add(item.ToXML()));

            return rootElement;
        }

        public override void FromXML(XElement xmlObj)
        {
            ToUserName = xmlObj.Element(WeiXinConstants.TO_USERNAME).Value;
            FromUserName = xmlObj.Element(WeiXinConstants.FROM_USERNAME).Value;
            CreateTime = xmlObj.Element(WeiXinConstants.CREATE_TIME).Value;
            MsgType = xmlObj.Element(WeiXinConstants.MSG_TYPE).Value;
            ArticleCount = xmlObj.Element(WeiXinConstants.ARTICAL_COUNT).Value;

            XElement articlesElement = xmlObj.Element(WeiXinConstants.ARTICALS);
            articlesElement.Elements().ToList().ForEach(delegate(XElement ele)
            {
                TuwenArticleMessage articleDto = new TuwenArticleMessage() { TuwenMessage = this };
                articleDto.FromXML(ele);
                AddArticle(articleDto);
            });
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TuwenMessage()
        {
            MsgType = "news";
        }

        public override string HandleAndGetResponse()
        {
            throw new NotImplementedException();
        }
    }
}

