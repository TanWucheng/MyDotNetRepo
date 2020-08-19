using System.Windows;
using DbTableToDotnetEntity.ViewModel;

namespace DbTableToDotnetEntity
{
    /// <summary>
    /// TestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TestWindow
    {
        private TestViewModel _viewModel;

        public TestWindow()
        {
            InitializeComponent();

            _viewModel = new TestViewModel();
            DataContext = _viewModel;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Validate:" + _viewModel.IsValid);
        }
    }
}
