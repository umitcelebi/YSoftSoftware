using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSoftSoftware.Entity;

namespace YSoftSoftware.Data.Abstract
{
    public interface ICompensationRepository
    {
        IQueryable<Compensation> GetAll();
        void Add(int personelId, decimal money);
        void Save();
    }
}
