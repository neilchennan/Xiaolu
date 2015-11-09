using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xiaolu.Utility.Common;
using Xiaolu.Utility.Result.Interface;

namespace Xiaolu.Utility.Result
{
    public class BaseActionResult : IActionResult<object, BaseActionResult>
    {
        protected bool _isSuccess;
        public bool IsSuccess
        {
            set
            {
                _isSuccess = value;
            }
            get
            {
                if (SubResultList.Count == 0) return _isSuccess;
                foreach (BaseActionResult subResult in SubResultList)
                {
                    if (!subResult.IsSuccess) return false;
                }
                return true;
            }
        }

        public object ResultObject { set; get; }

        public bool IsForMyActionItemPage { set; get; }

        protected string _message;
        public string Message
        {
            set
            {
                _message = value;
            }
            get
            {
                if (string.IsNullOrEmpty(_message))
                {
                    _message = "";
                }

                if (SubResultList.Count == 0) return _message;
                List<string> messageList = new List<string>();

                foreach (BaseActionResult subResult in SubResultList)
                {
                    if (messageList.Find(m => string.Equals(m, subResult.Message)) == null && !string.IsNullOrEmpty(subResult.Message))
                    {
                        messageList.Add(subResult.Message);
                    }
                }
                string joinedMessage = string.Join(XiaoluConstants.SPLIT_SYMBOL_STR, messageList);
                return joinedMessage;
            }
        }

        public int TotalNum
        {
            get
            {
                if (SubResultList.Count == 0) return 1;
                return SubResultList.Count;
            }
        }

        public int SuccessNum
        {
            get
            {
                if (SubResultList.Count == 0) return IsSuccess ? 1 : 0;
                int sCount = 0;
                SubResultList.ForEach(delegate(BaseActionResult subResult)
                {
                    if (subResult.IsSuccess)
                    {
                        sCount++;
                    }
                });
                return sCount;
            }
        }

        public int FailNum
        {
            get
            {
                if (SubResultList.Count == 0) return IsSuccess ? 0 : 1;
                return (SubResultList.Count - SuccessNum);
            }
        }

        protected string _summary;
        public string Summary
        {
            get
            {
                string SummaryFormat = XiaoluResources.MSG_TOTAL_RESULT_SUMMARY;
                return string.Format(SummaryFormat, TotalNum, SuccessNum, FailNum);
            }
        }

        public Exception Exception { set; get; }

        protected List<BaseActionResult> _subResultList;
        public List<BaseActionResult> SubResultList
        {
            set
            {
                _subResultList = value;
            }
            get
            {
                if (_subResultList == null)
                {
                    _subResultList = new List<BaseActionResult>();
                }
                return _subResultList;
            }
        }

        public BaseActionResult(bool isSuccess = true, string message = null, Exception ex = null, bool isForMyActionItemPage = true)
        {
            IsSuccess = isSuccess;
            Message = message;
            Exception = ex;
            IsForMyActionItemPage = isForMyActionItemPage;
        }

        public void AddSubResult(BaseActionResult subResult)
        {
            SubResultList.Add(subResult);
        }
    }
}
