using System;
using log4net;
using MyTimingWebAppDemo.Quartz.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MyTimingWebAppDemo.Quartz
{
    /// <summary>
    /// Quartz定时任务启停工具
    /// </summary>
    public class QuartzStartup
    {
        private IScheduler _scheduler;
        private readonly ILog _log;

        public QuartzStartup(IServiceProvider serviceProvider)
        {
            _log = LogManager.GetLogger(Startup.LoggerRepository.Name, typeof(QuartzStartup));
            IJobFactory iocJobFactory = new IocQuartzJobFactory(serviceProvider);
            var schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler().Result;
            _scheduler.JobFactory = iocJobFactory;
        }

        /// <summary>
        /// 开始定时任务
        /// </summary>
        public void Start()
        {
            _log.Info("Schedule job load as application start.");
            _scheduler.Start().Wait();

            //var job1 = JobBuilder.Create<HelloJob>()
            //   .WithIdentity("HelloJob")
            //   .Build();
            //var trigger1 = TriggerBuilder.Create()
            //   .WithIdentity("HelloJobCron")
            //   .StartNow()
            //   .WithSimpleSchedule(x => x
            //       .WithIntervalInSeconds(3600)
            //       .RepeatForever())
            //   .Build();
            //_scheduler.ScheduleJob(job1, trigger1).Wait();
            //_scheduler.TriggerJob(new JobKey("HelloJob"));

            var job2 = JobBuilder.Create<ExportExcelDemoJob>()
              .WithIdentity("ExportExcelDemoJob")
              .Build();
            var trigger2 = TriggerBuilder.Create()
              .WithIdentity("ExportExcelDemoJobCron")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(3600)
                  .RepeatForever())
              .Build();
            _scheduler.ScheduleJob(job2, trigger2).Wait();
            _scheduler.TriggerJob(new JobKey("ExportExcelDemoJob"));

            // var job3 = JobBuilder.Create<ExportExcelFromDbDemoJob>()
            //    .WithIdentity("ExportExcelFromDbDemoJob")
            //    .Build();
            // var trigger3 = TriggerBuilder.Create()
            //   .WithIdentity("ExportExcelFromDbDemoJobCron")
            //   .StartNow()
            //   .WithSimpleSchedule(x => x
            //       .WithIntervalInSeconds(3600)
            //       .RepeatForever())
            //   .Build();
            // _scheduler.ScheduleJob(job3, trigger3).Wait();
            // _scheduler.TriggerJob(new JobKey("ExportExcelFromDbDemoJob"));
        }

        /// <summary>
        /// 停止定时任务
        /// </summary>
        public void Stop()
        {
            if (_scheduler == null)
            {
                return;
            }

            if (_scheduler.Shutdown(true).Wait(30000))
                _scheduler = null;

            _log.Info("Schedule job upload as application stopped");
        }
    }
}