using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoCycle.Core.Schedules;

namespace TodoCycle.Core.UnitTests.Schedules
{
    [TestFixture]
    class AbstractScheduleTests
    {
        [Test]
        public void ParseReturnsDailyScheduleForDailyTask()
        {
            var json = "{ 'ScheduleType': 'Daily', 'EveryNDays': 3 }";
            var actual = AbstractSchedule.Parse(json);
            Assert.That(actual, Is.InstanceOf<DailySchedule>());
        }

        public void ParseReturnsWeeklyScheduleForWeeklyTask()
        {
            var json = "{ 'ScheduleType': 'Weekly', 'DaysOfWeek': [1, 3, 7] }";
            var actual = AbstractSchedule.Parse(json);
            Assert.That(actual, Is.InstanceOf<WeeklySchedule>());
        }
    }
}
