using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xiaolu.Models.DAL;
using Xiaolu.Models.Query;
using Xiaolu.Models.Repository;
using Xiaolu.Utility.Common;
using Xiaolu.Utility.Helper;
using Xiaolu.Utility.Result;

namespace Xiaolu.Models.Service
{
    public static partial class BusinessService
    {
        public static BaseActionResult CreateUserAccessToken(UserAccessToken obj4create)
        {
            string msg;
            if (obj4create == null)
            {
                msg = string.Format(XiaoluResources.MSG_CREATE_SUCCESS, XiaoluResources.STR_USER_ACCESS_TOKEN) + string.Format(XiaoluResources.STR_FAIL_RESAON, XiaoluResources.MSG_OBJECT_IS_NULL);
                return new BaseActionResult(false, msg);
            }

            try
            {
                using (var context = new XiaoluEntities())
                {
                    var repository = new UserAccessTokenRepository(context);
                    string newId = Guid.NewGuid().ToString();
                    obj4create.Id = newId;
                    repository.Create(obj4create);
                    context.SaveChanges();
                    msg = string.Format(XiaoluResources.MSG_CREATE_SUCCESS, obj4create.AccessToken);
                    return new BaseActionResult(true, msg);
                }
            }
            catch (Exception e)
            {
                msg = string.Format(XiaoluResources.MSG_CREATE_FAIL, obj4create.AccessToken) + string.Format(XiaoluResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new BaseActionResult(false, msg);
            }
        }

        public static BaseActionResult UpdateUserAccessToken(UserAccessToken obj4update)
        {
            string msg;
            if (obj4update == null)
            {
                msg = string.Format(XiaoluResources.MSG_UPDATE_SUCCESS, XiaoluResources.STR_USER_ACCESS_TOKEN) + string.Format(XiaoluResources.STR_FAIL_RESAON, XiaoluResources.MSG_OBJECT_IS_NULL);
                return new BaseActionResult(false, msg);
            }

            try
            {
                using (var context = new XiaoluEntities())
                {
                    var repository = new UserAccessTokenRepository(context);
                    repository.Update(obj4update);
                    context.SaveChanges();
                    msg = string.Format(XiaoluResources.MSG_UPDATE_SUCCESS, obj4update.AccessToken);
                    return new BaseActionResult(true, msg);
                }
            }
            catch (Exception e)
            {
                msg = string.Format(XiaoluResources.MSG_UPDATE_FAIL, obj4update.AccessToken) + string.Format(XiaoluResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new BaseActionResult(false, msg);
            }
        }

        public static BaseActionResult DeleteUserAccessToken(UserAccessToken obj4delete)
        {
            using (var context = new XiaoluEntities())
            {
                string msg;
                var repository = new UserAccessTokenRepository(context);

                if (obj4delete == null)
                {
                    msg = string.Format(XiaoluResources.MSG_DELETE_SUCCESS, XiaoluResources.STR_USER_ACCESS_TOKEN) + string.Format(XiaoluResources.STR_FAIL_RESAON, XiaoluResources.MSG_OBJECT_IS_NULL);
                    return new BaseActionResult(false, msg);
                }
                repository.Delete(obj4delete);
                context.SaveChanges();
                msg = string.Format(XiaoluResources.MSG_UPDATE_SUCCESS, obj4delete.AccessToken);
                return new BaseActionResult(true, msg);
            }
        }

        public static UserAccessToken GetUserAccessTokenById(string id4query)
        {
            UserAccessTokenQuery query = new UserAccessTokenQuery() { IdEqual = id4query };
            UserAccessToken userAccessTokenInDb = GetUserAccessTokenListByQuery(query).FirstOrDefault();
            return userAccessTokenInDb;
        }

        public static bool _isMatch(UserAccessToken obj, UserAccessTokenQuery query)
        {
            if (!string.IsNullOrEmpty(query.IdEqual) && !string.Equals(obj.Id, query.IdEqual))
                return false;
            if (!string.IsNullOrEmpty(query.IdNotEqual) && string.Equals(obj.Id, query.IdNotEqual))
                return false;

            if (!string.IsNullOrEmpty(query.UserIdEqual) && !string.Equals(obj.UserId, query.UserIdEqual))
                return false;

            if (!string.IsNullOrEmpty(query.AccessIdEqual) && !string.Equals(obj.AccessToken, query.AccessIdEqual))
                return false;

            if (!string.IsNullOrEmpty(query.WeixinIdEqual) && !string.Equals(obj.WeixinId, query.WeixinIdEqual))
                return false;

            return true;
        }

        private static dynamic _orderByKey(UserAccessToken obj, UserAccessTokenQuery query)
        {
            if (string.IsNullOrEmpty(query.OrderByKey))
                return obj.Id;
            return obj.GetType().GetProperty(query.OrderByKey).GetValue(obj);
        }

        public static List<UserAccessToken> GetUserAccessTokenListByQuery(UserAccessTokenQuery query)
        {
            using (var context = new XiaoluEntities())
            {
                var repository = new UserAccessTokenRepository(context);

                List<UserAccessToken> userAccessTokens = repository.GetPageList(item => _isMatch(item, query), item => _orderByKey(item, query), query.OrderByValue, query.Offset, query.Limit);
                return userAccessTokens;
            }
        }

        public static int CountUserAccessTokenByQuery(UserAccessTokenQuery query)
        {
            using (var context = new XiaoluEntities())
            {
                var repository = new UserAccessTokenRepository(context);

                int count = repository.GetPageCount(item => _isMatch(item, query));
                return count;
            }
        }

        public static BaseActionResult BulkDeleteUserAccessTokenByIds(string idsStr)
        {
            string msg;
            string[] idArr = idsStr.Split(',');
            if (idArr.Length == 0)
            {
                msg = XiaoluResources.ERR_MSG_NO_RECORD_FOR_ACTION;
                return new BaseActionResult(false, msg);
            }
            try
            {
                List<UserAccessToken> list4delete = new List<UserAccessToken>();
                foreach (string id in idArr)
                {
                    var obj4delete = GetUserAccessTokenById(id);
                    list4delete.Add(obj4delete);
                }

                using (var context = new XiaoluEntities())
                {
                    var repository = new UserAccessTokenRepository(context);
                    repository.BulkDelete(list4delete);
                    context.SaveChanges();
                    msg = string.Format(XiaoluResources.MSG_BULK_ACTION_SUCCESS, XiaoluResources.STR_USER_ACCESS_TOKEN, idArr.Length);
                    return new BaseActionResult(true, msg);
                }
            }
            catch (Exception e)
            {
                msg = string.Format(XiaoluResources.MSG_BULK_ACTION_FAIL, XiaoluResources.STR_DELETE, idArr.Length) + string.Format(XiaoluResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new BaseActionResult(false, msg, e);
            }
        }
    }
}
