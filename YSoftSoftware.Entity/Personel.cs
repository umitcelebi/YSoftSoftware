using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSoftSoftware.Entity
{
    public class Personel
    {
        public int PersonelId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DismissalDate { get; set; }
        public bool Status { get; set; }
        
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        
        public int? AccountingProgramId { get; set; }
        public AccountingProgram AccountingProgram { get; set; }
        
        public List<Project> Projects { get; set; }
        
        public Compensation Compensation { get; set; }

    }
}
