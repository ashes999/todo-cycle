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
    class WeeklyScheduleTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void IsDueIsTrueIfTaskRecursEveryDay(bool isDone)
        {
            var weekly = new WeeklySchedule(EveryDay);
            var task = new Core.Task();
            if (isDone)
            {
                task.DoneOnUtc = DateTime.UtcNow;
            }

            Assert.That(WeeklySchedule.IsDue(task, weekly), Is.EqualTo(true));
        }

        [Test]
        public void IsDueIsFalseIfTaskRecursOnDifferentDays()
        {
            var schedule = new WeeklySchedule(new DayOfWeek[] { DateTime.UtcNow.DayOfWeek == DayOfWeek.Tuesday ? DayOfWeek.Monday : DayOfWeek.Tuesday });
            var task = new Core.Task() { StartDateUtc = DateTime.UtcNow.AddDays(-1) };
            Assert.That(WeeklySchedule.IsDue(task, schedule), Is.EqualTo(false));
        }

        [Test]
        public void IsDueITrueIfUserMissedCompletingTheTask()
        {
            var weekdays = new WeeklySchedule(Weekdays);

            // Create/complete task on a Saturday
            var saturday = new DateTime(2016, 11, 5);
            var task = new Core.Task() { CreatedOnUtc = saturday, DoneOnUtc = saturday };

            // There were lots of saturdays/sundays between then and now. This should be due.
            Assert.That(WeeklySchedule.IsDue(task, weekdays), Is.EqualTo(true));
        }

        private readonly IEnumerable<DayOfWeek> EveryDay = new DayOfWeek[] { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday };
        private readonly IEnumerable<DayOfWeek> Weekdays = new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
        private readonly IEnumerable<DayOfWeek> Weekends = new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
        private readonly IEnumerable<DayOfWeek> SunnahDays = new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Thursday };
    }
}
