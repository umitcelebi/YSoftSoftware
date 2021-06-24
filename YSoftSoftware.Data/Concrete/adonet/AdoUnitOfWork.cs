using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSoftSoftware.Data.Abstract;

namespace YSoftSoftware.Data.Concrete.adonet
{
    public class AdoUnitOfWork:IUnitOfWork
    {
        private IPersonelRepository _personelRepository;
        private IProjectRepository _projectRepository;
        private IAccountingProgramRepository _accountingProgram;
        private IDepartmentRepository _departmentRepository;
        private ICompensationRepository _compensationRepository;

        public AdoUnitOfWork()
        {}
        
        public IProjectRepository Projects
        {
            get
            {
                return _projectRepository ?? (_projectRepository = new AdoProjectRepository());
            }

        }
        public IPersonelRepository Personels
        {
            get
            {
                return _personelRepository ?? (_personelRepository = new AdoPersonelRepository());
            }
        }

        public IAccountingProgramRepository Programs
        {
            get
            {
                return _accountingProgram ?? (_accountingProgram = new AdoAccountingProgramRepository());
            }
        }

        public IDepartmentRepository Departments
        {
            get
            {
                return _departmentRepository ?? (_departmentRepository = new AdoDepartmentRepository());
            }
        }

        public ICompensationRepository Compensations
        {
            get
            {
                return _compensationRepository ?? (_compensationRepository = new AdoCompensationRepository());
            }
        }
        public int SaveChanges()
        {
            return 0;
        }
        public void Dispose()
        {
        }
    }
}
