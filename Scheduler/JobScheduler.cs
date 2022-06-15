/*using MessagingService.Data;
using Quartz;
using Quartz.Impl;

namespace MessagingService.Scheduler
{
    public class JobScheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            
            JobKey jobkey = new JobKey("trigger1", "group1");

            if (await scheduler.CheckExists(jobkey))
            {
                await scheduler.DeleteJob(jobkey);
            }

            IJobDetail job = JobBuilder.Create<SendMailJob>().Build();

            TriggerKey triggerKey = new TriggerKey("trigger1, group1");




            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .StartNow()
                .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(45)
                .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
*/