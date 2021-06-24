using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSoftSoftware.Entity
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Project name cannot be empty!")]
        public string ProjectName { get; set; }

        [Range(minimum:1,maximum:20)]
        public int MinPersonel { get; set; }

        [Range(1,maximum: 20)]
        public int MaxPersonel { get; set; }

        public bool Status { get; set; }

        public List<Personel> Personels { get; set; }
    }
}
