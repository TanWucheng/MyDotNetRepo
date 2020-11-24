using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using MyTimingWebAppDemo.Models;
using OfficeOpenXml;

namespace MyTimingWebAppDemo.Utils
{
    internal static class FillWorksheetUtil<T> where T : IEntityBase
    {
        private static readonly ILog Logger;

        static FillWorksheetUtil()
        {
            Logger ??= LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
        }

        /// <summary>
        ///     填充worksheet,根据给定属性名称清单
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="list"></param>
        /// <param name="rowHeads"></param>
        /// <returns></returns>
        public static bool FillWorksheet(ExcelWorksheet worksheet, IEnumerable<T> list, List<RowTitleInfo> rowHeads)
        {
            var result = FillHead(worksheet, rowHeads);
            if (!result)
            {
                Logger.Info("填充列头信息失败");
                return false;
            }

            FillRows(list, worksheet, rowHeads);
            return true;
        }

        /// <summary>
        ///     填充worksheet,根据Entity自身属性名称清单
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool FillWorksheet(ExcelWorksheet worksheet, IEnumerable<T> list)
        {
            var entities = list as T[] ?? list.ToArray();
            var keyTypeDic = ReflectPropertyUtil<T>.ReflectGetPropertyKeyTypePairs(entities.FirstOrDefault());
            var rowHeads = (from typeCode in keyTypeDic select typeCode.Key).ToList();
            var result = FillHead(worksheet, rowHeads);
            if (!result)
            {
                Logger.Info("填充列头信息失败");
                return false;
            }

            FillRows(entities, worksheet, keyTypeDic, rowHeads);
            return true;
        }

        /// <summary>
        ///     填充除了表头之外每一行数据行,根据给定属性名称清单
        /// </summary>
        /// <param name="list"></param>
        /// <param name="worksheet"></param>
        /// <param name="rowHeads"></param>
        private static void FillRows(IEnumerable<T> list, ExcelWorksheet worksheet, List<RowTitleInfo> rowHeads)
        {
            var rowIndex = 2;
            var propertyList = (from row in rowHeads select row.AttrName).ToList();
            var entities = list as T[] ?? list.ToArray();
            var keyTypeDic =
                ReflectPropertyUtil<T>.ReflectGetPropertyKeyTypePairs(entities.FirstOrDefault(), propertyList);

            foreach (var keyValueDic in entities.Select(t =>
                ReflectPropertyUtil<T>.ReflectGetPropertyKeyValuePairs(t, propertyList)))
            {
                FillCells(keyValueDic, keyTypeDic, rowHeads, worksheet, rowIndex);
                rowIndex++;
            }
        }

        /// <summary>
        ///     填充除了表头之外每一行数据行,根据Entity自身属性名称清单
        /// </summary>
        /// <param name="list"></param>
        /// <param name="worksheet"></param>
        /// <param name="keyTypeDic"></param>
        /// <param name="rowHeads"></param>
        private static void FillRows(IEnumerable<T> list, ExcelWorksheet worksheet,
            Dictionary<string, TypeCode> keyTypeDic, List<string> rowHeads)
        {
            var rowIndex = 2;
            var entities = list as T[] ?? list.ToArray();
            foreach (var keyValueDic in entities.Select(ReflectPropertyUtil<T>.ReflectGetPropertyKeyValuePairs))
            {
                FillCells(keyValueDic, keyTypeDic, rowHeads, worksheet, rowIndex);
                rowIndex++;
            }
        }

        /// <summary>
        ///     填充列头行数据,根据给定属性名称清单
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="rowHeads"></param>
        /// <returns></returns>
        private static bool FillHead(ExcelWorksheet worksheet, IReadOnlyList<RowTitleInfo> rowHeads)
        {
            try
            {
                for (var i = 0; i < rowHeads.Count; i++) worksheet.Cells[1, i + 1].Value = rowHeads[i].AttrDesc;
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("填充列头出错", ex);
                return false;
            }
        }

        /// <summary>
        ///     填充列头行数据,根据Entity自身属性名称清单
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="rowHeads"></param>
        /// <returns></returns>
        private static bool FillHead(ExcelWorksheet worksheet, IReadOnlyList<string> rowHeads)
        {
            try
            {
                for (var i = 0; i < rowHeads.Count; i++) worksheet.Cells[1, i + 1].Value = rowHeads[i];
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("填充列头出错", ex);
                return false;
            }
        }

        /// <summary>
        ///     填充非列头行数据,使用dotnet属性类型代码,根据给定属性名称清单
        /// </summary>
        /// <param name="keyValueDic"></param>
        /// <param name="keyTypeDic"></param>
        /// <param name="rowHeads"></param>
        /// <param name="worksheet"></param>
        /// <param name="rowIndex"></param>
        private static void FillCells(Dictionary<string, object> keyValueDic,
            IReadOnlyDictionary<string, TypeCode> keyTypeDic, IReadOnlyList<RowTitleInfo> rowHeads,
            ExcelWorksheet worksheet, int rowIndex)
        {
            for (var cellIndex = 0; cellIndex < rowHeads.Count; cellIndex++)
                if (keyValueDic.TryGetValue(rowHeads[cellIndex].AttrName, out var obj) &&
                    keyTypeDic.TryGetValue(rowHeads[cellIndex].AttrName, out var propertyType))
                {
                    var value = obj?.ToString();
                    switch (propertyType)
                    {
                        case TypeCode.Decimal when !string.IsNullOrEmpty(value):
                        {
                            worksheet.Cells[rowIndex, cellIndex + 1].Value = TryParseUtil.TryParseDecimal(value, 0);
                            worksheet.Cells[rowIndex, cellIndex + 1].Style.Numberformat.Format = "0.00";
                            break;
                        }
                        case TypeCode.DateTime when !string.IsNullOrEmpty(value):
                        {
                            worksheet.Cells[rowIndex, cellIndex + 1].Value = TryParseUtil.TryParseDateTime(value);
                            worksheet.Cells[rowIndex, cellIndex + 1].Style.Numberformat.Format = "yyyy/m/d";
                            break;
                        }
                        case TypeCode.Int32 when !string.IsNullOrEmpty(value):
                        {
                            worksheet.Cells[rowIndex, cellIndex + 1].Value = TryParseUtil.TryParseInt32(value, 0);
                            worksheet.Cells[rowIndex, cellIndex + 1].Style.Numberformat.Format = "General";
                            break;
                        }
                        case TypeCode.Boolean when !string.IsNullOrEmpty(value):
                        {
                            worksheet.Cells[rowIndex, cellIndex + 1].Value =
                                TryParseUtil.TryParseBoolean(value) ? "是" : "否";
                            break;
                        }
                        default:
                        {
                            worksheet.Cells[rowIndex, cellIndex + 1].Value = value;
                            break;
                        }
                    }
                }
                else
                {
                    worksheet.Cells[rowIndex, cellIndex + 1].Value = string.Empty;
                }
        }

        /// <summary>
        ///     填充非列头行数据,使用dotnet属性类型代码,根据Entity自身属性名称清单
        /// </summary>
        /// <param name="keyValueDic"></param>
        /// <param name="keyTypeDic"></param>
        /// <param name="rowHeads"></param>
        /// <param name="worksheet"></param>
        /// <param name="rowIndex"></param>
        private static void FillCells(Dictionary<string, object> keyValueDic,
            IReadOnlyDictionary<string, TypeCode> keyTypeDic, IReadOnlyList<string> rowHeads, ExcelWorksheet worksheet,
            int rowIndex)
        {
            for (var cellIndex = 0; cellIndex < rowHeads.Count; cellIndex++)
                if (keyValueDic.TryGetValue(rowHeads[cellIndex], out var obj) &&
                    keyTypeDic.TryGetValue(rowHeads[cellIndex], out var propertyType))
                {
                    var value = obj?.ToString();
                    switch (propertyType)
                    {
                        case TypeCode.Decimal when !string.IsNullOrEmpty(value):
                        {
                            worksheet.Cells[rowIndex, cellIndex + 1].Value = TryParseUtil.TryParseDecimal(value, 0);
                            worksheet.Cells[rowIndex, cellIndex + 1].Style.Numberformat.Format = "0.00";
                            break;
                        }
                        case TypeCode.DateTime when !string.IsNullOrEmpty(value):
                        {
                            worksheet.Cells[rowIndex, cellIndex + 1].Value = TryParseUtil.TryParseDateTime(value);
                            worksheet.Cells[rowIndex, cellIndex + 1].Style.Numberformat.Format = "yyyy/m/d";
                            break;
                        }
                        case TypeCode.Int32 when !string.IsNullOrEmpty(value):
                        {
                            worksheet.Cells[rowIndex, cellIndex + 1].Value = TryParseUtil.TryParseInt32(value, 0);
                            worksheet.Cells[rowIndex, cellIndex + 1].Style.Numberformat.Format = "General";
                            break;
                        }
                        case TypeCode.Boolean when !string.IsNullOrEmpty(value):
                        {
                            worksheet.Cells[rowIndex, cellIndex + 1].Value =
                                TryParseUtil.TryParseBoolean(value) ? "是" : "否";
                            break;
                        }
                        default:
                        {
                            worksheet.Cells[rowIndex, cellIndex + 1].Value = value;
                            break;
                        }
                    }
                }
                else
                {
                    worksheet.Cells[rowIndex, cellIndex + 1].Value = string.Empty;
                }
        }
    }
}