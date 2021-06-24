using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSoftSoftware.Entity;

namespace YSoftSoftware.Data.Abstract
{
    public interface IPersonelRepository:IGenericRepository<Personel>
    {
        IQueryable<Personel> GetByDepartment(string department);
        List<Personel> GetByIds(int[] Ids);
        IQueryable<Personel> GetByDepartmentAndProject(string department,int projectId);
        void Dismiss(int personelId);
        IQueryable<Personel> getDismissPersonels();
    }
}
