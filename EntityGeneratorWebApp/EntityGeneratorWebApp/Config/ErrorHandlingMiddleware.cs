using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EntityGeneratorWebApp.Config
{
    /// <summary>
    /// 400~599响应状态码处理中间件
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = context.Response.StatusCode;
                if (ex is ArgumentException)
                {
                    statusCode = 200;
                }
                await HandleExceptionAsync(context, statusCode, ex.Message);
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                var msg = "";
                switch (statusCode)
                {
                    case 401:
                        {
                            msg = $"<div style=\"display: flex;align-items: center;justify-content: center;text-align: justify;width: 100%;height: 100%;margin: 0 auto;\"><p style=\"color: #ee6e73\">您未登录此网站，禁止访问！<a href=\"../login/signin\" style=\"color: #1976D2\">请点击此处登陆</a></p></div>";
                            break;
                        }
                    case 404:
                        {
                            msg = $"<div style=\"display: flex;align-items: center;justify-content: center;text-align: justify;width: 100%;height: 100%;margin: 0 auto;\"><p style=\"color: #ee6e73\">未知请求！<a href=\"../home/index\" style=\"color: #1976D2\">请点击此处返回主页</a></p></div>";
                            break;
                        }
                    case 502:
                        msg = "请求错误";
                        break;
                    default:
                        {
                            if (statusCode != 200)
                            {
                                msg = "未知错误";
                            }

                            break;
                        }
                }
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    await HandleExceptionAsync(context, statusCode, msg);
                }
            }
        }

        /// <summary>
        /// 异常错误信息捕获，将错误信息用HTML方式返回
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="statusCode">Http Status Code</param>
        /// <param name="msg">Message</param>
        /// <returns></returns>
        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            // var result = JsonConvert.SerializeObject(new { Success = false, Msg = msg, Type = statusCode.ToString() });
            context.Response.ContentType = "text/html;charset=utf-8";
            return context.Response.WriteAsync(msg);
        }
    }
}
