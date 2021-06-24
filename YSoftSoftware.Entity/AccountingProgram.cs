using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSoftSoftware.Entity
{
    public class AccountingProgram
    {
        public int AccountingProgramId { get; set; }
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }

        public List<Personel> Personels { get; set; }
    }
}
