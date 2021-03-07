using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DbTableToDotnetEntity.ViewModel;
using MySql.Data.MySqlClient;

namespace DbTableToDotnetEntity.Domain
{
    internal class CsCodeFile
    {
        public static async Task<bool> WriteFile(string folderName, string className, string fileSource, string extensionName, string filePath)
        {

            var path = filePath;
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            path = path + "\\" + folderName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var fileName = path + "\\" + className + extensionName;
            await using var writer = new StreamWriter(fileName);
            await writer.WriteAsync(fileSource);
            return true;
        }

        public static string GetFieldName(string fieldName)
        {
            var array = fieldName.Split('_');
            if (array.Length <= 0) return fieldName;
            var classFieldName = string.Empty;
            var builder = new StringBuilder();
            builder.Append(classFieldName);
            foreach (var t in array)
            {
                builder.Append(ToTitleCase(t));
            }
            classFieldName = builder.ToString();
            return classFieldName;

        }

        private static string ToTitleCase(string str)
        {
            return str.Substring(0, 1).ToUpper() + str[1..].ToLower();
        }

        public static string GetEntityClassName(string tableName)
        {
            var array = tableName.Split('_');
            if (array.Length <= 0) return ToTitleCase(tableName);
            var className = string.Empty;
            var builder = new StringBuilder();
            builder.Append(className);
            foreach (var t in array)
            {
                builder.Append(ToTitleCase(t));
            }
            className = builder.ToString();
            className += "Entity";
            return className;

        }

        public static async Task<bool> CreateEntityClass(string nameSpace, string connStr, string database, string[] tableNameArray, string filePath)
        {
            //nameSpace += ".Model";
            await using var connection = new MySqlConnection(connStr);
            connection.Open();
            var cmd = connection.CreateCommand();
            foreach (var tableName in tableNameArray)
            {
                cmd.CommandText = $"SELECT COLUMN_NAME,COLUMN_COMMENT,DATA_TYPE FROM information_schema.`COLUMNS` WHERE TABLE_NAME='{tableName}' AND TABLE_SCHEMA='{database}' ORDER BY ORDINAL_POSITION";
                var dataSet = new DataSet();
                using var adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dataSet);
                if (dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0) continue;
                var className = GetEntityClassName(tableName);
                var sb = new StringBuilder();
                sb.AppendLine("using System;");
                sb.AppendLine("");
                sb.AppendLine("namespace " + nameSpace);
                sb.AppendLine("{");
                sb.AppendLine("    public class " + className);
                sb.AppendLine("    {");
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    var colName = GetFieldName(row["COLUMN_NAME"].ToString()?.ToUpper());
                    var colType = GetDotNetTypeFormDbType(row["DATA_TYPE"].ToString());
                    var colComment = row["COLUMN_COMMENT"].ToString();
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine("        /// " + colComment);
                    sb.AppendLine("        /// </summary>");
                    if (colType.Equals("Int64?") && colName.Contains("Id"))
                    {
                        sb.AppendLine("        public virtual Int64 " + colName + "{ get; set; }");
                    }
                    else
                    {
                        sb.AppendLine("        public virtual " + colType + " " + colName + " { get; set; }");
                    }
                }
                sb.AppendLine("    }");
                sb.AppendLine("}");

                var result = await WriteFile("Model", className, sb.ToString(), ".cs", filePath);
                if (!result)
                {
                    return false;
                }
            }
            return true;
        }

        public static async Task<bool> CreateEntityClass(ExportEntityViewModel viewModel, string tableName)
        {
            var className = GetEntityClassName(tableName);
            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("");
            sb.AppendLine("namespace " + viewModel.NameSpace);
            sb.AppendLine("{");
            sb.AppendLine("    public class " + className);
            sb.AppendLine("    {");
            foreach (var item in viewModel.ColumnInfoItems)
            {
                var colName = GetFieldName(item.ColumnName.ToUpper());
                var colType = GetDotNetTypeFormDbType(item.DataType);
                var colComment = item.ColumnComment;
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// " + colComment);
                sb.AppendLine("        /// </summary>");
                if (colType.Equals("Int64?") && colName.Contains("Id"))
                {
                    sb.AppendLine("        public virtual Int64 " + colName + "{ get; set; }");
                }
                else
                {
                    sb.AppendLine("        public virtual " + colType + " " + colName + " { get; set; }");
                }
            }
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return await WriteFile("Model", className, sb.ToString(), ".cs", viewModel.FilePath);
        }

        private static string GetDotNetTypeFormDbType(string dbType)
        {
            switch (dbType.ToLower())
            {
                case "bit":
                    {
                        return "bool?";
                    }
                case "tinyint":
                    {
                        return "Byte?";
                    }
                case "smallint":
                    {
                        return "Int16?";
                    }
                case "mediumint":
                case "int":
                case "INTEGER":
                    {
                        return "Int32?";
                    }
                case "bigint":
                    {
                        return "Int64?";
                    }
                case "float":
                    {
                        return "float?";
                    }
                case "double":
                    {
                        return "double?";
                    }
                case "decimal":
                    {
                        return "decimal?";
                    }
                case "date":
                case "time":
                case "year":
                case "datetime":
                case "timestamp":
                    {
                        return "DateTime?";
                    }
                case "binary":
                case "varbinary":
                case "char":
                case "varchar":
                case "nchar":
                case "nvarchar":
                case "text":
                case "tinytext":
                case "mediumtext":
                case "longtext":
                case "blob":
                case "tinyblob":
                case "mediumblob":
                case "longblob":
                    {
                        return "string";
                    }
                default:
                    {
                        return "object?";
                    }
            }
        }
    }
}
