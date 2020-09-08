using System.Configuration;
using System.Windows;
using DbTableToDotnetEntity.Data;
using DbTableToDotnetEntity.Repository;
using DbTableToDotnetEntity.Repository.Interface;
using DbTableToDotnetEntity.Service;
using DbTableToDotnetEntity.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DbTableToDotnetEntity
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Init();
            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //窗口注入
            services.TryAddTransient<MainWindow>();
            services.TryAddTransient<TestWindow>();
            //ef core注入
            services.AddDbContext<EfCoreContext>(options =>
            {
                options.UseMySQL(ConfigurationManager.ConnectionStrings["MySQLConnection"].ToString());
            });
            //添加repository注入
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<ITableInfoRepository, TableInfoRepository>();
            services.AddTransient<IColumnInfoRepository, ColumnInfoRepository>();
            //添加service注入
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ITableInfoService, TableInfoService>();
            services.AddTransient<IColumnInfoService, ColumnInfoService>();
        }

        private void Init()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            services.BuildServiceProvider()
                .GetRequiredService<MainWindow>()
                .Show();
        }
    }
}
