using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiaolu.Models.Query
{
    public class HistoryQuery : BaseQuery
    {
        public string UserIdEqual { set; get; }
        public string ContentLike { set; get; }
    }
}
