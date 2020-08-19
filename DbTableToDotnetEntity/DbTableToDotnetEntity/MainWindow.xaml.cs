using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using DbTableToDotnetEntity.Service.Interface;
using DbTableToDotnetEntity.ViewModel;
using MaterialDesignThemes.Wpf;

namespace DbTableToDotnetEntity
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static Snackbar SnackBar;

        private int _i;

        private Rect _normalRect;
        private WindowState _windowState = WindowState.Normal;

        public MainWindow(IUsersService usersService,
            ITableInfoService tableInfoService,
            IColumnInfoService columnInfoService)
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel(usersService, tableInfoService, columnInfoService);

            _i = 0;

            SnackBar = MainSnackBar;
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private void ButtonWinMin_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ButtonWinMax_OnClick(object sender, RoutedEventArgs e)
        {
            if (_windowState == WindowState.Normal)
            {
                var packIcon = new PackIcon
                {
                    Kind = PackIconKind.WindowRestore
                };
                ButtonWinMax.Content = packIcon;
                _normalRect = new Rect(Left, Top, Width, Height);
                Left = 0;
                Top = 0;
                var rect = SystemParameters.WorkArea;
                Width = rect.Width;
                Height = rect.Height;
                _windowState = WindowState.Maximized;
            }
            else if (_windowState == WindowState.Maximized)
            {
                var packIcon = new PackIcon
                {
                    Kind = PackIconKind.WindowMaximize
                };
                ButtonWinMax.Content = packIcon;
                Left = _normalRect.Left;
                Top = _normalRect.Top;
                Width = _normalRect.Width;
                Height = _normalRect.Height;
                _windowState = WindowState.Normal;
            }
        }

        private void ButtonWinClose_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ToolBar_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _i += 1;
            var timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 300)
            };
            timer.Tick += (s, e1) =>
            {
                timer.IsEnabled = false;
                _i = 0;
            };
            timer.IsEnabled = true;
            if (_i % 2 == 0)
            {
                timer.IsEnabled = false;
                _i = 0;
                if (_windowState == WindowState.Normal)
                {
                    var packIcon = new PackIcon
                    {
                        Kind = PackIconKind.WindowRestore
                    };
                    ButtonWinMax.Content = packIcon;
                    _normalRect = new Rect(Left, Top, Width, Height);
                    Left = 0;
                    Top = 0;
                    var rect = SystemParameters.WorkArea;
                    Width = rect.Width;
                    Height = rect.Height;
                    _windowState = WindowState.Maximized;
                }
                else if (_windowState == WindowState.Maximized)
                {
                    var packIcon = new PackIcon
                    {
                        Kind = PackIconKind.WindowMaximize
                    };
                    ButtonWinMax.Content = packIcon;
                    Left = _normalRect.Left;
                    Top = _normalRect.Top;
                    Width = _normalRect.Width;
                    Height = _normalRect.Height;
                    _windowState = WindowState.Normal;
                }
            }
        }

        private void ToolBar_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
    }
}