using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YSoftSoftware.Entity;

namespace YSoftSoftware.Data.Concrete.ef
{
    public class YSoftContext:DbContext
    {
        public YSoftContext(DbContextOptions<YSoftContext> options)
            :base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Personel> Personels { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<AccountingProgram> AccountingProgram { get; set; }
        public DbSet<Compensation> Compensations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Personel>(entity =>
            {
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.Phone)
                    .IsRequired()
                    .HasMaxLength(11);
                entity.Property(p => p.DepartmentId).IsRequired();
                
            });
            
        }
    }
}
