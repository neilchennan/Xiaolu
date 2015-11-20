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
        public static BaseActionResult CreateHistory(History obj4create)
        {
            string msg;
            if (obj4create == null)
            {
                msg = string.Format(XiaoluResources.MSG_CREATE_SUCCESS, XiaoluResources.STR_HISTORY) + string.Format(XiaoluResources.STR_FAIL_RESAON, XiaoluResources.MSG_OBJECT_IS_NULL);
                return new BaseActionResult(false, msg);
            }

            try
            {
                using (var context = new XiaoluEntities())
                {
                    var repository = new HistoryRepository(context);
                    string newId = Guid.NewGuid().ToString();
                    obj4create.Id = newId;
                    repository.Create(obj4create);
                    context.SaveChanges();
                    msg = string.Format(XiaoluResources.MSG_CREATE_SUCCESS, obj4create.UserId);
                    return new BaseActionResult(true, msg);
                }
            }
            catch (Exception e)
            {
                msg = string.Format(XiaoluResources.MSG_CREATE_FAIL, obj4create.UserId) + string.Format(XiaoluResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new BaseActionResult(false, msg);
            }
        }

        public static BaseActionResult UpdateHistory(History obj4update)
        {
            string msg;
            if (obj4update == null)
            {
                msg = string.Format(XiaoluResources.MSG_UPDATE_SUCCESS, XiaoluResources.STR_HISTORY) + string.Format(XiaoluResources.STR_FAIL_RESAON, XiaoluResources.MSG_OBJECT_IS_NULL);
                return new BaseActionResult(false, msg);
            }

            try
            {
                using (var context = new XiaoluEntities())
                {
                    var repository = new HistoryRepository(context);
                    repository.Update(obj4update);
                    context.SaveChanges();
                    msg = string.Format(XiaoluResources.MSG_UPDATE_SUCCESS, obj4update.UserId);
                    return new BaseActionResult(true, msg);
                }
            }
            catch (Exception e)
            {
                msg = string.Format(XiaoluResources.MSG_UPDATE_FAIL, obj4update.UserId) + string.Format(XiaoluResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new BaseActionResult(false, msg);
            }
        }

        public static BaseActionResult DeleteHistory(History obj4delete)
        {
            using (var context = new XiaoluEntities())
            {
                string msg;
                var repository = new HistoryRepository(context);

                if (obj4delete == null)
                {
                    msg = string.Format(XiaoluResources.MSG_DELETE_SUCCESS, XiaoluResources.STR_HISTORY) + string.Format(XiaoluResources.STR_FAIL_RESAON, XiaoluResources.MSG_OBJECT_IS_NULL);
                    return new BaseActionResult(false, msg);
                }
                repository.Delete(obj4delete);
                context.SaveChanges();
                msg = string.Format(XiaoluResources.MSG_UPDATE_SUCCESS, obj4delete.UserId);
                return new BaseActionResult(true, msg);
            }
        }

        public static History GetHistoryById(string id4query)
        {
            HistoryQuery query = new HistoryQuery() { IdEqual = id4query };
            History historyInDb = GetHistoryListByQuery(query).FirstOrDefault();
            return historyInDb;
        }

        public static bool _isMatch(History obj, HistoryQuery query)
        {
            if (!string.IsNullOrEmpty(query.IdEqual) && !string.Equals(obj.Id, query.IdEqual))
                return false;
            if (!string.IsNullOrEmpty(query.IdNotEqual) && string.Equals(obj.Id, query.IdNotEqual))
                return false;

            if (!string.IsNullOrEmpty(query.UserIdEqual) && !string.Equals(obj.UserId, query.UserIdEqual))
                return false;
            if (!string.IsNullOrEmpty(query.ContentLike) && !obj.Content.Contains(query.ContentLike))
                return false;

            if ((null != query.MinCreationDate) && (obj.CreationDate < query.MinCreationDate))
                return false;
            if ((null != query.MaxCreationDate) && (obj.CreationDate > query.MaxCreationDate))
                return false;

            return true;
        }

        private static dynamic _orderByKey(History obj, HistoryQuery query)
        {
            if (string.IsNullOrEmpty(query.OrderByKey))
                return obj.Id;
            return obj.GetType().GetProperty(query.OrderByKey).GetValue(obj);
        }

        public static List<History> GetHistoryListByQuery(HistoryQuery query)
        {
            using (var context = new XiaoluEntities())
            {
                var repository = new HistoryRepository(context);

                List<History> historys = repository.GetPageList(item => _isMatch(item, query), item => _orderByKey(item, query), query.OrderByValue, query.Offset, query.Limit);
                return historys;
            }
        }

        public static int CountHistoryByQuery(HistoryQuery query)
        {
            using (var context = new XiaoluEntities())
            {
                var repository = new HistoryRepository(context);

                int count = repository.GetPageCount(item => _isMatch(item, query));
                return count;
            }
        }

        public static BaseActionResult BulkDeleteHistoryByIds(string idsStr)
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
                List<History> list4delete = new List<History>();
                foreach (string id in idArr)
                {
                    var obj4delete = GetHistoryById(id);
                    list4delete.Add(obj4delete);
                }

                using (var context = new XiaoluEntities())
                {
                    var repository = new HistoryRepository(context);
                    repository.BulkDelete(list4delete);
                    context.SaveChanges();
                    msg = string.Format(XiaoluResources.MSG_BULK_ACTION_SUCCESS, XiaoluResources.STR_HISTORY, idArr.Length);
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
