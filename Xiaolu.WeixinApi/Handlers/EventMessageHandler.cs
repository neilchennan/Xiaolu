using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xiaolu.WeixinApi.Common;
using Xiaolu.WeixinApi.Message;
using Xiaolu.WeixinApi.Utility;

namespace Xiaolu.WeixinApi.Handlers
{
    class LocationSearchResult
    {
        public string resultcode { set; get; }
        public string resultinfo { set; get; }
        public LocationSearchResultRow row { set; get; }
    }

    class LocationSearchResultRow
    {
        public string status { set; get; }
        public LocationSearchResultRowResult result { set; get; }
    }

    class LocationSearchResultRowResult
    {
        public string formatted_address { set; get; }
    }

    public class EventMessageHandler
    {
        //protected IUserRepository<UserDTO> userRepository = RepositoryFactory.GetInstance().GetUserRepository();
        //protected IAuthProfileRepository<AuthProfileDTO> authProfileRepository = RepositoryFactory.GetInstance().GetAuthProfileRepository();

        public string Handle_LocationEventMessage(LocationMessage message)
        {
            //回复欢迎消息
            TextMessage tm = new TextMessage();
            tm.ToUserName = message.FromUserName;
            tm.FromUserName = message.ToUserName;
            tm.CreateTime = WeiXinHelper.GetNowTime();
            //tm.Content = string.Format("您当前的经度：{0}， 纬度{1}",message.Longitude, message.Latitude);

            string url = string.Format("http://lbs.juhe.cn/api/getaddressbylngb?lngx={0}&lngy={1}", message.Longitude, message.Latitude);
            string content = HttpUtility.SendGetHttpRequest(url, "text/json");
            //Log4netHelper.Write("content from web: " + content);

            JsonTextReader tr = new JsonTextReader(new StringReader(content));
            JsonSerializer jSerializer = new JsonSerializer();
            LocationSearchResult rObj = jSerializer.Deserialize<LocationSearchResult>(tr);

            //Log4netHelper.Write("resultinfo: " + rObj.resultinfo);
            //Log4netHelper.Write("formatted_address: " + rObj.row.result.formatted_address);

            //Log4netHelper.Write("Handle_LocationEventMessage content: " + content);
            tm.Content = string.Format("您当前的位置为：{0}", rObj.row.result.formatted_address);
            string returnValue = tm.GetResponseString();
            return returnValue;
        }

        public string Handle_MenuEventMessage(MenuEventMessage message)
        {
            //Log4netHelper.Write("Handle_MenuEventMessage");
            string result = string.Empty;
            if (message != null && message.EventKey != null)
            {
                switch (message.EventKey.ToUpper())
                {
                    //case "BTN_LOGIN":
                    //    result = HandleLoginClickAndGetResponse(message);
                    //    break;
                    //case "BTN_LOGOUT":
                    //    result = HandleLogoutClickAndGetResponse(message);
                    //    break;
                    //case "MY_MENU":
                    //    result = HandleMyMenuClickAndGetResponse(message);
                    //    break;
                    //case "PERSONAL_INFO":
                    //    result = HandlePersonalInfoClickAndGetResponse(message);
                    //    break;
                    case "BTN_OUTLINE":
                        result = HandleOutlineClickAndGetResponse(message);
                        break;
                }
            }

            return result;
        }

        protected string HandleSubscribeAndGetResponse(EventMessage message)
        {
            //回复欢迎消息
            TextMessage tm = new TextMessage();
            tm.ToUserName = message.FromUserName;
            tm.FromUserName = message.ToUserName;
            tm.CreateTime = WeiXinHelper.GetNowTime();
            tm.Content = "欢迎您关注交大红娘-小鹿心动，我是服务小二，有事就请问我，呵呵！\n\n";
            string returnValue = tm.GetResponseString();
            return returnValue;
        }

        //protected string HandleLoginClickAndGetResponse(EventMessage message)
        //{
        //    TextMessage tm = new TextMessage();
        //    tm.ToUserName = message.FromUserName;
        //    tm.FromUserName = message.ToUserName;
        //    tm.CreateTime = WeiXinHelper.GetNowTime();
        //    tm.Content = AuthUtility.GetAuthResult(message);

        //    string returnValue = tm.GetResponseString();

        //    return returnValue;
        //}

        //private string HandleLogoutClickAndGetResponse(EventMessage message)
        //{
        //    TextMessage tm = new TextMessage();
        //    tm.ToUserName = message.FromUserName;
        //    tm.FromUserName = message.ToUserName;
        //    tm.CreateTime = WeiXinHelper.GetNowTime();
        //    tm.Content = AuthUtility.GetLogoutResult(message);

        //    string returnValue = tm.GetResponseString();

        //    return returnValue;
        //}

        private string HandleOutlineClickAndGetResponse(MenuEventMessage message)
        {
            TuwenMessage twMsg = new TuwenMessage();
            twMsg.FromUserName = message.ToUserName;
            twMsg.ToUserName = message.FromUserName;
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

        //private string HandleMyMenuClickAndGetResponse(MenuEventMessage message)
        //{
        //    string corpname = message.ToUserName;
        //    string username = message.FromUserName;
        //    string utcNowStr = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        //    string signature = string.Format("uihwx|{0}|{1}", username, utcNowStr);
        //    Log4netHelper.Write("signature: " + signature);

        //    string signature_encrypted = new ServiceCenterEncryption().Encrypto(signature);
        //    string signature_encrypted_encoded = System.Web.HttpUtility.UrlEncode(signature_encrypted);

        //    TuwenMessage twMsg = new TuwenMessage();
        //    twMsg.FromUserName = corpname;
        //    twMsg.ToUserName = username;
        //    twMsg.CreateTime = WeiXinHelper.GetNowTime();

        //    TuwenArticleMessage article1 = new TuwenArticleMessage()
        //    {
        //        Title = "Service Center",
        //        Description = "Service STI团队成员有cheng.zhang, lu.zhang, xuefeng.yin, zhiixn.xu, nan.chen",
        //        PicUrl = "http://www.united-imaging.com/tl_files/contents/banner/uih_news.jpg",
        //        Url = "http://www.united-imaging.com/"
        //    };

        //    string workOrderUrl = string.Format("{0}/WorkOrder/Index?signature={1}", WeiXinConstants.SiteUrl, signature_encrypted_encoded);
        //    Log4netHelper.Write("url: " + workOrderUrl);
        //    TuwenArticleMessage article2 = new TuwenArticleMessage()
        //    {
        //        Title = "我的工单",
        //        Description = "我需要操作的工单",
        //        PicUrl = string.Format("{0}/images/menu/WorkOrder.png", WeiXinConstants.SiteUrl),
        //        Url = workOrderUrl
        //    };

        //    //TuwenArticleMessage article3 = new TuwenArticleMessage()
        //    //{
        //    //    Title = "我的工时",
        //    //    Description = "我需要操作的工时",
        //    //    PicUrl = string.Format("{0}/images/menu/WorkTime.png", WeiXinConstants.SiteUrl),
        //    //    Url = WeiXinConstants.SiteUrl
        //    //};

        //    string sparePartTypeUrl = string.Format("{0}/sp/MySp?signature={1}", WeiXinConstants.SiteUrl, signature_encrypted_encoded);
        //    TuwenArticleMessage article4 = new TuwenArticleMessage()
        //    {
        //        Title = "我的备件申请",
        //        Description = "我需要操作的备件申请",
        //        PicUrl = string.Format("{0}/images/menu/SparePartType.png", WeiXinConstants.SiteUrl),
        //        Url = sparePartTypeUrl
        //    };

        //    twMsg.AddArticle(article1);
        //    twMsg.AddArticle(article2);
        //    //twMsg.AddArticle(article3);
        //    twMsg.AddArticle(article4);

        //    return twMsg.GetResponseString();
        //}

        //public string HandlePersonalInfoClickAndGetResponse(MenuEventMessage message)
        //{
        //    string corpname = message.ToUserName;
        //    string username = message.FromUserName;
        //    string utcNowStr = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        //    string signature = string.Format("uihwx|{0}|{1}", username, utcNowStr);
        //    Log4netHelper.Write("signature: " + signature);

        //    string signature_encrypted = new ServiceCenterEncryption().Encrypto(signature);
        //    string signature_encrypted_encoded = System.Web.HttpUtility.UrlEncode(signature_encrypted);

        //    string personalInfoUrl = string.Format("{0}/Home/PersonalInfo?signature={1}", WeiXinConstants.SiteUrl, signature_encrypted_encoded);
        //    Log4netHelper.Write("url: " + personalInfoUrl);

        //    var user = userRepository.GetByName(username);
        //    if (user == null)
        //    {
        //        TextMessage tm = new TextMessage();
        //        tm.ToUserName = message.FromUserName;
        //        tm.FromUserName = message.ToUserName;
        //        tm.CreateTime = WeiXinHelper.GetNowTime();
        //        tm.Content = "亲，找不到您在ServiceCenter中的用户哦";

        //        string returnValue = tm.GetResponseString();
        //        return returnValue;
        //    }

        //    TuwenMessage twMsg = new TuwenMessage();
        //    twMsg.FromUserName = corpname;
        //    twMsg.ToUserName = username;
        //    twMsg.CreateTime = WeiXinHelper.GetNowTime();

        //    AuthProfileDTO profile = authProfileRepository.GetById(user.ProfileId);
        //    string profileName = profile == null ? string.Empty : profile.Name;

        //    string picSrc = string.Empty;
        //    if (user.HeadImageContent != null)
        //    {
        //        picSrc = string.Format("{0}/User/HeadImage/{1}?rd={2}", WeiXinConstants.SiteUrl, user.UserId, DateTime.Now.Millisecond);
        //    }
        //    if (string.IsNullOrWhiteSpace(picSrc))
        //    {
        //        picSrc = string.Format("{0}/Images/UnknownPerson.jpg", WeiXinConstants.SiteUrl);
        //    }

        //    TuwenArticleMessage article1 = new TuwenArticleMessage()
        //    {
        //        Title = string.Format("{0} ({1})", user.Description, user.Name),
        //        Description = user.Description,
        //        PicUrl = picSrc,
        //        Url = picSrc
        //    };

        //    TuwenArticleMessage article2 = new TuwenArticleMessage()
        //    {
        //        Title = string.Format("权限策略 : {0}", profileName),
        //        Description = string.Format("权限策略 : {0}", profileName),
        //        PicUrl = string.Format("{0}/images/menu/AuthProfile.png", WeiXinConstants.SiteUrl),
        //        Url = personalInfoUrl
        //    };

        //    twMsg.AddArticle(article1);
        //    twMsg.AddArticle(article2);

        //    return twMsg.GetResponseString();
        //}
    }
}
