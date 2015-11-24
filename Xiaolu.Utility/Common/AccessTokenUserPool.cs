using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiaolu.Utility.Common
{
    public class AccessTokenUserPool
    {
        private const int MAX_ACCESS_TOKEN_NUMBER = 10000;
        private AccessTokenUserPool() { }

        private static List<string> _dicList = new List<string>();

        private static readonly Dictionary<string, string> _accessTokenUserDic = new Dictionary<string, string>();
        private static readonly object _lock = new object();

        public static string GetAccessTokenByUserId(string accessToken)
        {
            return _accessTokenUserDic.ContainsKey(accessToken) ? _accessTokenUserDic[accessToken] : null;
        }

        public static void AddAccessTokenUserId(string accessToken, string userId)
        {
            // 此处认为在锁外读散列表不会产生并行问题。
            if (!_accessTokenUserDic.ContainsKey(accessToken))
            {
                lock (_lock)
                {
                    if (!_accessTokenUserDic.ContainsKey(accessToken))
                    {
                        _accessTokenUserDic[accessToken] = userId;
                        if (_dicList.Count >= MAX_ACCESS_TOKEN_NUMBER)
                        {
                            string oldest_accessToken = _dicList[0];
                            if (_accessTokenUserDic.ContainsKey(oldest_accessToken))
                            {
                                _accessTokenUserDic.Remove(oldest_accessToken);
                            }
                            _dicList.RemoveAt(0);
                        }
                        if (_dicList.Count < MAX_ACCESS_TOKEN_NUMBER)
                        {
                            _dicList.Add(accessToken);
                        }
                    }
                }
            }
        }

        public static void RemoveUserByAccessToken(string accessToken)
        {
            if (_accessTokenUserDic.ContainsKey(accessToken))
            {
                lock (_lock)
                {
                    if (_accessTokenUserDic.ContainsKey(accessToken))
                    {
                        _accessTokenUserDic.Remove(accessToken);
                    }
                }
            }
        }
    }
}
