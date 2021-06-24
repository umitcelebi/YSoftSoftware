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
    public class EfDepartmentRepository:IDepartmentRepository
    {
        public YSoftContext context { get; set; }

        public EfDepartmentRepository(YSoftContext _context)
        {
            context = _context;
        }

        public Department GetById(int id)
        {
            return context.Departments.FirstOrDefault(d => d.DepartmentId == id);
        }

        public IQueryable<Department> GetAll()
        {
            return context.Departments;
        }

        public void Add(Department Entity)
        {
            context.Departments.Add(Entity);
        }

        public void Update(Department Entity)
        {
            context.Entry<Department>(Entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var department= context.Departments.FirstOrDefault(d => d.DepartmentId == id);

            if (department != null)
            {
                context.Departments.Remove(department);
            }

        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
