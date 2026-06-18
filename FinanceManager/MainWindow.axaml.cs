using Avalonia.Controls;
using Avalonia.Interactivity;

namespace FinanceManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new FinanceManager.ViewModels.MainViewModel();
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
        public void AddIncomeButton(object? sender, RoutedEventArgs e)
        {
            AddExpensePage.IsVisible = false;
            AddIncomePage.IsVisible = true;
        }
        public void AddExpenseButton(object? sender, RoutedEventArgs e)
        {
            AddExpensePage.IsVisible = true;
            AddIncomePage.IsVisible = false;
        }
        public void CancelTransactionButton(object? sender, RoutedEventArgs e)
        {
            AddExpensePage.IsVisible = false;
            AddIncomePage.IsVisible = false;
        }

    }
}