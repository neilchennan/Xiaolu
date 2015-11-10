using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiaolu.WeixinApi.Message
{
    public abstract class PushMessage
    {
        /// <summary>
        /// 单个成员ID列表
        /// 是否必须： 否
        /// </summary>
        public string ToUser { set; get; }

        /// <summary>
        /// 多个成员ID列表（消息接收者，多个接收者用‘|’分隔，最多支持1000个）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送
        /// 是否必须： 否
        /// </summary>
        public List<string> ToUserList { set; get; }

        /// <summary>
        /// 部门ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数
        /// 是否必须： 否
        /// </summary>
        public string ToParty { set; get; }

        /// <summary>
        /// 标签ID列表，多个接收者用‘|’分隔。当touser为@all时忽略本参数
        /// 是否必须： 否
        /// </summary>
        public string ToTag { set; get; }


        /// <summary>
        /// 消息类型，有 text, image, voice, video, file, news, mpnews
        /// 是否必须： 是
        /// </summary>
        public string MsgType { set; get; }

        /// <summary>
        /// 消息类型，企业应用的id，整型。可在应用的设置页面查看
        /// 是否必须： 是
        /// </summary>
        public string AgentId { set; get; }

        /// <summary>
        /// 表示是否是保密消息，0表示否，1表示是，默认0
        /// 是否必须： 是
        /// </summary>
        public string Safe { set; get; }

        public abstract string ToJsonString();
    }
}
