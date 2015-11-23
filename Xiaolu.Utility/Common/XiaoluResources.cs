using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiaolu.Utility.Common
{
    public class XiaoluResources
    {
        public static string STR_SUCCESS = "成功";
        public static string STR_FAIL = "失败";
        public static string STR_USER = "用户";
        public static string STR_DELETE = "删除";
        public static string STR_PROFESSION = "专业";
        public static string STR_USER_ACCESS_TOKEN = "用户访问令牌";
        public static string STR_HISTORY = "历史";

        public static string MSG_OBJECT_IS_NULL = "对象为空";
        public static string MSG_CREATE_SUCCESS = "成功新建 '{0}'。";
        public static string MSG_CREATE_FAIL = "无法创建 '{0}'。";
        public static string STR_FAIL_RESAON = "失败原因 ： {0}";
        public static string MSG_UPDATE_SUCCESS = "成功更新 '{0}'。";
        public static string MSG_UPDATE_FAIL = "无法更新 '{0}'。";
        public static string MSG_DELETE_SUCCESS = "成功删除 '{0}'。";
        public static string MSG_DELETE_FAIL = "无法删除 '{0}'。";
        public static string MSG_BULK_ACTION_SUCCESS = "成功{0}{1}条记录。";
        public static string MSG_BULK_ACTION_FAIL = "{0}{1}条记录失败。";
        public static string MSG_SEND_VERCODE_FAIL = "验证码发送失败。";
        public static string MSG_GEN_VERCODE_FAIL = "生成验证码失败。";
        public static string MSG_CHECK_VERCODE_FAIL = "检查验证码失败。";

        public static string MSG_USER_ID_ALREAY_EXIST = "用户Id已存在";
        public static string MSG_MOBILE_ALREAY_EXIST = "手机号已存在";
        public static string MSG_USER_ID_IS_NULL = "用户号为空";
        public static string MSG_MOBILE_IS_NULL = "手机号为空";
        public static string MSG_PASSWORD_IS_NOT_VALID = "密码不合法，必须至少6位";

        public static string MSG_INNER_EXCEPTION_INFO = "出错位置：{0}{1}--错误信息：{2}";
        public static string MSG_TOTAL_RESULT_SUMMARY = "总数 ： {0}, 成功 ： {1}, 失败 ： {2} 。";
        public static string MSG_SINGLE_ACTION_SUCCESS = "成功{0}{1}。";
        public static string MSG_SINGLE_ACTION_FAIL = "{0}{1}失败。";
        public static string MSG_OBJECT_NOT_FOUND_WITH_ID = "未找到Id为 '{0}' 的对象。";

        public static string ERR_MSG_NO_RECORD_FOR_ACTION = "#未找到需要操作的记录";
        public static string ERR_MSG_CHECK_SIGNATURE_FAIL = "签名验证出错";
        public static string ERR_MSG_HTTP_METHOD_NOT_SUPPORT = "该HTTP";
    }
}
