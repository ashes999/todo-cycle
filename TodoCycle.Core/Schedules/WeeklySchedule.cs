using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoCycle.Core.Schedules
{
    public class WeeklySchedule : AbstractSchedule
    {
        private IEnumerable<DayOfWeek> daysOfWeek;

        public WeeklySchedule(IEnumerable<DayOfWeek> daysOfWeek)
        {
            this.daysOfWeek = daysOfWeek;
        }
        public static bool IsDue(ScheduledTask task, WeeklySchedule schedule)
        {
            var start = task.StartDateUtc.HasValue ? task.StartDateUtc.Value : task.CreatedOnUtc;
            return DateTime.UtcNow.Date >= start.Date && schedule.daysOfWeek.Contains(DateTime.UtcNow.DayOfWeek);
        }
    }
}
