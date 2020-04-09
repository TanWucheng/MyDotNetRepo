using System;
using Quartz;
using Quartz.Spi;

namespace MyTimingWebAppDemo.Quartz
{
    ///<summary>
    ///IocQuartzJobFactory:实现在Timer触发的时候注入生成对应的 Job 组件
    ///</summary>
    public class IocQuartzJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public IocQuartzJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Called by the scheduler at the time of the trigger firing, in order to produce
        /// a Quartz.IJob instance on which to call Execute.
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle?.JobDetail;
            if (jobDetail != null)
            {
                return _serviceProvider.GetService(jobDetail.JobType) as IJob;
            }

            return null;
        }

        /// <summary>
        /// Allows the job factory to destroy/cleanup the job if needed.
        /// </summary>
        /// <param name="job"></param>
        public void ReturnJob(IJob job)
        {

        }
    }
}