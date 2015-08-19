using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiaolu.Models.Repository.Interface
{
    public interface IRepository<T>
    {
        void Create(T obj4create);
        //T GetById(string id4query);
        T GetByModel(T model4query);

        //void DeleteById(string id4delete);
        void Delete(T obj4delete);

        void Update(T obj4update);
        //List<T> FindAll();
        //int Count();
        //void DeleteAll();

        //List<T> FindAllByQuery(BaseQuery query);

        //void BulkDeleteByIds(string[] ids);
    }
}
