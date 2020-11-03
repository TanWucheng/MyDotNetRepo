using Microsoft.AspNetCore.Builder;

namespace EntityGeneratorWebApp.Config
{
    /// <summary>
    /// 400~599响应状态码处理扩展
    /// </summary>
    public static class ErrorHandlingExtension
    {
        /// <summary>
        /// 注册400~599响应状态码处理中间件
        /// </summary>
        /// <param name="builder">IApplicationBuilder</param>
        /// <returns></returns>
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
