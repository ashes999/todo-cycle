using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoCycle.Core.Schedules
{

    public abstract class AbstractSchedule
    {
        internal static AbstractSchedule Parse(string scheduleJson)
        {
            dynamic json = JsonConvert.DeserializeObject(scheduleJson);
            string scheduleType = json.ScheduleType.ToUpper();

            switch (scheduleType)
            {
                case "DAILY":
                    return new DailySchedule(json.EveryNDays);
                //case "WEEKLY":
                //    return new WeeklySchedule(json.DaysOfWeek);
                //case "MONTHLY":
                //    return new MonthlySchedule(json.DayOfMonth);
                //case "YEARLY":
                //    return new YearlySchedule(json.Month, json.DayOfMonth);
                default:
                    throw new InvalidOperationException(scheduleJson);
            }
        }
    }
}
