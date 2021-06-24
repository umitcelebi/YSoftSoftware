using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSoftSoftware.Entity
{
    public class Department
    {
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Department Name cannot eb empty")]
        public string DepartmentName { get; set; }
        public List<Personel> Personels { get; set; }
    }
}
