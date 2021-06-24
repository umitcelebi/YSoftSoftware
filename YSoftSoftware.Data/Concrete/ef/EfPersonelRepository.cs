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
    public class EfPersonelRepository:IPersonelRepository
    {
        public YSoftContext context { get; set; }

        public EfPersonelRepository(YSoftContext _context)
        {
            context = _context;
        }
        public Personel GetById(int id)
        {
            return context.Personels
                .Include(p=>p.Department)
                .Include(p=>p.AccountingProgram)
                .FirstOrDefault(p => p.PersonelId == id);
        }

        public IQueryable<Personel> GetAll()
        {
            return context.Personels;
        }

        public void Add(Personel Entity)
        {
            Entity.Status = true;
            Entity.StartDate=DateTime.Now;
            context.Personels.Add(Entity);
        }

        public void Update(Personel Entity)
        {
            context.Entry<Personel>(Entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var personel = context.Personels.FirstOrDefault(p => p.PersonelId == id);
            if (personel != null)
            {
                context.Personels.Remove(personel);
            }
        }
        
        public IQueryable<Personel> GetByDepartment(string department)
        {
            return context.Personels.Where(p => p.Department.DepartmentName == department);
        }

        public List<Personel> GetByIds(int[] Ids)
        {
            return Ids.Select(id => context.Personels.FirstOrDefault(p => p.PersonelId == id)).ToList();
        }

        public IQueryable<Personel> GetByDepartmentAndProject(string department, int projectId)
        {
            return context.Personels.Where(p => p.Department.DepartmentName == department && p.Projects.Any(a=>a.ProjectId==projectId));
        }

        public void Dismiss(int personelId)
        {
            var personel = context.Personels.Include(p=>p.Projects).FirstOrDefault(p => p.PersonelId == personelId);
            personel.Status = false;
            personel.Projects = null;
            personel.DismissalDate=DateTime.Now;

        }

        public IQueryable<Personel> getDismissPersonels()
        {
            return context.Personels.Where(p => p.Status == false);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
