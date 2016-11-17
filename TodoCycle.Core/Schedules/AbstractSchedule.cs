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
        public static AbstractSchedule Parse(string scheduleJson)
        {
            dynamic json = JsonConvert.DeserializeObject(scheduleJson);
            string scheduleType = json.ScheduleType.Value.ToUpper();

            switch (scheduleType)
            {
                case "DAILY":
                    // long => int
                    return new DailySchedule((int)json.EveryNDays.Value);
                case "WEEKLY":
                    string rawDays = json.DaysOfWeek.Value;
                    var days = rawDays.Split(new char[] { ',' }).Select(n => (DayOfWeek)int.Parse(n));
                    return new WeeklySchedule(days);
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
