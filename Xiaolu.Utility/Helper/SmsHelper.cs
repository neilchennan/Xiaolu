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
    public static class SmsHelper
    {
        private const string URL = "http://gw.api.taobao.com/router/rest";
        private const string APP_KEY = "23271701";
        private const string APP_SECRET = "c7fb8e4b0db27d3454a441955497e1ec";
        private const long SIGNATURE_ID = 786L;
        //发送短信验证码

        public static BaseActionResult Sendvercode(string mobile, string code, bool realsend = true)
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
		        ITopClient client = new DefaultTopClient(URL, APP_KEY, APP_SECRET);
                OpenSmsSendmsgRequest req = new OpenSmsSendmsgRequest();
                req.SendMessageRequest = "{\"external_id\":\"wb121212\",\"template_id\":\"142571913\",\"signature_id\":\"786\",\"mobile\":\"" 
                    + mobile + "\",\"context\":{\"code\":\"" + code + "\", \"minute\":\"2\", \"name\":\"小鹿\"}}";

                OpenSmsSendmsgResponse rsp = client.Execute(req);
                msg = rsp.Body;
                return new BaseActionResult() { IsSuccess = !rsp.IsError, Message = msg };
	        }
	        catch (Exception e)
	        {
                msg = XiaoluResources.MSG_SEND_VERCODE_FAIL + string.Format(XiaoluResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new BaseActionResult() { IsSuccess = false, Message = msg };
	        }
        }


    }
}
