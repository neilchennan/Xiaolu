using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Xiaolu.core.Utility
{
    public sealed class XiaoluStringUtility
    {
        public static bool IsNumber(string str)
        {
            Regex regex = new Regex(@"^((\+)?|(\-)?)(\d)+$");
            bool isMatch = regex.IsMatch(str);
            return isMatch;
        }
    }
}
