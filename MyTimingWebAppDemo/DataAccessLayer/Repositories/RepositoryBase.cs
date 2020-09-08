using Microsoft.Extensions.Options;
using MyTimingWebAppDemo.Models;

namespace MyTimingWebAppDemo.DataAccessLayer.Repositories
{
    /// <summary>
    /// 仓库基类,用以统一注入AppSettings.json设定的连接字符串
    /// </summary>
    internal class RepositoryBase
    {
        protected readonly ConnectionStrings ConnectionStrings;

        protected RepositoryBase(IOptions<ConnectionStrings> connStrings)
        {
            ConnectionStrings = connStrings.Value;
        }
    }
}