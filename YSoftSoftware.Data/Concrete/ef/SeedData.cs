using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YSoftSoftware.Entity;

namespace YSoftSoftware.Data.Concrete.ef
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            YSoftContext context = app.ApplicationServices.GetRequiredService<YSoftContext>();
            context.Database.Migrate();

            if (!context.Departments.Any())
            {
                context.Departments.AddRange(
                    new Department()
                    {
                        DepartmentName = "Manager"
                    },
                    new Department()
                    {
                        DepartmentName = "Programmer"
                    },
                    new Department()
                    {
                        DepartmentName = "Analyst"
                    },
                    new Department()
                    {
                        DepartmentName = "Designer"
                    }
                    );
                context.SaveChanges();
            }

            if (!context.AccountingProgram.Any())
            {
                context.AccountingProgram.AddRange(
                    new AccountingProgram()
                    {
                        Name = "muhasebe1"
                    },
                    new AccountingProgram()
                    {
                        Name = "muhasebe2"
                    }
                    );
                context.SaveChanges();
            }

            if (!context.Projects.Any())
            {
                context.Projects.AddRange(
                    new Project()
                    {
                        ProjectName = "Proje1",
                        MinPersonel = 5,
                        MaxPersonel = 9,
                        Status = true
                    },
                    new Project()
                    {
                        ProjectName = "Proje2",
                        MinPersonel = 1,
                        MaxPersonel = 3,
                        Status = true
                    },
                    new Project()
                    {
                        ProjectName = "Proje3",
                        MinPersonel = 7,
                        MaxPersonel = 13,
                        Status = true
                    },
                    new Project()
                    {
                        ProjectName = "Proje4",
                        MinPersonel = 4,
                        MaxPersonel = 7,
                        Status = true
                    },
                    new Project()
                    {
                        ProjectName = "Proje5",
                        MinPersonel = 3,
                        MaxPersonel = 6,
                        Status = true
                    }
                    );
                context.SaveChanges();
            }

            if (!context.Personels.Any())
            {
                context.Personels.AddRange(
                    new Personel()
                    {
                        Name = "Umit",
                        Phone = "5344868787",
                        AccountingProgramId = 1,
                        DepartmentId = 3,
                        Salary = 8000,
                        Status = true,
                        StartDate = DateTime.Now.AddYears(-5).AddDays(-60)
                    },
                    new Personel()
                    {
                        Name = "Ahmet",
                        Phone = "5248758487",
                        AccountingProgramId = 1,
                        DepartmentId = 2,
                        Salary = 5000,
                        Status = true,
                        StartDate = DateTime.Now.AddYears(-2).AddDays(-40)
                    },
                    new Personel()
                    {
                        Name = "Ali",
                        Phone = "5344868787",
                        AccountingProgramId = 2,
                        DepartmentId = 1,
                        Salary = 5500,
                        Status = true,
                        StartDate = DateTime.Now.AddYears(-3).AddDays(-55)
                    },
                    new Personel()
                    {
                        Name = "Emin",
                        Phone = "5344868787",
                        AccountingProgramId = 2,
                        DepartmentId = 2,
                        Salary = 7500,
                        Status = true,
                        StartDate = DateTime.Now.AddYears(-4).AddDays(-28)
                    },
                    new Personel()
                    {
                        Name = "Kerem",
                        Phone = "5344868787",
                        AccountingProgramId = 1,
                        DepartmentId = 3,
                        Salary = 7000,
                        Status = true,
                        StartDate = DateTime.Now.AddYears(-4).AddDays(-33)
                    },
                    new Personel()
                    {
                        Name = "Emre",
                        Phone = "5344868787",
                        AccountingProgramId = 2,
                        DepartmentId = 2,
                        Salary = 4000,
                        Status = true,
                        StartDate = DateTime.Now.AddDays(-50).AddDays(-78)
                    },
                    new Personel()
                    {
                        Name = "Furkan",
                        Phone = "5344868787",
                        AccountingProgramId = 1,
                        DepartmentId = 3,
                        Salary = 8000,
                        Status = true,
                        StartDate = DateTime.Now.AddYears(-5).AddDays(-19)
                    }
                    );
                context.SaveChanges();
            }

        }
    }
}
