using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xiaolu.Models.DAL;

namespace Xiaolu.Models.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(XiaoluEntities context) : base(context) { }
    }
}
