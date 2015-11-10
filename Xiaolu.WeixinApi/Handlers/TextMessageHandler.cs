using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xiaolu.WeixinApi.Common;
using Xiaolu.WeixinApi.Message;

namespace Xiaolu.WeixinApi.Handlers
{
    public class TextMessageHandler
    {
        public string HandleAndGetResponse(BaseMessage message)
        {
            string response = "";
            TextMessage textMsg = message as TextMessage;

            string content = textMsg.Content.Trim();

            if (string.IsNullOrEmpty(content))
            {
                response = "您什么都没输入，没法帮您啊，%>_<%。";
            }
            //else if (SessionUtility.Contains(textMsg.FromUserName))
            //{
            //    response = AuthUtility.GetAuthResult(textMsg);
            //}
            else
            {
                if (content.Contains("图文"))
                {
                    return GetTuwenResponse(textMsg);
                }
                response = HandleOtherString(content);
            }


            TextMessage tm = new TextMessage();
            tm.ToUserName = message.FromUserName;
            tm.FromUserName = message.ToUserName;
            tm.CreateTime = WeiXinHelper.GetNowTime();
            tm.Content = response;

            string returnValue = tm.GetResponseString();
            return returnValue;
        }

        protected string GetTuwenResponse(TextMessage tm)
        {
            TuwenMessage twMsg = new TuwenMessage();
            twMsg.FromUserName = tm.ToUserName;
            twMsg.ToUserName = tm.FromUserName;
            twMsg.CreateTime = WeiXinHelper.GetNowTime();

            TuwenArticleMessage article1 = new TuwenArticleMessage()
            {
                Title = "交大红娘团队",
                Description = "交大红娘团队成员有吴斯一, 陈楠，石皓，刘崇宵",
                PicUrl = "http://img.taopic.com/uploads/allimg/130716/318769-130G60Q62985.jpg",
                Url = "http://img.taopic.com/uploads/allimg/130716/318769-130G60Q62985.jpg"
            };

            TuwenArticleMessage article2 = new TuwenArticleMessage()
            {
                Title = "吴斯一，CEO",
                Description = "吴斯一，CEO",
                PicUrl = "http://e.hiphotos.baidu.com/image/h%3D200/sign=9e12075d6e224f4a4899741339f69044/d1a20cf431adcbef5ae00f7dafaf2edda2cc9ff0.jpg",
                Url = "http://e.hiphotos.baidu.com/image/h%3D200/sign=9e12075d6e224f4a4899741339f69044/d1a20cf431adcbef5ae00f7dafaf2edda2cc9ff0.jpg"
            };

            TuwenArticleMessage article3 = new TuwenArticleMessage()
            {
                Title = "陈楠，CTO",
                Description = "陈楠，CTO",
                PicUrl = "http://g.hiphotos.baidu.com/image/pic/item/9345d688d43f879412576a35d11b0ef41bd53a04.jpg",
                Url = "http://g.hiphotos.baidu.com/image/pic/item/9345d688d43f879412576a35d11b0ef41bd53a04.jpg"
            };

            TuwenArticleMessage article4 = new TuwenArticleMessage()
            {
                Title = "石皓，CFO",
                Description = "石皓，CFO",
                PicUrl = "http://g.hiphotos.baidu.com/image/pic/item/9345d688d43f879412576a35d11b0ef41bd53a04.jpg",
                Url = "http://g.hiphotos.baidu.com/image/pic/item/9345d688d43f879412576a35d11b0ef41bd53a04.jpg"
            };

            TuwenArticleMessage article5 = new TuwenArticleMessage()
            {
                Title = "刘崇宵，COO",
                Description = "刘崇宵，COO",
                PicUrl = "http://g.hiphotos.baidu.com/image/pic/item/9345d688d43f879412576a35d11b0ef41bd53a04.jpg",
                Url = "http://g.hiphotos.baidu.com/image/pic/item/9345d688d43f879412576a35d11b0ef41bd53a04.jpg"
            };

            twMsg.AddArticle(article1);
            twMsg.AddArticle(article2);
            twMsg.AddArticle(article3);
            twMsg.AddArticle(article4);
            twMsg.AddArticle(article5);

            return twMsg.GetResponseString();
        }

        private string HandleOtherString(string requestContent)
        {
            string response = string.Empty;
            if (requestContent.Contains("交大红娘是个啥"))
            {
                response = "交大红娘是靠谱的高学历交友平台";
            }
            else if (requestContent.Contains("哪些活动"))
            {
                response = "8分钟相亲等特色活动";
            }
            else if (requestContent.Contains("你好") || requestContent.Contains("您好"))
            {
                response = "您也好~";
            }
            else if (requestContent.Contains("傻"))
            {
                response = "我不傻！哼~ ";
            }
            else if (requestContent.Contains("逼") || requestContent.Contains("操"))
            {
                response = "哼，你说脏话！ ";
            }
            else if (requestContent.Contains("是谁"))
            {
                response = "我是大哥大，有什么能帮您的吗？~";
            }
            else if (requestContent.Contains("再见"))
            {
                response = "再见！";
            }
            else if (requestContent.Contains("bye"))
            {
                response = "Bye！";
            }
            else if (requestContent.Contains("谢谢"))
            {
                response = "不客气！嘿嘿";
            }
            else if (requestContent == "h" || requestContent == "H" || requestContent.Contains("帮助"))
            {
                response = @"查询天气，输入tq 城市名称\拼音\首字母";
            }
            else
            {
                response = "您说的，可惜，我没明白啊。不过没关系，\n 外事问<a href=\"http://www.google.com\">谷歌</a>，\n内事问<a href=\"http://www.baidu.com\">百度</a>, \nX事问<a href=\"http://www.tianya.cn\">天涯</a>";
            }

            return response;
        }
    }
}
