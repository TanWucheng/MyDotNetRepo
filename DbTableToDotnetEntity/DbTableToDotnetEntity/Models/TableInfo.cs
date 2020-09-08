using System.ComponentModel.DataAnnotations;

namespace DbTableToDotnetEntity.Models
{
    public class TableInfo : BaseModel
    {
        public string TableName { get; set; }

        public string EntityClassName { get; set; }

        public string TableComment { get; set; }
    }
}
