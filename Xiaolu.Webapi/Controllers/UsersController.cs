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
                //List<UserAPIModel> vmList = UserAPIModel.FromUserList(dbList);

                msg = string.Format(XiaoluResources.MSG_SINGLE_ACTION_SUCCESS, "获取", "用户列表");
                return new { IsSuccess = true, Message = msg, Obj = dbList };
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
            if (objinDb == null)
            {
                msg = string.Format(XiaoluResources.MSG_OBJECT_NOT_FOUND_WITH_ID, id);
                return new { IsSuccess = false, Message = msg };
            }
            msg = string.Format(XiaoluResources.MSG_SINGLE_ACTION_SUCCESS, "获取", "用户");
            return new { IsSuccess = true, Message = msg, Obj = objinDb };
        }

        // POST api/users
        //新建
        public Object Post(User obj)
        {
            BaseActionResult result = BusinessService.CreateUser(obj);
            return new { IsSuccess = result.IsSuccess, Message = result.Message };
        }

        // PUT api/users/5
        //修改
        public Object Put(string id, User obj)
        {
            string msg;
            User objinDb = BusinessService.GetUserById(id);
            if (objinDb == null)
            {
                msg = string.Format(XiaoluResources.MSG_OBJECT_NOT_FOUND_WITH_ID, id);
                return new { IsSuccess = false, Message = msg };
            }
            objinDb.Name = obj.Name;
            objinDb.Description = obj.Description;
            objinDb.Gender = obj.Gender;
            objinDb.Birthday = obj.Birthday;
            objinDb.Level = obj.Level;
            objinDb.Status = obj.Status;
            objinDb.Mobile = obj.Mobile;
            objinDb.WeixinId = obj.WeixinId;

            BaseActionResult result = BusinessService.UpdateUser(objinDb);
            return new { IsSuccess = result.IsSuccess, Message = result.Message };
        }

        // DELETE api/users/5
        public Object Delete(string id)
        {
            BaseActionResult result = BusinessService.BulkDeleteUserByIds(id);
            return new { IsSuccess = result.IsSuccess, Message = result.Message };
        }
    }
}
