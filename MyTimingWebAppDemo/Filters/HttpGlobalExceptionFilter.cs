using log4net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyTimingWebAppDemo.Filters
{
    /// <summary>
    /// 捕捉Action异常类
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILog _log = LogManager.GetLogger(Startup.LoggerRepository.Name, typeof(HttpGlobalExceptionFilter));
        public void OnException(ExceptionContext context)
        {
            _log.Error(context?.Exception);
        }
    }
}