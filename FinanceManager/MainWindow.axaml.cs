using Avalonia.Controls;
using Avalonia.Interactivity;
using FinanceManager.Models;
using FinanceManager.Data;
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
        public void BudgetSettingsButton(object? sender, RoutedEventArgs e)
        {
            BudgetSettingsText.IsVisible = true;
            BudgetSettingsPage.IsVisible = true;
            HomePage.IsVisible = false;
            HomePageButtons.IsVisible = false;
            HomePageButtons2.IsVisible = false;
            HomePageButtons3.IsVisible = false;
            HomePageButtons4.IsVisible = false;
            HomePageTransactionList.IsVisible = false;
            HomePageCategoryList.IsVisible = false;
        }
        public void CloseBudgetSettingsButton(object? sender, RoutedEventArgs e)
        {
            BudgetSettingsPage.IsVisible = false;
            BudgetSettingsText.IsVisible = false;
            HomePage.IsVisible = true;
            HomePageButtons.IsVisible = true;
            HomePageButtons2.IsVisible = true;
            HomePageButtons3.IsVisible = true;
            HomePageButtons4.IsVisible = true;
            HomePageTransactionList.IsVisible = true;
            HomePageCategoryList.IsVisible = true;
        }
    }
}