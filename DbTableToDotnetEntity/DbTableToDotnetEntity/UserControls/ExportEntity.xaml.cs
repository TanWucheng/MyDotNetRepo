using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DbTableToDotnetEntity.Domain;
using DbTableToDotnetEntity.Models;
using DbTableToDotnetEntity.Service.Interface;
using DbTableToDotnetEntity.ViewModel;

namespace DbTableToDotnetEntity.UserControls
{
    /// <summary>
    /// ExportEntity.xaml 的交互逻辑
    /// </summary>
    public partial class ExportEntity : UserControl
    {
        public ExportEntity(ITableInfoService tableInfoService,IColumnInfoService columnInfoService)
        {
            InitializeComponent();

            DataContext = new ExportEntityViewModel(tableInfoService,columnInfoService);
        }

        private void ListBoxItem_PreviewGotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            if (sender is ListBoxItem item)
            {
                item.IsSelected = true;
            }
        }

        private void PwdBox_UserPwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PwdBoxUserPwd.Background = string.IsNullOrWhiteSpace(PwdBoxUserPwd.Password) ? new SolidColorBrush(Color.FromArgb(255, 249, 189, 187)) : new SolidColorBrush(Colors.White);
        }

        private void DbHistoryListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem listBoxItem;
            ContentPresenter myContentPresenter;
            DataTemplate myDataTemplate;
            RadioButton radioButton;
            var selectedIndex = DbHistoryListBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                if (DataContext is ExportEntityViewModel viewModel)
                {
                    viewModel.CurrentDbServerInfoItem = new DbServerInfo
                    {
                        Ip = viewModel.DbServerHistoryItems[selectedIndex].Ip,
                        Database = viewModel.DbServerHistoryItems[selectedIndex].Database,
                        DatabaseType = viewModel.DbServerHistoryItems[selectedIndex].DatabaseType,
                        Encoding = viewModel.DbServerHistoryItems[selectedIndex].Encoding,
                        Password = viewModel.DbServerHistoryItems[selectedIndex].Password,
                        TcpIpPort = viewModel.DbServerHistoryItems[selectedIndex].TcpIpPort,
                        UserId = viewModel.DbServerHistoryItems[selectedIndex].UserId
                    };
                }

                listBoxItem = (ListBoxItem)DbHistoryListBox.ItemContainerGenerator.ContainerFromItem(DbHistoryListBox.Items[DbHistoryListBox.SelectedIndex]);
                var visualTreeFinder = new VisualTreeFinder();
                myContentPresenter = visualTreeFinder.FindVisualChild<ContentPresenter>(listBoxItem);
                myDataTemplate = myContentPresenter.ContentTemplate;
                radioButton = myDataTemplate.FindName("IpRadioButton", myContentPresenter) as RadioButton;
                if (radioButton != null) radioButton.IsChecked = true;
            }
            else
            {
                foreach (var item in DbHistoryListBox.Items)
                {
                    listBoxItem = (ListBoxItem)DbHistoryListBox.ItemContainerGenerator.ContainerFromItem(item);
                    var visualTreeFinder = new VisualTreeFinder();
                    myContentPresenter = visualTreeFinder.FindVisualChild<ContentPresenter>(listBoxItem);
                    myDataTemplate = myContentPresenter.ContentTemplate;
                    radioButton = myDataTemplate.FindName("IpRadioButton", myContentPresenter) as RadioButton;
                    if (radioButton != null) radioButton.IsChecked = false;
                }
            }
        }
    }
}
