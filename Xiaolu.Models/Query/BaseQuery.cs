using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiaolu.Models.Query
{
    public class BaseQuery
    {
        public const string ASC = "asc";
        public const string DESC = "desc";

        public BaseQuery()
        {
            OrderByValue = BaseQuery.ASC;
            Offset = -1;
            Limit = 10000;
        }

        public string OrderByValue { set; get; }

        public string OrderByKey { set; get; }

        public int Offset { set; get; }
        public int Limit { set; get; }

        public string IdEqual { set; get; }
        public string IdNotEqual { set; get; }

        public string NameEqual { set; get; }
        public string NameNotEqual { set; get; }
        public string NameLike { set; get; }

        public string DescriptionEqual { set; get; }
        public string DescriptionNotEqual { set; get; }
        public string DescriptionLike { set; get; }

        public string CreatedByIdEqual { set; get; }
        public string CreatedByIdNotEqual { set; get; }

        public string LastModifiedByIdEqual { set; get; }
        public string LastModifiedByIdNotEqual { set; get; }

        public string MinCreationDateStr { set; get; }
        public string MaxCreationDateStr { set; get; }

        public string MinLastModifiedDateStr { set; get; }
        public string MaxLastModifiedDateStr { set; get; }

        public string WorkflowIdEqual { set; get; }
        public string WorkflowIdNotEqual { set; get; }

        public DateTime? MinCreationDate
        {
            set
            {
                if (value == null) return;
                MinCreationDateStr = value.ToString();
            }
            get
            {
                if (string.IsNullOrEmpty(MinCreationDateStr))
                    return null;

                return DateTime.Parse(MinCreationDateStr);
            }
        }

        public DateTime? MaxCreationDate
        {
            set
            {
                if (value == null) return;
                MaxCreationDateStr = value.ToString();
            }
            get
            {
                if (string.IsNullOrEmpty(MaxCreationDateStr))
                    return null;

                return DateTime.Parse(MaxCreationDateStr);
            }
        }

        public DateTime? MinLastModifiedDate
        {
            set
            {
                if (value == null) return;
                MinLastModifiedDateStr = value.ToString();
            }
            get
            {
                if (string.IsNullOrEmpty(MinLastModifiedDateStr))
                    return null;

                return DateTime.Parse(MinLastModifiedDateStr);
            }
        }

        public DateTime? MaxLastModifiedDate
        {
            set
            {
                if (value == null) return;
                MaxLastModifiedDateStr = value.ToString();
            }
            get
            {
                if (string.IsNullOrEmpty(MaxLastModifiedDateStr))
                    return null;

                return DateTime.Parse(MaxLastModifiedDateStr);
            }
        }
    }
}
