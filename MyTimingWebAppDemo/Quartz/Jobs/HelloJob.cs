using System;
using System.Threading.Tasks;
using log4net;
using MyTimingWebAppDemo.DataAccessLayer.Services;
using Quartz;

namespace MyTimingWebAppDemo.Quartz.Jobs
{
    internal class HelloJob : IJob
    {
        private readonly ILog _log;
        private IPersonService _service;

        public HelloJob(IPersonService service)
        {
            _log = LogManager.GetLogger(Startup.LoggerRepository.Name, typeof(HelloJob));
            _service = service;
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                _log.Info($"Hello Task - {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                var list = _service.GetAll();
                foreach (var entity in list)
                {
                    _log.Info(entity.ToString());
                }
            }
            catch (Exception e)
            {
                _log.Error(e);
            }
            return Task.CompletedTask;
        }
    }
}