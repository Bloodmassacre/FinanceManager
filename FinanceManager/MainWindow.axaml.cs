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
            BudgetSettingsButtons.IsVisible = true;
            BudgetSettingsPage.IsVisible = true;
            HomePage.IsVisible = false;
            HomePageButtons.IsVisible = false;
            HomePageButtons2.IsVisible = false;
            HomePageButtons3.IsVisible = false;
            HomePageButtons4.IsVisible = false;
            AddIncomePage.IsVisible = false;
            AddExpensePage.IsVisible = false;
            HomePageTransactionList.IsVisible = false;
            HomePageCategoryList.IsVisible = false;
        }
        public void CategorySettingsButton(object? sender, RoutedEventArgs e)
        {
            CategorySettingsText.IsVisible = true;
            CategorySettingsPage.IsVisible = true;
            CategorySettingsButtons.IsVisible = true;
            HomePage.IsVisible = false;
            HomePageButtons.IsVisible = false;
            HomePageButtons2.IsVisible = false;
            HomePageButtons3.IsVisible = false;
            HomePageButtons4.IsVisible = false;
            HomePageTransactionList.IsVisible = false;
            HomePageCategoryList.IsVisible = false;
            AddIncomePage.IsVisible = false;
            AddExpensePage.IsVisible = false;
        }
        public void CloseSettingsButton(object? sender, RoutedEventArgs e)
        {
            BudgetSettingsText.IsVisible = false;
            BudgetSettingsButtons.IsVisible = false;
            BudgetSettingsPage.IsVisible = false;
            BudgetCreate.IsVisible = false;
            CategorySettingsButtons.IsVisible = false;
            CategorySettingsText.IsVisible = false;
            CategorySettingsPage.IsVisible = false;
            AddCategoryPage.IsVisible = false;
            DeleteCategoryPage.IsVisible = false;
            HomePage.IsVisible = true;
            HomePageButtons.IsVisible = true;
            HomePageButtons2.IsVisible = true;
            HomePageButtons3.IsVisible = true;
            HomePageButtons4.IsVisible = true;
            HomePageTransactionList.IsVisible = true;
            HomePageCategoryList.IsVisible = true;
        }
        public void AddCategoryButton(object? sender, RoutedEventArgs e)
        {
            CategorySettingsPage.IsVisible = false;
            AddCategoryPage.IsVisible = true;
        }
        public void DeleteCategoryButton(object? sender, RoutedEventArgs e)
        {
            CategorySettingsPage.IsVisible = false;
            DeleteCategoryPage.IsVisible = true;
        }
        public void AddBudgetButton(object? sender, RoutedEventArgs e)
        {
            BudgetSettingsPage.IsVisible = false;
            BudgetCreate.IsVisible = true;
        }
    }
}