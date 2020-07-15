using System.IO;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyTimingWebAppDemo.DataAccessLayer.Repositories;
using MyTimingWebAppDemo.Filters;
using MyTimingWebAppDemo.Models;
using MyTimingWebAppDemo.Quartz;
using MyTimingWebAppDemo.Quartz.Jobs;
using MyTimingWebAppDemo.Services;

namespace MyTimingWebAppDemo
{
    public class Startup
    {
        public static ILoggerRepository LoggerRepository { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //加载log4net日志配置文件
            LoggerRepository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(LoggerRepository, new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //面向Action切面捕获全局异常
            services.AddControllers(configure => { configure.Filters.Add<HttpGlobalExceptionFilter>(); });
            //注入Options对象
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"))
                .Configure<ExportFileAddress>(Configuration.GetSection("ExportFileAddress"));
            //这里使用瞬时依赖注入
            services.AddTransient<HelloJob>();
            services.AddTransient<ExportExcelDemoJob>();
            services.AddTransient<ExportExcelFromDbDemoJob>();
            //这里使用单例依赖注入
            services.AddSingleton<QuartzStartup>();
            //注入业务模块
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IPersonService, PersonService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="lifetime"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //开启&关闭定时任务
            var quartz = app?.ApplicationServices.GetRequiredService<QuartzStartup>();
            if (quartz != null)
            {
                lifetime?.ApplicationStarted.Register(quartz.Start);
                lifetime?.ApplicationStopped.Register(quartz.Stop);
            }
        }
    }
}
