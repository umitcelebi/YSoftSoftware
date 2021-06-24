using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSoftSoftware.Entity;

namespace YSoftSoftware.WebUI.Models
{
    public sealed class CompensationCalculate
    {
        private static CompensationCalculate instance = null;
        private static readonly object padlock = new object();

        public CompensationCalculate()
        {
        }

        public static CompensationCalculate Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new CompensationCalculate();
                    }

                    return instance;
                }
            }
        }

        public static decimal Calculate(Personel personel)
        {
            int year = (personel.DismissalDate.Year - personel.StartDate.Year);
            int day = personel.DismissalDate.Subtract(personel.StartDate).Days%365;

            decimal money = year * personel.Salary + day * (personel.Salary / 30);
            money = money * (decimal)0.759;
            return money;
        }

    }
}
