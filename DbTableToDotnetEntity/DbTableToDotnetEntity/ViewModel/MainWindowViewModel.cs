using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DbTableToDotnetEntity.Domain;
using DbTableToDotnetEntity.Widget;
using MaterialDesignThemes.Wpf;
using Color = System.Windows.Media.Color;

namespace DbTableToDotnetEntity.ViewModel
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            _isLoggedIn = false;
            _accountPopupBoxIcon = new PackIcon
            {
                Kind = PackIconKind.AccountRemove,
                Width = 24,
                Height = 24,
                Margin = new Thickness(4, 0, 4, 0),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };
        }

        /// <summary>
        /// 是否已登录
        /// </summary>
        private bool _isLoggedIn;

        private PackIcon _accountPopupBoxIcon;

        /// <summary>
        /// 是否已登录
        /// </summary>
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => this.MutateVerbose(ref _isLoggedIn, value, RaisePropertyChanged());
        }

        public PackIcon AccountPopupBoxIcon
        {
            get => _accountPopupBoxIcon;
            set => this.MutateVerbose(ref _accountPopupBoxIcon, value, RaisePropertyChanged());
        }

        /// <summary>
        /// 登录按钮点击处理Command
        /// </summary>
        public ICommand RunLoginDialogCommand => new AnotherCommandImplementation(ExecuteRunLoginDialog);

        /// <summary>
        /// 登出按钮点击处理Command
        /// </summary>
        public ICommand RunLogoutCommand => new AnotherCommandImplementation(async (o) =>
        {
            var view = new SimpleProgressDialog();
            var result = await DialogHost.Show(view, "RootDialog", LogoutOpenedEventHandler,
                LogoutClosingEventHandler);
        });

        /// <summary>
        /// 关于按钮点击处理Command
        /// </summary>
        public ICommand RunAboutCommand => new AnotherCommandImplementation(async (o) =>
          {
              var view = new AboutDialog();
              await DialogHost.Show(view, "RootDialog");
          });

        private async void ExecuteRunLoginDialog(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new LoginDialog()
            {
                DataContext = new LoginDialogViewModel()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", LoginOpenedEventHandler,
                LoginClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private void LoginOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            Console.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");
        }

        private void LoginClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            //OK, lets cancel the close...
            eventArgs.Cancel();

            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new SimpleProgressDialog());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            //lets run a fake operation for 3 seconds then close this baby.
            Task.Delay(TimeSpan.FromSeconds(1))
                .ContinueWith((t, _) =>
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
                    }, null,
                    TaskScheduler.FromCurrentSynchronizationContext());
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
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void LogoutClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("已经登出");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
