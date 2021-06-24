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
    public class EfCompensationRepository: ICompensationRepository
    {
        private readonly YSoftContext context;

        public EfCompensationRepository(YSoftContext _context)
        {
            context = _context;
        }


        public IQueryable<Compensation> GetAll()
        {
            return context.Compensations.Include(c=>c.Personel);
        }

        public void Add(int personelId,decimal money)
        {
            var personel = context.Personels.FirstOrDefault(p=>p.PersonelId==personelId);

            Compensation compensation = new Compensation()
            {
                PersonelId = personelId,
                Money = money
            };
            context.Compensations.Add(compensation);

        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
