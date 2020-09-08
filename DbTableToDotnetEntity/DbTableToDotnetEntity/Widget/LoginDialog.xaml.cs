using System.Windows.Controls;
using DbTableToDotnetEntity.ViewModel;

namespace DbTableToDotnetEntity.Widget
{
    /// <summary>
    /// LoginDialog.xaml 的交互逻辑
    /// </summary>
    public partial class LoginDialog : UserControl
    {
        public LoginDialog()
        {
            InitializeComponent();

            DataContext = new LoginDialogViewModel();
        }
    }
}
