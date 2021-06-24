using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YSoftSoftware.Data.Abstract;
using YSoftSoftware.Entity;

namespace YSoftSoftware.Data.Concrete.ef
{
    public class EfAccountingProgramRepository:IAccountingProgramRepository
    {
        public YSoftContext context { get; set; }

        public EfAccountingProgramRepository(YSoftContext _context)
        {
            context = _context;
        }

        public AccountingProgram GetById(int id)
        {
            return context.AccountingProgram.FirstOrDefault(a=>a.AccountingProgramId==id);
        }

        public IQueryable<AccountingProgram> GetAll()
        {
            return context.AccountingProgram;
        }

        public void Add(AccountingProgram Entity)
        {
            context.AccountingProgram.Add(Entity);
        }

        public void Update(AccountingProgram Entity)
        {
            context.Entry<AccountingProgram>(Entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var result =
                context.AccountingProgram.Include(p=>p.Personels).FirstOrDefault(a => a.AccountingProgramId == id);
            
            //result.Personels.Clear();
            //context.SaveChanges();
            if (result != null)
            {
                context.AccountingProgram.Remove(result);
            }

        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
