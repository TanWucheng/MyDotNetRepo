using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using DbTableToDotnetEntity.ViewModel;
using DbTableToDotnetEntity.Widget;
using MaterialDesignThemes.Wpf;

namespace DbTableToDotnetEntity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private async void ButtonLogout_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SampleMessageDialog()
            {
                Message = { Text = $"Now:{DateTime.Now:yyyy-MM-dd HH:mm:ss}" }
            };

            await DialogHost.Show(dialog, "RootDialog");
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
    }
}
