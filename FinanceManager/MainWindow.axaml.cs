using Avalonia.Controls;
using Avalonia.Interactivity;
using FinanceManager.MainViewModel;

namespace FinanceManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new FinanceManager.MainViewModel.MainViewModel();
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