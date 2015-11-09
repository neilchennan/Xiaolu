using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiaolu.Models.Service
{
    public static partial class BusinessService
    {
    //    public static BaseActionResult CreateUser(User obj4create)
    //    {
    //        string msg;
    //        if (obj4create == null)
    //        {
    //            msg = string.Format(Intern4jobResources.MSG_CREATE_SUCCESS, Intern4jobResources.STR_USER) + string.Format(Intern4jobResources.STR_FAIL_RESAON, Intern4jobResources.MSG_OBJECT_IS_NULL);
    //            return new BaseActionResult(false, msg);
    //        }

    //        try
    //        {
    //            using (var context = new Intern4jobEntities())
    //            {
    //                var repository = new UserRepository(context);
    //                string newId = Guid.NewGuid().ToString();
    //                obj4create.Id = newId;
    //                repository.Create(obj4create);
    //                context.SaveChanges();
    //                msg = string.Format(Intern4jobResources.MSG_CREATE_SUCCESS, obj4create.Name);
    //                return new BaseActionResult(true, msg);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            msg = string.Format(Intern4jobResources.MSG_CREATE_FAIL, obj4create.Name) + string.Format(Intern4jobResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
    //            return new BaseActionResult(false, msg);
    //        }
    //    }

    //    public static BaseActionResult UpdateUser(User obj4update)
    //    {
    //        string msg;
    //        if (obj4update == null)
    //        {
    //            msg = string.Format(Intern4jobResources.MSG_UPDATE_SUCCESS, Intern4jobResources.STR_USER) + string.Format(Intern4jobResources.STR_FAIL_RESAON, Intern4jobResources.MSG_OBJECT_IS_NULL);
    //            return new BaseActionResult(false, msg);
    //        }

    //        try
    //        {
    //            using (var context = new Intern4jobEntities())
    //            {
    //                var repository = new UserRepository(context);
    //                repository.Update(obj4update);
    //                context.SaveChanges();
    //                msg = string.Format(Intern4jobResources.MSG_UPDATE_SUCCESS, obj4update.Name);
    //                return new BaseActionResult(true, msg);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            msg = string.Format(Intern4jobResources.MSG_UPDATE_FAIL, obj4update.Name) + string.Format(Intern4jobResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
    //            return new BaseActionResult(false, msg);
    //        }
    //    }

    //    public static BaseActionResult DeleteUser(User obj4delete)
    //    {
    //        using (var context = new Intern4jobEntities())
    //        {
    //            string msg;
    //            var repository = new UserRepository(context);

    //            if (obj4delete == null)
    //            {
    //                msg = string.Format(Intern4jobResources.MSG_DELETE_SUCCESS, Intern4jobResources.STR_USER) + string.Format(Intern4jobResources.STR_FAIL_RESAON, Intern4jobResources.MSG_OBJECT_IS_NULL);
    //                return new BaseActionResult(false, msg);
    //            }
    //            repository.Delete(obj4delete);
    //            context.SaveChanges();
    //            msg = string.Format(Intern4jobResources.MSG_UPDATE_SUCCESS, obj4delete.Name);
    //            return new BaseActionResult(true, msg);
    //        }
    //    }

    //    public static User GetUserById(string id4query)
    //    {
    //        UserQuery query = new UserQuery() { IdEqual = id4query };
    //        User userInDb = GetUserListByQuery(query).FirstOrDefault();
    //        return userInDb;
    //    }

    //    public static bool _isMatch(User obj, UserQuery query)
    //    {
    //        if (!string.IsNullOrEmpty(query.IdEqual) && !string.Equals(obj.Id, query.IdEqual))
    //            return false;
    //        if (!string.IsNullOrEmpty(query.IdNotEqual) && string.Equals(obj.Id, query.IdNotEqual))
    //            return false;

    //        if (!string.IsNullOrEmpty(query.NameEqual) && !string.Equals(obj.Name, query.NameEqual))
    //            return false;
    //        if (!string.IsNullOrEmpty(query.NameNotEqual) && string.Equals(obj.Name, query.NameNotEqual))
    //            return false;
    //        if (!string.IsNullOrEmpty(query.NameLike) && !obj.Name.Contains(query.NameLike))
    //            return false;

    //        if (!string.IsNullOrEmpty(query.StatusEqual) && !string.Equals(obj.Status, query.StatusEqual))
    //            return false;
    //        if (!string.IsNullOrEmpty(query.StatusNotEqual) && string.Equals(obj.Status, query.StatusNotEqual))
    //            return false;
    //        if ((query.StatusIn != null) && (query.StatusIn.Select(item => string.Equals(item, obj.Status)) == null))
    //            return false;
    //        if ((query.StatusNotIn != null) && (query.StatusNotIn.Select(item => string.Equals(item, obj.Status)) != null))
    //            return false;

    //        return true;
    //    }

    //    private static dynamic _orderByKey(User obj, UserQuery query)
    //    {
    //        if (string.IsNullOrEmpty(query.OrderByKey))
    //            return obj.Id;
    //        return obj.GetType().GetProperty(query.OrderByKey).GetValue(obj);
    //    }

    //    public static List<User> GetUserListByQuery(UserQuery query)
    //    {
    //        using (var context = new Intern4jobEntities())
    //        {
    //            var repository = new UserRepository(context);

    //            List<User> users = repository.GetPageList(item => _isMatch(item, query), item => _orderByKey(item, query), query.OrderByValue, query.Offset, query.Limit);
    //            return users;
    //        }
    //    }

    //    public static int CountUserByQuery(UserQuery query)
    //    {
    //        using (var context = new Intern4jobEntities())
    //        {
    //            var repository = new UserRepository(context);

    //            int count = repository.GetPageCount(item => _isMatch(item, query));
    //            return count;
    //        }
    //    }

    //    public static BaseActionResult BulkDeleteUserByIds(string idsStr)
    //    {
    //        string msg;
    //        string[] idArr = idsStr.Split(',');
    //        if (idArr.Length == 0)
    //        {
    //            msg = Intern4jobResources.ERR_MSG_NO_RECORD_FOR_ACTION;
    //            return new BaseActionResult(false, msg);
    //        }
    //        try
    //        {
    //            List<User> list4delete = new List<User>();
    //            foreach (string id in idArr)
    //            {
    //                var obj4delete = GetUserById(id);
    //                list4delete.Add(obj4delete);
    //            }

    //            using (var context = new Intern4jobEntities())
    //            {
    //                var repository = new UserRepository(context);
    //                repository.BulkDelete(list4delete);
    //                context.SaveChanges();
    //                msg = string.Format(Intern4jobResources.MSG_BULK_ACTION_SUCCESS, Intern4jobResources.STR_USER, idArr.Length);
    //                return new BaseActionResult(true, msg);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            msg = string.Format(Intern4jobResources.MSG_BULK_ACTION_FAIL, Intern4jobResources.STR_DELETE, idArr.Length) + string.Format(Intern4jobResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
    //            return new BaseActionResult(false, msg, e);
    //        }
    //    }
    }
}