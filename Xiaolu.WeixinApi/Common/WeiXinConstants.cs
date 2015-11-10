using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiaolu.WeixinApi.Common
{
    public class WeiXinConstants
    {
        private static string _defaultSiteUrl = "http://wx.xiaolulove.com";

        public static string SiteUrl
        {
            get
            {
                string siteUrl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"];
                if (string.IsNullOrWhiteSpace(siteUrl))
                    return _defaultSiteUrl;
                return siteUrl;
            }
        }
        /// <summary>
        /// TOKEN
        /// </summary>
        public const string DEFAULT_TOKEN = "xiaoluweixin";

        public const string XML = "xml";
        /// <summary>
        /// 加密签名
        /// </summary>
        public const string SIGNATURE = "signature";
        /// <summary>
        /// 时间戳
        /// </summary>
        public const string TIMESTAMP = "timestamp";
        /// <summary>
        /// 随机数
        /// </summary>
        public const string NONCE = "nonce";
        /// <summary>
        /// 随机字符串
        /// </summary>
        public const string ECHOSTR = "echostr";

        /// <summary>
        /// 发送人
        /// </summary>
        public const string FROM_USERNAME = "FromUserName";
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public const string TO_USERNAME = "ToUserName";
        /// <summary>
        /// 消息内容
        /// </summary>
        public const string CONTENT = "Content";
        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public const string CREATE_TIME = "CreateTime";
        /// <summary>
        /// 消息类型
        /// </summary>
        public const string MSG_TYPE = "MsgType";

        public const string MSG_TYPE_LOWER = "msgtype";
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public const string MSG_ID = "MsgId";

        public const string EVENT = "Event";

        public const string EVENT_KEY = "EventKey";

        public const string ARTICAL_COUNT = "ArticleCount";

        public const string ARTICALS = "Articles";

        public const string ITEM = "item";

        public const string TITLE = "Title";

        public const string DESCRIPTION = "Description";

        public const string PIC_URL = "PicUrl";

        public const string URL = "Url";

        public const string VOICE = "Voice";

        public const string MEDIA_ID = "MediaId";

        public const string IMAGE = "Image";

        public const string VIDEO = "Video";

        public const string MUSIC = "Music";

        public const string MUSICURL = "MusicUrl";

        public const string HQMUSICURL = "HQMusicUrl";

        public const string THUMBMEDIAID = "ThumbMediaId";

        public const string MSG_SIGNATURE = "msg_signature";

        public const string ENCRYPT = "Encrypt";

        public const string AGENT_ID = "AgentID";

        public const string AGENT_ID_LOWER = "agentid";

        public const string MSG_SIGNATURE_BACK = "MsgSignature";

        public const string FORMAT = "Format";

        public const string LATITUDE = "Latitude";

        public const string LONGTITUDE = "Longitude";

        public const string PRECISION = "Precision";

        public const string TO_USER = "touser";

        public const string TO_PARTY = "toparty";

        public const string TO_TAG = "totag";

        public const string SAFE = "safe";

        public const string TEXT = "text";
    }
}
