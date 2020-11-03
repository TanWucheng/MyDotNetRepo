using System.Collections.Generic;
using System.Threading.Tasks;
using DaoLibrary.Entities;
using DaoLibrary.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EntityGeneratorWebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IMySqlRepository _mySqlRepository;
        private readonly ISqlServerRepository _sqlServerRepository;

        public DatabaseController(IMySqlRepository mySqlRepository, ISqlServerRepository sqlServerRepository)
        {
            _mySqlRepository = mySqlRepository;
            _sqlServerRepository = sqlServerRepository;
        }

        /// <summary>
        /// 获取MySQL表字段信息
        /// </summary>
        /// <param name="schemaName">数据库Schema</param>
        /// <param name="tableName">表名称</param>
        /// <returns>ColumnInfo清单</returns>
        [HttpGet("ColumnInfo/MySQL/{schemaName}/{tableName}")]
        [Authorize]
        public Task<IEnumerable<ColumnInfo>> GetMySqlColumnsInfo(string schemaName, string tableName)
        {
            return _mySqlRepository.GetColumnInfoAsync(schemaName, tableName);
        }

        /// <summary>
        /// 获取Microsoft Sql Server表字段信息
        /// </summary>
        /// <param name="schemaName">数据库Schema</param>
        /// <param name="tableName">表名称</param>
        /// <returns>ColumnInfo清单</returns>
        [HttpGet("GetMsSqlColumnsInfo")]
        public Task<IEnumerable<ColumnInfo>> GetMsSqlColumnsInfo(string schemaName, string tableName)
        {
            return _sqlServerRepository.GetColumnInfoAsync(schemaName, tableName);
        }
    }
}
