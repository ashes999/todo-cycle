using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoCycle.Core.Schedules;

namespace TodoCycle.Core
{
    public class ScheduledTask : Task
    {
        // Serialized to the DB as JSON
        public string ScheduleJson { get; set; }

        public DateTime? StartDateUtc { get; set; }

        public AbstractSchedule Schedule
        {
            get
            {
                return AbstractSchedule.Parse(this.ScheduleJson);
            }
            set
            {
                ScheduleJson = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            }
        }
    }
}
