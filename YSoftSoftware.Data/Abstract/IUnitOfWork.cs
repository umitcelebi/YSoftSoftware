using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSoftSoftware.Data.Abstract
{
    public interface IUnitOfWork:IDisposable
    {
        IProjectRepository Projects { get; }
        IPersonelRepository Personels { get; }
        IAccountingProgramRepository Programs { get; }
        IDepartmentRepository Departments { get; }
        ICompensationRepository Compensations { get; }

        int SaveChanges();
    }
}
