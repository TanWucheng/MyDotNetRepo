namespace DaoLibrary.Entities
{
    public class ColumnInfo : EntityBase
    {
        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public string DataType { get; set; }

        public int DataLength { get; set; }

        public string ColumnComment { get; set; }
    }
}
