using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSoftSoftware.Data.Abstract
{
    public interface IGenericRepository<T> where T:class
    {
        T GetById(int id);
        IQueryable<T> GetAll();
        void Add(T Entity);
        void Update(T Entity);
        void Delete(int id);
        void Save();
    }
}
