using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CoreWpfDemo.Domain;

namespace CoreWpfDemo.UserControls
{
    /// <summary>
    /// Buttons.xaml 的交互逻辑
    /// </summary>
    public partial class Buttons : UserControl
    {
        public Buttons()
        {
            InitializeComponent();

            FloatingActionDemoCommand = new AnotherCommandImplementation(Execute);
        }

        public ICommand FloatingActionDemoCommand { get; }

        private void Execute(object o)
        {
            Console.WriteLine("Floating action button command. - " + (o ?? "NULL").ToString());
        }

        private void CountingButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CountingBadge.Badge?.ToString()))
            {
                CountingBadge.Badge = 0;
            }

            var next = int.Parse(CountingBadge.Badge.ToString() ?? string.Empty) + 1;
            CountingBadge.Badge = next < 21 ? (object)next : null;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Just checking we haven't suppressed the button.");
        }
        private void PopupBox_OnOpened(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Just making sure the popup has opened.");
        }

        private void PopupBox_OnClosed(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Just making sure the popup has closed.");
        }
    }
}
