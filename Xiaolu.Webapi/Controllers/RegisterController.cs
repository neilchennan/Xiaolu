using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Xiaolu.Models.Service;
using Xiaolu.Webapi.Models;
using Xiaolu.Models.DAL;
using Xiaolu.Utility.Common;
using System.Web.Security;
using Xiaolu.Utility.Result;
using Xiaolu.Utility.Helper;

namespace Xiaolu.Webapi.Controllers
{
    public class RegisterController : ApiController
    {
        // GET api/register
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/register/5
        public string Get(int id)
        {
            return "value";
        }
        
        // POST api/register
        //目前仅这个在用
        public Object Post(RegisterApiModel obj)
        {
            string msg;
            try
            {
                if (string.IsNullOrEmpty(obj.UserId))
                {
                    msg = string.Format(XiaoluResources.MSG_CREATE_FAIL, obj.UserId) + string.Format(XiaoluResources.STR_FAIL_RESAON, XiaoluResources.MSG_USER_ID_IS_NULL);
                    return new { IsSuccess = false, Message = msg };
                }

                if (string.IsNullOrEmpty(obj.Mobile))
                {
                    msg = string.Format(XiaoluResources.MSG_CREATE_FAIL, obj.UserId) + string.Format(XiaoluResources.STR_FAIL_RESAON, XiaoluResources.MSG_MOBILE_IS_NULL);
                    return new { IsSuccess = false, Message = msg };
                }

                if (string.IsNullOrEmpty(obj.Password) || obj.Password.Trim().Length < 6)
                {
                    msg = string.Format(XiaoluResources.MSG_CREATE_FAIL, obj.UserId) + string.Format(XiaoluResources.STR_FAIL_RESAON, XiaoluResources.MSG_PASSWORD_IS_NOT_VALID);
                    return new { IsSuccess = false, Message = msg };
                }

                User userInDb = BusinessService.GetUserByName(obj.UserId);
                if (userInDb != null)
                {
                    msg = string.Format(XiaoluResources.MSG_CREATE_FAIL, obj.UserId) + string.Format(XiaoluResources.STR_FAIL_RESAON, XiaoluResources.MSG_USER_ID_ALREAY_EXIST);
                    return new { IsSuccess = false, Message = msg };
                }

                userInDb = BusinessService.GetUserByMobile(obj.Mobile);
                if (userInDb != null)
                {
                    msg = string.Format(XiaoluResources.MSG_CREATE_FAIL, obj.UserId) + string.Format(XiaoluResources.STR_FAIL_RESAON, XiaoluResources.MSG_MOBILE_ALREAY_EXIST);
                    return new { IsSuccess = false, Message = msg };
                }

                User obj4save = new User()
                {
                    Name = obj.UserId,
                    Mobile = obj.Mobile,
                    Password = FormsAuthentication.HashPasswordForStoringInConfigFile(obj.Password, "MD5")
                };
                BaseActionResult result = BusinessService.CreateUser(obj4save);
                return new { IsSuccess = result.IsSuccess, Message = result.Message };
            }
            catch (Exception e)
            {
                msg = string.Format(XiaoluResources.MSG_CREATE_FAIL, obj.UserId) + string.Format(XiaoluResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new { IsSuccess = false, Message = msg };
            }
        }

        // PUT api/register/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/register/5
        public void Delete(int id)
        {
        }
    }
}
