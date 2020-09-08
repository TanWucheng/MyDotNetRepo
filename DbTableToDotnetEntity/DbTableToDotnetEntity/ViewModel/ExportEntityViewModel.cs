using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DbTableToDotnetEntity.Domain;
using DbTableToDotnetEntity.Models;
using DbTableToDotnetEntity.Service.Interface;
using DbTableToDotnetEntity.Widget;
using MaterialDesignThemes.Wpf;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DbTableToDotnetEntity.ViewModel
{
    internal class ExportEntityViewModel : INotifyPropertyChanged, IValidationExceptionHandler
    {
        private ObservableCollection<TableInfoModel> _tableInfoItems;
        private ObservableCollection<DbServerInfo> _dbServerHistoryItems;
        private ObservableCollection<ColumnInfo> _columnInfoItems;

        private bool _isSelectAll;
        private bool _isDialogOpened;

        private ICommand _selectCommand;
        private ICommand _selectAllCommand;
        private ICommand _runGenEntityCommand;
        private ICommand _runFindFilePathCommand;
        private ICommand _runGenFileCommand;

        private DbServerInfo _currentDbServerInfoItem;

        private string _encoding;
        private string _dbType;
        private string _nameSpace;
        private string _filePath;

        private readonly ITableInfoService _tableInfoService;
        private readonly IColumnInfoService _columnInfoService;

        public ExportEntityViewModel(ITableInfoService tableInfoService, IColumnInfoService columnInfoService)
        {
            _tableInfoService = tableInfoService;
            _columnInfoService = columnInfoService;

            _dbServerHistoryItems = DbServerInfoFile.GetDbServerInfoHistoryItems();

            _currentDbServerInfoItem = new DbServerInfo();
        }

        public ObservableCollection<TableInfoModel> TableInfoItems
        {
            get => _tableInfoItems;
            set
            {
                _tableInfoItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TableInfoItems)));
            }
        }

        public ObservableCollection<ColumnInfo> ColumnInfoItems
        {
            get => _columnInfoItems;
            set
            {
                _columnInfoItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColumnInfoItems)));
            }
        }

        public ObservableCollection<DbServerInfo> DbServerHistoryItems
        {
            get => _dbServerHistoryItems;
            set => this.MutateVerbose(ref _dbServerHistoryItems, value, RaisePropertyChanged());
        }

        public bool IsSelectAll
        {
            get => _isSelectAll;
            set => this.MutateVerbose(ref _isSelectAll, value, RaisePropertyChanged());
        }

        public bool IsDialogOpened
        {
            get => _isDialogOpened;
            set => this.MutateVerbose(ref _isDialogOpened, value, RaisePropertyChanged());
        }

        public IEnumerable<string> Encodings
        {
            get
            {
                yield return "UTF7";
                yield return "UTF8";
                yield return "UTF16";
                yield return "Unicode";
                yield return "Windows1252";
                yield return "iso88591";
                yield return "iso88593";
                yield return "iso885915";
                yield return "macroman";
                yield return "gbk";
            }
        }

        public IEnumerable<string> DbTypes
        {
            get
            {
                yield return "MySQL";
                yield return "Microsoft SQL Server";
                yield return "Oracle";
            }
        }

        public ICommand SelectAllCommand => _selectAllCommand ??= new DelegateCommand<object>(SelectAll);

        public ICommand SelectCommand => _selectCommand ??= new DelegateCommand<int>(Select);

        public ICommand RunGetTableInfoCommand => new AnotherCommandImplementation(async (o) =>
        {
            if (!IsValid)
            {
                ShowSnackBarMsg("输入有误，请检查！", SnackBarMessageType.Warning);
                return;
            }
            var result = GetConnectionStr(out var connStr);
            if (!result) return;
            var count = (from item in DbServerHistoryItems
                         where (item.Ip.Equals(CurrentDbServerInfoItem.Ip) &&
                                 item.Database.Equals(CurrentDbServerInfoItem.Database))
                         select item).Count();
            if (count <= 0)
            {
                DbServerHistoryItems.Add(CurrentDbServerInfoItem);
                DbServerInfoFile.WriteHistoryDataDirect(connStr);
            }

            var view = new SimpleProgressDialog();
            await DialogHost.Show(view, "RootDialog", DialogOpenedEventHandler,
                null);
        });

        public ICommand RunGenEntityCommand => _runGenEntityCommand ??= new DelegateCommand<string>(GenEntity);

        public ICommand RunFindFilePathCommand => _runFindFilePathCommand ??= new DelegateCommand<object>(FindFilePath);

        public ICommand RunGenFileCommand => _runGenFileCommand ??= new DelegateCommand<object>(GenFile);

        public DbServerInfo CurrentDbServerInfoItem
        {
            get => _currentDbServerInfoItem;
            set => this.MutateVerbose(ref _currentDbServerInfoItem, value, RaisePropertyChanged());
        }

        public string Encoding
        {
            get => _encoding;
            set => this.MutateVerbose(ref _encoding, value, RaisePropertyChanged());
        }

        public string DbType
        {
            get => _dbType;
            set => this.MutateVerbose(ref _dbType, value, RaisePropertyChanged());
        }

        public string NameSpace
        {
            get => _nameSpace;
            set => this.MutateVerbose(ref _nameSpace, value, RaisePropertyChanged());
        }

        public string FilePath
        {
            get => _filePath;
            set => this.MutateVerbose(ref _filePath, value, RaisePropertyChanged());
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="id"></param>
        public void SelectAll(object id)
        {
            foreach (var item in _tableInfoItems)
            {
                item.IsSelected = IsSelectAll;
            }
        }

        /// <summary>
        /// 单选
        /// </summary>
        /// <param name="id"></param>
        public void Select(int id)
        {
            var selectedItems = _tableInfoItems.FirstOrDefault(p => p.TableInfo.Id == id);
            if (selectedItems == null) return;
            if (!selectedItems.IsSelected && IsSelectAll)
            {
                IsSelectAll = false;
            }
            else if (selectedItems.IsSelected && !IsSelectAll)
            {
                if (_tableInfoItems.Any(item => !item.IsSelected))
                {
                    return;
                }

                IsSelectAll = true;
            }
        }

        public async void GenEntity(string tableName)
        {
            var list = await _columnInfoService.GetColumnInfoAsync("ten_db", tableName);
            ColumnInfoItems = new ObservableCollection<ColumnInfo>(list);
            IsDialogOpened = true;
        }

        public void FindFilePath(object obj)
        {
            var dlg = new CommonOpenFileDialog
            {
                Title = "选择文件保存的路径",
                IsFolderPicker = true,
                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = true,
                EnsureValidNames = true,
                Multiselect = false,
                ShowPlacesList = true
            };
            if (!string.IsNullOrWhiteSpace(FilePath))
            {
                dlg.InitialDirectory = FilePath;
            }
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FilePath = dlg.FileName;
            }
        }

        public async void GenFile(object obj)
        {
            var tableName = ColumnInfoItems[0].TableName;
            try
            {
                if (string.IsNullOrEmpty(NameSpace))
                {
                    ShowSnackBarMsg("请输入Model相关文件的命名空间!", SnackBarMessageType.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(FilePath))
                {
                    ShowSnackBarMsg("请输入文件放置路径!", SnackBarMessageType.Warning);
                    return;
                }

                var result = await CsCodeFile.CreateEntityClass(this, tableName);

                if (!result)
                    return;

                //var packIcon = new PackIcon
                //{
                //    Kind = PackIconKind.CheckboxMarkedCircleOutline
                //};
                //button.Content = packIcon;

                if (MessageBox.Show("生成文件成功，是否打开文件夹?", "Tips", MessageBoxButton.OKCancel) !=
                    MessageBoxResult.OK) return;
                var path = FilePath;
                System.Diagnostics.Process.Start("explorer.exe", path);
            }
            catch (Exception ex)
            {
                ShowSnackBarMsg("生成Entity文件发生异常:" + ex.Message, SnackBarMessageType.Error);
            }
        }

        private bool GetConnectionStr(out string connStr)
        {
            connStr = string.Empty;

            var server = CurrentDbServerInfoItem.Ip;
            var database = CurrentDbServerInfoItem.Database;
            var portNum = CurrentDbServerInfoItem.TcpIpPort;
            var uid = CurrentDbServerInfoItem.UserId;
            var dbType = CurrentDbServerInfoItem.DatabaseType;

            // 已由ValidationExceptionBehavior来处理验证错误
            /*if (string.IsNullOrEmpty(server))
            {
                ShowSnackBarMsg("请输入服务器名或者IP地址!", SnackBarMessageType.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(database))
            {
                ShowSnackBarMsg("请输入数据库名称!", SnackBarMessageType.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(portNum))
            {
                ShowSnackBarMsg("请输入服务器端口号!", SnackBarMessageType.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(uid))
            {
                ShowSnackBarMsg("请输入数据库登陆用户名!", SnackBarMessageType.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(dbType))
            {
                ShowSnackBarMsg("请选择数据类型!", SnackBarMessageType.Warning);
                return false;
            }*/

            var pwd = CurrentDbServerInfoItem.Password;
            if (string.IsNullOrEmpty(pwd))
            {
                ShowSnackBarMsg("请输入数据库登陆用户密码!", SnackBarMessageType.Warning);
                return false;
            }

            var encode = CurrentDbServerInfoItem.Encoding;
            if (string.IsNullOrEmpty(encode))
            {
                encode = "UTF8";
            }

            connStr = $"database={database};data source={server};user id={uid};password={pwd};pooling=false;charset={encode};port={portNum};dbType={dbType}";
            return true;
        }

        private static void ShowSnackBarMsg(string message, SnackBarMessageType messageType)
        {
            MainWindowSnackBarMessage.Show(message, messageType);
        }

        private async void DialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            var list = await _tableInfoService.GetTableInfoAsync("ten_db");
            var modelList = (from tableInfo in list
                             select new TableInfoModel() { TableInfo = tableInfo, IsSelected = false });
            var items = new ObservableCollection<TableInfoModel>(modelList);
            TableInfoItems = items;

            DialogHost.CloseDialogCommand.Execute(null, null);

            ShowSnackBarMsg("获取成功", SnackBarMessageType.Success);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }

        public bool IsValid { get; set; }
    }
}
