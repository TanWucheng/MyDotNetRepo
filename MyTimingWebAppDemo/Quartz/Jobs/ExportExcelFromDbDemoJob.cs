using System;
using System.IO;
using System.Threading.Tasks;
using log4net;
using Microsoft.Extensions.Options;
using MyTimingWebAppDemo.Models;
using MyTimingWebAppDemo.Services;
using MyTimingWebAppDemo.Utils;
using OfficeOpenXml;
using Quartz;

namespace MyTimingWebAppDemo.Quartz.Jobs
{
    internal class ExportExcelFromDbDemoJob : IJob
    {
        private readonly ILog _log;
        private readonly ExportFileAddress _exportFileAddress;
        private readonly IPersonService _service;

        public ExportExcelFromDbDemoJob(IOptions<ExportFileAddress> options, IPersonService service)
        {
            _log = LogManager.GetLogger(Startup.LoggerRepository.Name, typeof(ExportExcelFromDbDemoJob));
            _exportFileAddress = options.Value;
            _service = service;
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                FileInfo fileInfo = new FileInfo($"{_exportFileAddress.ExportExcel}\\人员测试.xlsx");
                using ExcelPackage package = new ExcelPackage(fileInfo);
                var list = _service.GetAll();
                //var rowHeads = new List<RowTitleInfo>{
                //    new RowTitleInfo{N="Id",T="Id",P="int"},
                //    new RowTitleInfo{N="Name",T="名字",P="nvarchar"},
                //    new RowTitleInfo{N="Sex",T="性别",P="nvarchar"},
                //    new RowTitleInfo{N="Age",T="年龄",P="int"},
                //    new RowTitleInfo{N="Address",T="住址",P="nvarchar"},
                //    new RowTitleInfo{N="Email",T="电子邮件",P="nvarchar"},
                //    new RowTitleInfo{N="PhoneNum",T="电话号码",P="nvarchar"}
                //};
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("人员清单");
                worksheet.View.FreezePanes(2, 1);
                FillWorksheetUtil<PersonEntity>.FillWorksheet(worksheet, list);
                package.Save();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            return Task.CompletedTask;
        }
    }
}