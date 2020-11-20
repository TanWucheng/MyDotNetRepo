namespace DaoLibrary.Entities
{
    /// <summary>
    /// AppSettings.json连接字符串配置项模型
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// Microsoft Sql Server Connection String
        /// </summary>
        public string SqlServer { get; set; }

        /// <summary>
        /// MySQL Connection String
        /// </summary>
        public string MySql { get; set; }
    }
}
