using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top.Api;
using Top.Api.Domain;
using Top.Api.Request;
using Top.Api.Response;
using Xiaolu.Utility.Common;
using Xiaolu.Utility.Result;

namespace Xiaolu.Utility.Helper
{
    public class AlibaichuanMsgSuccess
    {
        public result result;
    }
    public class checkvercode_success_response{
        public AlibaichuanMsgSuccess open_sms_checkvercode_response { set; get; }
    }
    public class sendvercode_success_response
    {
        public AlibaichuanMsgSuccess open_sms_sendvercode_response { set; get; }
    }
    public class sendmsg_success_response
    {
        public AlibaichuanMsgSuccess open_sms_sendmsg_response { set; get; }
    }
    public class result{
        public bool successful { set; get; }
        public string code { set; get; }
        public string message { set; get; }
        public datas datas { set; get; }
    }

    public class datas{
        public string task_id { set; get; }
    }

    public class AlibaichuanMsgError
    {
        public error_response error_response { set; get; }
    }
    public class error_response
    {
        public string code { set; get; }
        public string msg { set; get; }
        public string sub_code { set; get; }
        public string sub_msg { set; get; }
    }

    public static class SmsHelper
    {
        private const string URL = "http://gw.api.taobao.com/router/rest";
        private const string APP_KEY = "23271701";
        private const string APP_SECRET = "c7fb8e4b0db27d3454a441955497e1ec";
        private const long SIGNATURE_ID = 786L;
        //发送短信验证码

        public static BaseActionResult SendMsg(string mobile, string ver_code, bool realsend = true)
        {
            String msg;
            if (!realsend)
            {
                return new BaseActionResult() { IsSuccess = true, Message = @"
                <open_sms_sendmsg_response>
                <result>
                    <code>1</code>
                    <datas>
                        <task_id>421206248265132472</task_id>
                    </datas>
                    <message>SUCCESS</message>
                    <successful>true</successful>
                </result>
                <request_id>101yxmwbhzd8t</request_id>
                </open_sms_sendmsg_response>
                <!--top010178059118.n.et2-->"
                };
            }
            try 
	        {
                ITopClient client = new DefaultTopClient(URL, APP_KEY, APP_SECRET, "json");
                OpenSmsSendmsgRequest req = new OpenSmsSendmsgRequest();
                req.SendMessageRequest = "{\"external_id\":\"wb121212\",\"template_id\":\"142571913\",\"signature_id\":\"786\",\"mobile\":\"" 
                    + mobile + "\",\"context\":{\"code\":\"" + ver_code + "\", \"minute\":\"2\", \"name\":\"小鹿\"}}";

                OpenSmsSendmsgResponse rsp = client.Execute(req);

                string respBody = rsp.Body;
                bool isSuccess = !rsp.IsError;

                if (isSuccess)
                {
                    sendmsg_success_response responseObj = JsonConvert.DeserializeObject<sendmsg_success_response>(respBody);
                    msg = responseObj.open_sms_sendmsg_response.result.message;
                }
                else
                {
                    AlibaichuanMsgError errRspObj = JsonConvert.DeserializeObject<AlibaichuanMsgError>(respBody);
                    msg = errRspObj.error_response.msg;
                }
                return new BaseActionResult() { IsSuccess = isSuccess, Message = msg };
	        }
	        catch (Exception e)
	        {
                msg = XiaoluResources.MSG_SEND_VERCODE_FAIL + string.Format(XiaoluResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new BaseActionResult() { IsSuccess = false, Message = msg };
	        }
        }

        public static BaseActionResult GenVercode(string mobile)
        {
            String msg;
            try
            {
                ITopClient client = new DefaultTopClient(URL, APP_KEY, APP_SECRET, "json");
                OpenSmsSendvercodeRequest req = new OpenSmsSendvercodeRequest();

                req.SendVerCodeRequest = "{\"external_id\":\"wb121212\",\"template_id\":\"142571913\",\"signature_id\":\"786\",\"ver_code_length\":\"4\", \"mobile\":\""
                    + mobile + "\",\"context\":{\"minute\":\"2\", \"name\":\"小鹿\"}}";

                OpenSmsSendvercodeResponse rsp = client.Execute(req);

                string respBody = rsp.Body;
                bool isSuccess = !rsp.IsError;

                if (isSuccess)
                {
                    sendvercode_success_response responseObj = JsonConvert.DeserializeObject<sendvercode_success_response>(respBody);
                    msg = responseObj.open_sms_sendvercode_response.result.message;
                }
                else
                {
                    AlibaichuanMsgError errRspObj = JsonConvert.DeserializeObject<AlibaichuanMsgError>(respBody);
                    msg = errRspObj.error_response.msg;
                }
                return new BaseActionResult() { IsSuccess = isSuccess, Message = msg };
            }
            catch (Exception e)
            {
                msg = XiaoluResources.MSG_GEN_VERCODE_FAIL + string.Format(XiaoluResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new BaseActionResult() { IsSuccess = false, Message = msg };
            }
        }

        public static BaseActionResult CheckVercode(string mobile, string ver_code)
        {
            String msg;
            try
            {
                ITopClient client = new DefaultTopClient(URL, APP_KEY, APP_SECRET, "json");
                OpenSmsCheckvercodeRequest req = new OpenSmsCheckvercodeRequest();

                req.CheckVerCodeRequest = "{\"ver_code\":\"" + ver_code + "\", \"mobile\":\"" + mobile + "\", \"check_fail_limit\" : \"100\"}";

                OpenSmsCheckvercodeResponse rsp = client.Execute(req);

                string respBody = rsp.Body;
                bool isSuccess = !rsp.IsError;

                if (isSuccess)
                {
                    checkvercode_success_response responseObj = JsonConvert.DeserializeObject<checkvercode_success_response>(respBody);
                    msg = responseObj.open_sms_checkvercode_response.result.message;
                }
                else
                {
                    AlibaichuanMsgError errRspObj = JsonConvert.DeserializeObject<AlibaichuanMsgError>(respBody);
                    msg = errRspObj.error_response.msg;
                }
                return new BaseActionResult() { IsSuccess = isSuccess, Message = msg };
            }
            catch (Exception e)
            {
                msg = XiaoluResources.MSG_CHECK_VERCODE_FAIL + string.Format(XiaoluResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new BaseActionResult() { IsSuccess = false, Message = msg };
            }
        }
    }
}
