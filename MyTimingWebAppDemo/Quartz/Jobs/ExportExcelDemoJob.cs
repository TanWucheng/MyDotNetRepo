using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using log4net;
using Microsoft.Extensions.Options;
using MyTimingWebAppDemo.Models;
using MyTimingWebAppDemo.Services;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using Quartz;

namespace MyTimingWebAppDemo.Quartz.Jobs
{
    internal class ExportExcelDemoJob : IJob
    {
        private readonly ILog _log;
        private readonly ExportFileAddress _exportFileAddress;

        private static readonly string[] MonthNames = { "一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月" };
        private static readonly string[] CompanyNames = { "Microsoft", "IBM", "Oracle", "Google", "Yahoo", "HP" };

        public ExportExcelDemoJob(IOptions<ExportFileAddress> options)
        {
            _log = LogManager.GetLogger(Startup.LoggerRepository.Name, typeof(ExportExcelDemoJob));
            _exportFileAddress = options.Value;
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                string reportTitle = "2013 年度五大公司实际情况与原计划的百分比";
                FileInfo file = new FileInfo($"{_exportFileAddress.ExportExcel}\\Excel图表测试.xlsx");
                using ExcelPackage package = new ExcelPackage(file);

                #region research  
                var worksheet = package.Workbook.Worksheets.Add("Data");
                DataTable dataPercent = GetDataPercent();
                //chart = Worksheet.Drawings.AddChart("ColumnStackedChart", eChartType.Line) as ExcelLineChart;   
                if (worksheet.Drawings.AddChart("ColumnStackedChart", eChartType.LineMarkers) is ExcelLineChart chart)
                {
                    chart.Legend.Position = eLegendPosition.Right;
                    chart.Legend.Add();
                    chart.Title.Text = reportTitle; // 设置图表的名称   
                    //chart.SetPosition (200, 50);// 设置图表位置   
                    chart.SetSize(800, 400); // 设置图表大小   
                    chart.ShowHiddenData = true;
                    //chart.YAxis.MinorUnit = 1;   
                    chart.XAxis.MinorUnit = 1; // 设置 X 轴的最小刻度   
                    //chart.DataLabel.ShowCategory = true;   
                    chart.DataLabel.ShowPercent = true; // 显示百分比   

                    // 设置月份   
                    for (int col = 1; col <= dataPercent.Columns.Count; col++)
                    {
                        worksheet.Cells[1, col].Value = dataPercent.Columns[col - 1].ColumnName;
                    }

                    // 设置数据   
                    for (int row = 1; row <= dataPercent.Rows.Count; row++)
                    {
                        for (int col = 1; col <= dataPercent.Columns.Count; col++)
                        {
                            string strValue = dataPercent.Rows[row - 1][col - 1].ToString();
                            if (col == 1)
                            {
                                worksheet.Cells[row + 1, col].Value = strValue;
                            }
                            else
                            {
                                double realValue = double.Parse(strValue);
                                worksheet.Cells[row + 1, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[row + 1, col].Style.Numberformat.Format = "#0\\.00%"; // 设置数据的格式为百分比   
                                worksheet.Cells[row + 1, col].Value = realValue;
                                if (realValue < 0.90d) // 如果小于 90% 则该单元格底色显示为红色   
                                {
                                    worksheet.Cells[row + 1, col].Style.Fill.BackgroundColor.SetColor(Color.Red);
                                }
                                else if (realValue >= 0.90d && realValue <= 0.95d) // 如果在 90% 与 95% 之间则该单元格底色显示为黄色   
                                {
                                    worksheet.Cells[row + 1, col].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                }
                                else
                                {
                                    worksheet.Cells[row + 1, col].Style.Fill.BackgroundColor
                                        .SetColor(Color.Green); // 如果大于 95% 则该单元格底色显示为绿色   
                                }
                            }
                        }

                        //chartSerie = chart.Series.Add(worksheet.Cells["A2:M2"], worksheet.Cells["B1:M1"]);   
                        //chartSerie.HeaderAddress = worksheet.Cells["A2"];   
                        //chart.Series.Add () 方法所需参数为：chart.Series.Add (X 轴数据区，Y 轴数据区)   
                        ExcelChartSerie chartSerie = chart.Series.Add(
                            worksheet.Cells[row + 1, 2, row + 1, 2 + dataPercent.Columns.Count - 2],
                            worksheet.Cells["B1:M1"]);
                        chartSerie.HeaderAddress = worksheet.Cells[row + 1, 1]; // 设置每条线的名称   
                    }

                    // 因为假定每家公司至少完成了 80% 以上，所以这里设置 Y 轴的最小刻度为 80%，这样使图表上的折线更清晰   
                    chart.YAxis.MinValue = 0.8d;
                    //chart.SetPosition (200, 50);// 可以通过制定左上角坐标来设置图表位置   
                    // 通过指定图表左上角所在的行和列及对应偏移来指定图表位置   
                    // 这里 CompanyNames.Length + 1 及 3 分别表示行和列   
                    chart.SetPosition(CompanyNames.Length + 1, 10, 3, 20);
                }

                #endregion research 

                package.Save();// 保存文件   
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 生成数据，由于这一步不是主要逻辑，所以采用随机生成数据的方式，实际中可根据需要从数据库或其它数据源中读取需要的数据
        /// </summary>
        /// <returns></returns>
        private static DataTable GetDataPercent()
        {
            DataTable data = new DataTable();
            Random random = new Random();
            data.Columns.Add(new DataColumn("公司名", typeof(string)));
            foreach (string monthName in MonthNames)
            {
                data.Columns.Add(new DataColumn(monthName, typeof(double)));
            }
            // 每个公司每月的百分比表示完成的业绩与计划的百分比   
            foreach (var t in CompanyNames)
            {
                var row = data.NewRow();
                row[0] = t;
                for (int j = 1; j <= MonthNames.Length; j++)
                {
                    // 这里采用了随机生成数据，但假定每家公司至少完成了计划的 85% 以上   
                    row[j] = 0.85d + random.Next(0, 15) / 100d;
                }
                data.Rows.Add(row);
            }

            return data;
        }
    }
}