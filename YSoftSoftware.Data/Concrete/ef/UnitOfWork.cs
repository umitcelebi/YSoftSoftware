using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSoftSoftware.Data.Abstract;

namespace YSoftSoftware.Data.Concrete.ef
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly YSoftContext context;
        private IPersonelRepository _personelRepository;
        private IProjectRepository _projectRepository;
        private IAccountingProgramRepository _accountingProgram;
        private IDepartmentRepository _departmentRepository;
        private ICompensationRepository _compensationRepository;

        public UnitOfWork(YSoftContext _context)
        {
            context = _context;
        }


        public IProjectRepository Projects
        {
            get
            {
                return _projectRepository??(_projectRepository=new EfProjectRepository(context));
            }
        }

        public IPersonelRepository Personels
        {
            get
            {
                return _personelRepository ?? (_personelRepository = new EfPersonelRepository(context));
            }
        }

        public IAccountingProgramRepository Programs
        {
            get
            {
                return _accountingProgram ?? (_accountingProgram = new EfAccountingProgramRepository(context));
            }
        }

        public IDepartmentRepository Departments
        {
            get
            {
                return _departmentRepository ?? (_departmentRepository = new EfDepartmentRepository(context));
            }
        }

        public ICompensationRepository Compensations
        {
            get
            {
                return _compensationRepository??(_compensationRepository=new EfCompensationRepository(context));
            }
        }

        public int SaveChanges()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
