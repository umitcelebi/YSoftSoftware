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
    public class EfProjectRepository:IProjectRepository
    {
        public YSoftContext context { get; set; }

        public EfProjectRepository(YSoftContext _context)
        {
            context = _context;
        }

        public Project GetById(int id)
        {
            return context.Projects.Include(p=>p.Personels).FirstOrDefault(p => p.ProjectId == id);
        }

        public IQueryable<Project> GetAll()
        {
            return context.Projects;
        }

        public void Add(Project Entity)
        {
            context.Projects.Add(Entity);
        }

        public void Update(Project Entity)
        {
            var project = context.Projects.Include(p=>p.Personels).FirstOrDefault(p=>p.ProjectId==Entity.ProjectId);

            project.Status = Entity.Status;
            project.ProjectName = Entity.ProjectName;
            project.MinPersonel = Entity.MinPersonel;
            project.MaxPersonel = Entity.MaxPersonel;
            project.Personels = Entity.Personels;

        }

        public void Delete(int id)
        {
            var project = context.Projects.FirstOrDefault(p => p.ProjectId == id);
            if (project != null)
            {
                context.Projects.Remove(project);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public IQueryable<Project> SearchProject(string q)
        {
            return context.Projects
                .Include(p => p.Personels)
                .Where(p => p.ProjectName.Contains(q) || p.Personels.Any(a => a.Name.Contains(q)));
        }
    }
}
