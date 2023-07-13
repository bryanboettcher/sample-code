using System.Globalization;

namespace MaintWindowScheduling
{
    internal class Program
    {
        private static List<MaintenanceWindow> maintenanceWindows = new()
        {
            new(DateTime.Parse("2022-01-06T12:00:00"), DateTime.Parse("2022-01-07T08:45:00")),
            new(DateTime.Parse("2022-01-13T08:00:00"), DateTime.Parse("2022-01-14T08:45:00")),
            new(DateTime.Parse("2022-02-01T15:00:00"), DateTime.Parse("2022-02-02T06:15:00")),
            new(DateTime.Parse("2022-02-12T18:00:00"), DateTime.Parse("2022-02-14T11:45:00")),
            new(DateTime.Parse("2022-03-30T00:00:00"), DateTime.Parse("2022-04-01T00:00:00")),
        };

        static void Main(string[] args)
        {
            var reportsScheduled = new List<DateTime>
            {
                DateTime.Parse("2022-01-06T07:59:00"),
                DateTime.Parse("2022-01-13T08:12:00"),
                DateTime.Parse("2022-02-01T14:56:00"),
                DateTime.Parse("2022-02-14T13:43:00"),
                DateTime.Parse("2022-02-14T13:46:00"),
            };

            var preFudge = TimeSpan.FromHours(6);
            var postFudge = TimeSpan.FromHours(2);

            foreach (var target in reportsScheduled)
            {
                var isInWindow = maintenanceWindows.FirstOrDefault(w => w.IsInsideWindow(target, preFudge, postFudge));

                var actualStartTime = target;
                if (isInWindow is not null)
                {
                    actualStartTime = isInWindow.GetSafeTime(target, preFudge, postFudge);
                    Console.WriteLine($"The {target} job is inside a maintenance window!  The next time to run is: {actualStartTime}");
                }
                else
                {
                    Console.WriteLine($"The {target} job is not in a maintenance window and will run then.");
                }

                var schedule = new QuartzSchedule();
                schedule.ScheduleJob(actualStartTime);
            }
        }
    }

    internal record MaintenanceWindow(DateTime start, DateTime end)
    {
        public bool IsInsideWindow(DateTime target, TimeSpan startFudgeFactor, TimeSpan endFudgeFactor)
        {
            return (target + startFudgeFactor >= start && target <= end + endFudgeFactor);
        }

        public DateTime GetSafeTime(DateTime target, TimeSpan startFudgeFactor, TimeSpan endFudgeFactor)
        {
            if (!IsInsideWindow(target, startFudgeFactor, endFudgeFactor))
                return target;

            return end + endFudgeFactor;
        }
    }

    internal class QuartzSchedule
    {
        public void ScheduleJob(DateTime instant)
        {
            // this is the area where the JobDetails are built
            // the Schedule & Trigger are built
            // and the other Quartz.NET fluff happens

            Console.WriteLine(
                $"This would normally create the job inside of Quartz.NET to run on {instant.ToString(CultureInfo.CurrentCulture)}");
        }
    }
}