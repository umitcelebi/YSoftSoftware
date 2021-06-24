using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSoftSoftware.Entity
{
    public class Compensation
    {
        public int compensationId { get; set; }

        public int PersonelId { get; set; }
        public Personel Personel { get; set; }

        public decimal Money { get; set; }

    }
}
