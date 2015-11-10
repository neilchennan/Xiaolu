using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xiaolu.WeixinApi.Utility;

namespace Xiaolu.WeixinApi.Manager
{
    public class AccessTokenManager
    {
        private static object _lock = new object();
        /// <summary>
        /// 获取token的URL
        /// </summary>
        private const string GET_TOKEN_URL_DEFAULT = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        private static string _getTokenUrl;
        public static string GetTokenUrl
        {
            set
            {
                if (_getTokenUrl == value) return;
                lock (_lock)
                {
                    if (_getTokenUrl != value)
                    {
                        _getTokenUrl = value;
                        _mAccessToken = null;
                    }
                }
            }
            get
            {
                return string.IsNullOrEmpty(_getTokenUrl) ? GET_TOKEN_URL_DEFAULT : _getTokenUrl;
            }
        }

        private static string _appID;
        public static string AppId
        {
            set
            {
                if (_appID == value) return;
                lock (_lock)
                {
                    if (_appID != value)
                    {
                        _appID = value;
                        _mAccessToken = null;
                    }
                }
            }
        }

        private static string _appSecret;
        public static string AppSecret
        {
            set
            {
                if (_appSecret == value) return;
                if (_appSecret != value)
                {
                    _appSecret = value;
                    _mAccessToken = null;
                }
            }
        }

        private static DateTime _getAccessToken_Time;
        /// <summary>
        /// 过期时间为7200秒
        /// </summary>
        private static int Expires_Period = 7200;

        /// <summary>
        /// accessToken值
        /// </summary>
        private static string _mAccessToken;

        public static string AccessToken
        {
            get
            {
                //如果为空，或者过期，需要重新获取
                if (string.IsNullOrEmpty(_mAccessToken) || HasExpired())
                {
                    //获取
                    _mAccessToken = GetAccessToken(_appID, _appSecret);
                    //Log4netHelper.Write(string.Format("GetAccessToken result : {0}", _mAccessToken));
                }

                return _mAccessToken;
            }
        }

        /// <summary>
        /// 判断Access_token是否过期
        /// </summary>
        /// <returns>bool</returns>
        private static bool HasExpired()
        {
            if (_getAccessToken_Time != null)
            {
                //过期时间，允许有一定的误差，一分钟。获取时间消耗
                if (DateTime.Now > _getAccessToken_Time.AddSeconds(Expires_Period).AddSeconds(-60))
                {
                    return true;
                }
            }
            return false;
        }

        private static string GetAccessToken(string appId, string appSecret)
        {
            string url = string.Format(GetTokenUrl, appId, appSecret);
            string result = HttpUtility.GetData(url);

            XDocument doc = XmlUtility.ParseJson(result, "root");
            XElement root = doc.Root;
            if (root != null)
            {
                XElement access_token = root.Element("access_token");
                if (access_token != null)
                {
                    _getAccessToken_Time = DateTime.Now;
                    if (root.Element("expires_in") != null)
                    {
                        Expires_Period = int.Parse(root.Element("expires_in").Value);
                    }
                    return access_token.Value;
                }
                else
                {
                    _getAccessToken_Time = DateTime.MinValue;
                }
            }

            return null;
        }
    }
}

