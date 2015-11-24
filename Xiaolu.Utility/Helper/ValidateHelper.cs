using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Xiaolu.Utility.Helper
{
    public class ValidateHelper
    {
        public const int Len5000 = 5000;
        public const string Pattern4Number = @"^[0-9]+$";
        public const string Pattern4Email = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        #region string-验证

        public static bool Required(string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        public static bool Length(string s, int minLength = 1, int maxLength = Len5000)
        {
            if (s==null)
            {
                return false;
            }
            if(s.Length < minLength) 
            {
                return false;
            }
            if (s.Length > maxLength)
            {
                return false;
            }
            return true;
        }

        public static bool IsNumber(string s)
        {
            return Regex.IsMatch(s, Pattern4Number);
        }

        public static bool IsValidEmail(string s)
        {
            return Regex.IsMatch(s, Pattern4Email); 
        }

        #endregion 

        #region datetime-验证

        public static bool IsDateTime(string str)
        {
            DateTime rslt;
            return DateTime.TryParse(str, out rslt);
        }

        #endregion
    }
}
