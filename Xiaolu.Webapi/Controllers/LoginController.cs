using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using Xiaolu.Models.DAL;
using Xiaolu.Models.Service;
using Xiaolu.Utility.Common;
using Xiaolu.Utility.Helper;
using Xiaolu.Webapi.Models;

namespace Xiaolu.Webapi.Controllers
{
    public class LoginController : ApiController
    {
        private const int PWD_ALLOW_TIMES = 5;
        //用户可以保持登录状态的时长(秒)
        private const int ACCESS_TOKEN_DURATION_IN_SECONDS = 900;
        // GET api/login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/login
        public Object Post(LoginApiModel obj)
        {
            string msg;
            try
            {
                if (string.IsNullOrEmpty(obj.UserIdOrMobile))
                {
                    msg = XiaoluResources.MSG_LOGIN_FAIL + string.Format(XiaoluResources.STR_FAIL_RESAON, XiaoluResources.MSG_MOBILE_OR_USERID_IS_NULL);
                    return new { IsSuccess = false, Message = msg };
                }

                if (string.IsNullOrEmpty(obj.Password) || obj.Password.Trim().Length < 6)
                {
                    msg = XiaoluResources.MSG_LOGIN_FAIL + string.Format(XiaoluResources.STR_FAIL_RESAON, XiaoluResources.MSG_PASSWORD_IS_NOT_VALID);
                    return new { IsSuccess = false, Message = msg };
                }

                User userInDb = BusinessService.GetUserByName(obj.UserIdOrMobile);
                User userInDb2 = BusinessService.GetUserByMobile(obj.UserIdOrMobile);
                if (userInDb == null && userInDb2 == null )
                {
                    msg = XiaoluResources.MSG_LOGIN_FAIL + string.Format(XiaoluResources.STR_FAIL_RESAON, XiaoluResources.MSG_CANNOT_FIND_USER);
                    return new { IsSuccess = false, Message = msg };
                }

                User findedUser = (userInDb == null) ? userInDb2 : userInDb;
                string md5Pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(obj.Password, "MD5");
                //验证密码
                if (!string.Equals(md5Pwd, findedUser.Password))
                {
                    findedUser.LastFailLoginTime = DateTime.Now;
                    if (findedUser.ErrLoginTimes == null)
                    {
                        findedUser.ErrLoginTimes = 0;
                    }
                    findedUser.ErrLoginTimes++;
                    BusinessService.UpdateUser(findedUser);

                    msg = XiaoluResources.MSG_LOGIN_FAIL + string.Format(XiaoluResources.STR_FAIL_RESAON, XiaoluResources.MSG_PASSWORD_IS_IN_CORRECT);
                    return new { IsSuccess = false, Message = msg };
                }
                //密码验证通过，则登录成功
                //写历史记录
                msg = XiaoluResources.MSG_LOGIN_SUCCESS;
                History his = new History()
                {
                    UserId = findedUser.Name,
                    CreationDate = DateTime.Now,
                    Content = msg
                };
                BusinessService.CreateHistory(his);

                string accessToken4User;
                UserAccessToken uatInDb = BusinessService.GetAccessTokenByUserId(findedUser.Name);
                if (uatInDb != null && uatInDb.ExpireDate > DateTime.Now)
                {
                    accessToken4User = uatInDb.AccessToken;
                    return new { IsSuccess = true, Message = msg, AccessToken = accessToken4User };
                }
                if (uatInDb != null)
                {
                    BusinessService.DeleteUserAccessToken(uatInDb);
                }

                accessToken4User = Guid.NewGuid().ToString();
                UserAccessToken uat = new UserAccessToken()
                {
                    UserId = findedUser.Name,
                    AccessToken = accessToken4User,
                    WeixinId = findedUser.WeixinId,
                    ExpireDate = DateTime.Now.AddSeconds(ACCESS_TOKEN_DURATION_IN_SECONDS)
                };
                BusinessService.CreateUserAccessToken(uat);

                AccessTokenUserPool.AddAccessTokenUserId(accessToken4User, findedUser.Name);
                return new { IsSuccess = true, Message = msg, AccessToken = accessToken4User };
            }
            catch (Exception e)
            {
                msg = XiaoluResources.MSG_LOGIN_FAIL + string.Format(XiaoluResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new { IsSuccess = false, Message = msg };
            }
        }

        // PUT api/login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/login/5
        public void Delete(int id)
        {
        }
    }
}
