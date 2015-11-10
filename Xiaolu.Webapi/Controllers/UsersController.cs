using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Xiaolu.Models.DAL;
using Xiaolu.Models.Query;
using Xiaolu.Models.Service;
using Xiaolu.Utility.Common;
using Xiaolu.Utility.Helper;
using Xiaolu.Utility.Result;
using Xiaolu.Webapi.Models;

namespace Xiaolu.Webapi.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/users
        public Object Get(UserQuery query)
        {
            string msg;
            try
            {
                if (query == null)
                    query = new UserQuery();
                List<User> dbList = BusinessService.GetUserListByQuery(query);
                List<UserApiModel> vmList = UserApiModel.FromUserList(dbList);

                msg = string.Format(XiaoluResources.MSG_SINGLE_ACTION_SUCCESS, "获取", "用户列表");
                return new { IsSuccess = true, Message = msg, Obj = vmList };
            }
            catch (Exception e)
            {
                msg = string.Format(XiaoluResources.MSG_SINGLE_ACTION_FAIL, "获取", "用户列表") + string.Format(XiaoluResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new { IsSuccess = false, Message = msg };
            }
        }

        // GET api/users/5
        public Object Get(string id)
        {
            string msg;
            User objinDb = BusinessService.GetUserById(id);
            UserApiModel vmObj = new UserApiModel(objinDb);
            if (objinDb == null)
            {
                msg = string.Format(XiaoluResources.MSG_OBJECT_NOT_FOUND_WITH_ID, id);
                return new { IsSuccess = false, Message = msg };
            }
            msg = string.Format(XiaoluResources.MSG_SINGLE_ACTION_SUCCESS, "获取", "用户");
            return new { IsSuccess = true, Message = msg, Obj = vmObj };
        }

        // POST api/users
        //新建
        public Object Post(UserApiModel obj)
        {
            string msg;
            try
            {
                User obj4save = obj.ToDto();
                BaseActionResult result = BusinessService.CreateUser(obj4save);
                return new { IsSuccess = result.IsSuccess, Message = result.Message };
            }
            catch (Exception e)
            {
                msg = string.Format(XiaoluResources.MSG_CREATE_FAIL, obj.Name) + string.Format(XiaoluResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new { IsSuccess = false, Message = msg };
            }
        }

        // PUT api/users/5
        //修改
        public Object Put(string id, UserApiModel obj)
        {
            string msg;
            try
            {
                User obj4save = obj.ToDto();

                User objinDb = BusinessService.GetUserById(id);
                if (objinDb == null)
                {
                    msg = string.Format(XiaoluResources.MSG_OBJECT_NOT_FOUND_WITH_ID, id);
                    return new { IsSuccess = false, Message = msg };
                }
                objinDb.Name = obj4save.Name;
                objinDb.Description = obj4save.Description;
                objinDb.Gender = obj4save.Gender;
                objinDb.Birthday = obj4save.Birthday;
                objinDb.Level = obj4save.Level;
                objinDb.Status = obj4save.Status;
                objinDb.Mobile = obj4save.Mobile;
                objinDb.WeixinId = obj4save.WeixinId;

                BaseActionResult result = BusinessService.UpdateUser(objinDb);
                return new { IsSuccess = result.IsSuccess, Message = result.Message };
            }
            catch (Exception e)
            {
                msg = string.Format(XiaoluResources.MSG_CREATE_FAIL, obj.Name) + string.Format(XiaoluResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new { IsSuccess = false, Message = msg };
            }
        }

        // DELETE api/users/5
        public Object Delete(string id)
        {
            BaseActionResult result = BusinessService.BulkDeleteUserByIds(id);
            return new { IsSuccess = result.IsSuccess, Message = result.Message };
        }
    }
}
