using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DbTableToDotnetEntity.Domain;
using DbTableToDotnetEntity.Models;
using DbTableToDotnetEntity.Service.Interface;
using DbTableToDotnetEntity.UserControls;
using DbTableToDotnetEntity.Widget;
using MaterialDesignThemes.Wpf;

namespace DbTableToDotnetEntity.ViewModel
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     抽屉导航栏所有项目
        /// </summary>
        private readonly ObservableCollection<NavigationItem> _allItems;

        /// <summary>
        ///     存储过程sp_query_column_info服务
        /// </summary>
        private readonly IColumnInfoService _columnInfoService;

        /// <summary>
        ///     存储过程sp_query_table_info服务
        /// </summary>
        private readonly ITableInfoService _tableInfoService;

        /// <summary>
        ///     用户表服务
        /// </summary>
        private readonly IUsersService _usersService;

        /// <summary>
        ///     登录下拉框按钮图标
        /// </summary>
        private PackIcon _accountPopupBoxIcon;

        /// <summary>
        ///     是否已登录
        /// </summary>
        private bool _isLoggedIn;

        /// <summary>
        ///     经过筛选的抽屉导航栏项目
        /// </summary>
        private ObservableCollection<NavigationItem> _navItems;

        /// <summary>
        ///     抽屉导航栏搜索关键词
        /// </summary>
        private string _searchKeyword;

        /// <summary>
        ///     选中的抽屉导航栏项目的索引
        /// </summary>
        private int _selectedIndex;

        /// <summary>
        ///     选中的抽屉导航栏项目
        /// </summary>
        private NavigationItem _selectedItem;

        public MainWindowViewModel(IUsersService usersService,
            ITableInfoService tableInfoService,
            IColumnInfoService columnInfoService)
        {
            _usersService = usersService;
            _tableInfoService = tableInfoService;
            _columnInfoService = columnInfoService;

            _isLoggedIn = false;
            _accountPopupBoxIcon = new PackIcon
            {
                Kind = PackIconKind.AccountRemove,
                Width = 24,
                Height = 24,
                Margin = new Thickness(4, 0, 4, 0),
                Foreground = new SolidColorBrush(Color.FromRgb(244, 67, 54))
            };

            _allItems = GenerateNavItems();
            FilterItems(null);

            _selectedItem = _navItems[0];

            MovePrevCommand = new AnotherCommandImplementation(
                _ => SelectedIndex--,
                _ => SelectedIndex > 0);

            MoveNextCommand = new AnotherCommandImplementation(
                _ => SelectedIndex++,
                _ => SelectedIndex < _allItems.Count - 1);
        }

        public AnotherCommandImplementation MovePrevCommand { get; }

        public AnotherCommandImplementation MoveNextCommand { get; }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedIndex)));
            }
        }

        public NavigationItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (value == null || value.Equals(_selectedItem)) return;

                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                _searchKeyword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NavItems)));
                FilterItems(_searchKeyword);
            }
        }

        /// <summary>
        ///     是否已登录
        /// </summary>
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => this.MutateVerbose(ref _isLoggedIn, value, RaisePropertyChanged());
        }

        public ObservableCollection<NavigationItem> NavItems
        {
            get => _navItems;
            set
            {
                _navItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NavItems)));
            }
        }

        public PackIcon AccountPopupBoxIcon
        {
            get => _accountPopupBoxIcon;
            set => this.MutateVerbose(ref _accountPopupBoxIcon, value, RaisePropertyChanged());
        }

        /// <summary>
        ///     登录按钮点击处理Command
        /// </summary>
        public ICommand RunLoginDialogCommand => new AnotherCommandImplementation(ExecuteRunLoginDialog);

        /// <summary>
        ///     登出按钮点击处理Command
        /// </summary>
        public ICommand RunLogoutCommand => new AnotherCommandImplementation(async o =>
        {
            var view = new SimpleProgressDialog();
            await DialogHost.Show(view, "RootDialog", LogoutOpenedEventHandler,
                null);
        });

        /// <summary>
        ///     关于按钮点击处理Command
        /// </summary>
        public ICommand RunAboutCommand => new AnotherCommandImplementation(async o =>
        {
            var view = new AboutDialog();
            await DialogHost.Show(view, "RootDialog");
        });

        public event PropertyChangedEventHandler PropertyChanged;

        private async void ExecuteRunLoginDialog(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new LoginDialog
            {
                DataContext = new LoginDialogViewModel()
            };

            //show the dialog
            await DialogHost.Show(view, "RootDialog", null,
                      LoginClosingEventHandler);
        }

        private async void LoginClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            //OK, lets cancel the close...
            eventArgs.Cancel();

            if (!(eventArgs.Session.Content is LoginDialog dialog)) return;
            if (!(dialog.DataContext is LoginDialogViewModel context)) return;

            if (!context.IsValid)
            {
                context.WarningMsg = "请输入用户名！";
                context.Visibility = Visibility.Visible;
                return;
            }

            if (string.IsNullOrWhiteSpace(context.Password))
            {
                context.WarningMsg = "请输入密码!";
                context.Visibility = Visibility.Visible;
                return;
            }

            eventArgs.Session.UpdateContent(new SimpleProgressDialog());

            await Task.Delay(1000);

            var result = await DoLogin(context);

            if (result)
            {
                eventArgs.Session.Close(false);

                IsLoggedIn = true;
                AccountPopupBoxIcon = new PackIcon
                {
                    Kind = PackIconKind.AccountCheck,
                    Width = 24,
                    Height = 24,
                    Margin = new Thickness(4, 0, 4, 0),
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
                };

                ShowSnackBarMsg("登陆成功", SnackBarMessageType.Success);
            }
            else
            {
                context.WarningMsg = "登录失败，请检查输入！";
                context.Visibility = Visibility.Visible;
                eventArgs.Session.UpdateContent(new LoginDialog
                {
                    DataContext = context
                });
            }
        }

        private async Task<bool> DoLogin(LoginDialogViewModel viewModel)
        {
            var searchPredicate = PredicateBuilder.True<Users>();
            var password = Encryption.Md5(viewModel.Password).ToLower();
            searchPredicate = searchPredicate.And(x => x.Name.Equals(viewModel.Name))
                .And(x => x.Password.Equals(password));
            var list = await _usersService.GetUserList(searchPredicate);
            return list.Any();
        }

        private async void LogoutOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            await Task.Delay(1000);
            IsLoggedIn = false;
            AccountPopupBoxIcon = new PackIcon
            {
                Kind = PackIconKind.AccountRemove,
                Width = 24,
                Height = 24,
                Margin = new Thickness(4, 0, 4, 0),
                Foreground = new SolidColorBrush(Color.FromRgb(244, 67, 54))
            };
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private ObservableCollection<NavigationItem> GenerateNavItems()
        {
            return new ObservableCollection<NavigationItem>
            {
                new NavigationItem("主页", new Home())
                {
                    HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto,
                    VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                },
                new NavigationItem("生成Entity", new ExportEntity(_tableInfoService, _columnInfoService))
                {
                    HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto,
                    VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                }
            };
        }

        private void FilterItems(string keyword)
        {
            var filteredItems =
                string.IsNullOrWhiteSpace(keyword)
                    ? _allItems
                    : _allItems.Where(i => i.Name.ToLower().Contains(keyword.ToLower()));

            NavItems = new ObservableCollection<NavigationItem>(filteredItems);
        }

        private static void ShowSnackBarMsg(string message, SnackBarMessageType messageType)
        {
            MainWindowSnackBarMessage.Show(message, messageType);
        }

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}