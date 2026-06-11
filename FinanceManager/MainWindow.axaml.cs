using Avalonia.Controls;
using Avalonia.Interactivity;

namespace FinanceManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void RegisterButton(object? sender, RoutedEventArgs e)
        {
            LoginPanel.IsVisible = false;
            RegisterPanel.IsVisible = true;
        }
        public void CancelRegisterButton(object? sender, RoutedEventArgs e)
        {
            LoginPanel.IsVisible = true;
            RegisterPanel.IsVisible = false;
        }
    }
}