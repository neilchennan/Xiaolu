using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Xiaolu.WeixinApi.Common
{
    public class WeiXinHelper
    {
        /// <summary>
        /// 得到当前时间的long值（整型）（考虑时区）
        /// </summary>
        /// <returns></returns>
        public static string GetNowTime()
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳
            long a = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000;
            return a.ToString();
        }

        /// <summary>
        /// 读取请求对象的内容
        /// 只能读一次
        /// </summary>
        /// <param name="request">HttpRequest对象</param>
        /// <returns>string</returns>
        public static string ReadRequest(HttpRequest request)
        {
            string reqStr = string.Empty;
            using (Stream s = request.InputStream)
            {
                using (StreamReader reader = new StreamReader(s, Encoding.UTF8))
                {
                    reqStr = reader.ReadToEnd();
                }
            }

            return reqStr;
        }
    }
}

