using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoCycle.Core.Schedules
{
    public class DailySchedule : AbstractSchedule
    {
        private int everyNDays = 1;

        public DailySchedule(int everyNDays)
        {
            this.everyNDays = everyNDays;
        }

        public static bool IsDue(ScheduledTask task, DailySchedule schedule)
        {
            var start = task.StartDateUtc.HasValue ? task.StartDateUtc.Value : task.CreatedOnUtc;
            int elapsedDays = (int)Math.Floor((DateTime.UtcNow - start).TotalDays);
            // Special case: N % 1 == 0, so this is always true for dailies.
            return elapsedDays >= 0 && elapsedDays % schedule.everyNDays == 0;
        }
    }
}
