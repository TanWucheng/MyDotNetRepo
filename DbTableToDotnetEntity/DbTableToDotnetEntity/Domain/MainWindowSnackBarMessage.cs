using System.Windows.Media;

namespace DbTableToDotnetEntity.Domain
{
    internal class MainWindowSnackBarMessage
    {
        public static void Show(string message, SnackBarMessageType messageType)
        {
            switch (messageType)
            {
                case SnackBarMessageType.Success:
                    {
                        MainWindow.SnackBar.Background = new SolidColorBrush(Color.FromArgb(255, 37, 155, 36));
                        break;
                    }
                case SnackBarMessageType.Normal:
                    {
                        MainWindow.SnackBar.Background = new SolidColorBrush(Color.FromArgb(255, 66, 66, 66));
                        break;
                    }
                case SnackBarMessageType.Warning:
                    {
                        MainWindow.SnackBar.Background = new SolidColorBrush(Color.FromArgb(255, 239, 108, 0));
                        break;
                    }
                case SnackBarMessageType.Error:
                    {
                        MainWindow.SnackBar.Background = new SolidColorBrush(Color.FromArgb(255, 229, 28, 35));
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            MainWindow.SnackBar.MessageQueue.Enqueue(message);
        }
    }
}
