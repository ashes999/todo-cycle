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
    class DailyScheduleTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void IsDueIsTrueIfTaskRecursDaily(bool isDone)
        {
            var daily = new DailySchedule(1);
            var task = new Core.ScheduledTask();
            if (isDone)
            {
                task.DoneOnUtc = DateTime.UtcNow;
            }

            Assert.That(DailySchedule.IsDue(task, daily), Is.EqualTo(true));
        }

        [Test]
        public void IsDueIsTrueOnTheNthDayOnly()
        {
            var everyOtherDay = new DailySchedule(2);

            // It was completed yesterday. It shouldn't be due until tomorrow.
            var task = new Core.ScheduledTask() { CreatedOnUtc = DateTime.UtcNow.AddDays(-1), DoneOnUtc = DateTime.UtcNow.AddDays(-1) };

            Assert.That(DailySchedule.IsDue(task, everyOtherDay), Is.EqualTo(false));

            // It was never done. We created it today, so it's due today.
            task.DoneOnUtc = null;
            task.CreatedOnUtc = DateTime.UtcNow;
            Assert.That(DailySchedule.IsDue(task, everyOtherDay), Is.EqualTo(true));
        }

        [Test]
        public void IsDueIsTrueIfTheTaskWasntDoneEvenOnNonNthDays()
        {
            var schedule = new DailySchedule(2);
            var task = new Core.ScheduledTask() { CreatedOnUtc = DateTime.UtcNow.AddDays(-20), DoneOnUtc = DateTime.UtcNow.AddDays(-14) };
            Assert.That(DailySchedule.IsDue(task, schedule), Is.EqualTo(true));
        }

        [Test]
        public void IsDueIsTrueIfYouMissedADay()
        {
            // Every 10 days.
            var schedule = new DailySchedule(10);
            // Created 20 days ago, completed 18 days ago, it was then due 10 days ago. 
            var task = new Core.ScheduledTask() { CreatedOnUtc = DateTime.UtcNow.AddDays(-20), DoneOnUtc = DateTime.UtcNow.AddDays(-18) };
            Assert.That(DailySchedule.IsDue(task, schedule), Is.EqualTo(true));
        }

        [Test]
        public void IsDueIsFalseIfStartDateIsInTheFuture()
        {
            var daily = new DailySchedule(1);
            var task = new Core.ScheduledTask() { StartDateUtc = DateTime.UtcNow.AddDays(1) };
            Assert.That(DailySchedule.IsDue(task, daily), Is.EqualTo(false));
        }
    }
}
