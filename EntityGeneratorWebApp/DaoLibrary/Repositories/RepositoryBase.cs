using DaoLibrary.Entities;
using Microsoft.Extensions.Options;

namespace DaoLibrary.Repositories
{
    /// <summary>
    /// 仓库基类,用以统一注入AppSettings.json设定的连接字符串
    /// </summary>
    public class RepositoryBase
    {
        protected readonly ConnectionStrings ConnectionStrings;

        protected RepositoryBase(IOptions<ConnectionStrings> connStrings)
        {
            ConnectionStrings = connStrings.Value;
        }
    }
}
