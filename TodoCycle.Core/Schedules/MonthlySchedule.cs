using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoCycle.Core.Schedules
{
    public class MonthlySchedule : AbstractSchedule
    {
        private int dayOfMonth = 1; // 1 = 1st

        public MonthlySchedule(int dayOfMonth)
        {
            this.dayOfMonth = dayOfMonth;
        }

        public override bool IsDue(Task task)
        {
            return false;
        }
    }
}
