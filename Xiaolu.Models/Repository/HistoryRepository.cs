using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xiaolu.Models.DAL;

namespace Xiaolu.Models.Repository
{
    public class HistoryRepository: BaseRepository<History>
    {
        public HistoryRepository(XiaoluEntities context) : base(context) { }
    }
}
