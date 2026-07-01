using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using FinanceManager.Commands;
using FinanceManager.Data;
using FinanceManager.Enums;
using FinanceManager.Models;
using FinanceManager.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading;

namespace FinanceManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly UserRepository userRepository = new UserRepository();
        private readonly BudgetRepository budgetRepository = new BudgetRepository();
        private readonly ExpenseRepository expenseRepository = new ExpenseRepository();
        private readonly IncomeRepository incomeRepository = new IncomeRepository();
        private readonly TransactionRepository transactionRepository = new TransactionRepository();
        private readonly CategoryRepository categoryRepository = new CategoryRepository();
        private readonly RecurringTransactionRepository recurringTransactionRepository = new RecurringTransactionRepository();
        private readonly Database _db = new Database();
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private int _userId;
        private string _login;
        private string _password;
        private string _email;
        private bool _homePageVisible = false;
        private bool _registerPageVisible = false;
        private bool _loginPageVisible = true;
        private bool _budgetCreatePageVisible = false;
        private bool _budgetSettingsPageVisible = false;
        private bool _categorySettingsPageVisible = false;
        private bool _recurringTransactionSettingsPageVisible = false;
        private string _budgetCountString;
        private int _budgetCount;
        private string _transactionAmountString;
        private string _transactionDescription;
        private int _transactionAmount;
        private int _balance;
        private string _icon;
        private string _categoryName;
        private List<Category> _categoryList = new List<Category>();
        private List<Transaction> _transactionList = new List<Transaction>();
        private bool _isIncomeChecked = true;
        private bool _isExpenseChecked = false;
        private Category _selectedCategory;
        private string _budgetStatus;
        private string _budgetEndDateString;
        private DateTime _budgetEndDate;
        private bool _hasBudget;
        private int _budgetPercent;
        private List<Category> _incomeCategoryList;
        private List<Category> _expenseCategoryList;
        private string _newBudgetCountString;
        private int _newBudgetCount;
        private string _recurringTransactionAmountString;
        private string _recurringTransactionDescription;
        private string _recurringTransactionEndDateString;
        private RecurringPeriod _recurringPeriod;
        private int _recurringTransactionAmount;
        private DateTime _recurringTransactionEndDate;
        private bool _isDailyChecked = true;
        private bool _isWeeklyChecked = false;
        private bool _isMonthlyChecked = false;
        private bool _isYearlyChecked = false;
        private List<RecurringTransaction> _recurringTransactionList = new List<RecurringTransaction>();
        private RecurringTransaction _selectedRecurringTransaction;
        private Timer _recurringTransactionTimer;

        private bool _reportPageVisible;
        private List<Transaction> _monthlyTransactions;
        private int _monthlyIncome;
        private int _monthlyExpense;
        private int _monthlyBalance;
        private string _selectedMonth;
        private List<string> _monthOptions = new List<string>();
        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
                LoginCommand.RaiseCanExecuteChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
                LoginCommand.RaiseCanExecuteChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }
        public bool HomePageVisible
        {
            get { return _homePageVisible; }
            set
            {
                _homePageVisible = value;
                OnPropertyChanged();
            }
        }
        public bool LoginPageVisible
        {
            get { return _loginPageVisible; }
            set
            {
                _loginPageVisible = value;
                OnPropertyChanged();
            }
        }
        public bool BudgetCreatePageVisible
        {
            get { return _budgetCreatePageVisible; }
            set
            {
                _budgetCreatePageVisible = value;
                OnPropertyChanged();
            }
        }
        public bool RegisterPageVisible
        {
            get { return _registerPageVisible; }
            set
            {
                _registerPageVisible = value;
                OnPropertyChanged();
            }
        }
        public string BudgetCountString
        {
            get { return _budgetCountString; }
            set
            {
                _budgetCountString = value;
                OnPropertyChanged();
                CreateBudgetCommand.RaiseCanExecuteChanged();
            }
        }
        public int BudgetCount
        {
            get { return _budgetCount; }
            set
            {
                _budgetCount = value;
                OnPropertyChanged();
            }
        }
        public string TransactionAmountString
        {
            get { return _transactionAmountString; }
            set
            {
                _transactionAmountString = value;
                OnPropertyChanged();
                AddExpenseCommand.RaiseCanExecuteChanged();
                AddIncomeCommand.RaiseCanExecuteChanged();

            }
        }
        public int TransactionAmount
        {
            get { return _transactionAmount; }
            set
            {
                _transactionAmount = value;
                OnPropertyChanged();

            }
        }
        public string TransactionDescription
        {
            get { return _transactionDescription; }
            set
            {
                _transactionDescription = value;
                OnPropertyChanged();
                AddExpenseCommand.RaiseCanExecuteChanged();
                AddIncomeCommand.RaiseCanExecuteChanged();
            }
        }
        public int Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                OnPropertyChanged();
            }
        }
        public bool BudgetSettingsPageVisible
        {
            get { return _budgetSettingsPageVisible; }
            set
            {
                _budgetSettingsPageVisible = value;
                OnPropertyChanged();
            }
        }
        public List<Category> CategoryList
        {
            get { return _categoryList; }
            set
            {
                _categoryList = value;
                OnPropertyChanged();
            }
        }
        public List<Transaction> TransactionList
        {
            get { return _transactionList; }
            set
            {
                _transactionList = value;
                OnPropertyChanged();
            }
        }
        public string Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                OnPropertyChanged();
                AddCategoryCommand.RaiseCanExecuteChanged();
            }
        }
        public bool IsIncomeChecked
        {
            get { return _isIncomeChecked; }
            set
            {
                _isIncomeChecked = value;
                OnPropertyChanged();
            }
        }
        public bool IsExpenseChecked
        {
            get { return _isExpenseChecked; }
            set
            {
                _isExpenseChecked = value;
                OnPropertyChanged();
            }
        }
        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                _categoryName = value;
                OnPropertyChanged();
                AddCategoryCommand.RaiseCanExecuteChanged();
            }
        }
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                AddIncomeCommand.RaiseCanExecuteChanged();
                AddExpenseCommand.RaiseCanExecuteChanged();
            }
        }
        public bool CategorySettingsPageVisible
        {
            get { return _categorySettingsPageVisible; }
            set
            {
                _categorySettingsPageVisible = value;
                OnPropertyChanged();
            }
        }
        public string BudgetStatus
        {
            get { return _budgetStatus; }
            set
            {
                _budgetStatus = value;
                OnPropertyChanged();
            }
        }
        public string BudgetEndDateString
        {
            get { return _budgetEndDateString; }
            set
            {
                _budgetEndDateString = value;
                OnPropertyChanged();
                CreateBudgetCommand.RaiseCanExecuteChanged();
            }
        }
        public DateTime BudgetEndDate
        {
            get { return _budgetEndDate; }
            set
            {
                _budgetEndDate = value;
                OnPropertyChanged();
            }
        }
        public bool HasBudget
        {
            get { return _hasBudget; }
            set
            {
                _hasBudget = value;
                OnPropertyChanged();
            }
        }
        public int BudgetPercent
        {
            get { return _budgetPercent; }
            set
            {
                _budgetPercent = value;
                OnPropertyChanged();
            }
        }
        public List<Category> IncomeCategoryList
        {
            get { return _incomeCategoryList; }
            set
            {
                _incomeCategoryList = value;
                OnPropertyChanged();
            }
        }
        public List<Category> ExpenseCategoryList
        {
            get { return _expenseCategoryList; }
            set
            {
                _expenseCategoryList = value;
                OnPropertyChanged();
            }
        }
        public string NewBudgetCountString
        {
            get { return _newBudgetCountString; }
            set
            {
                _newBudgetCountString = value;
                OnPropertyChanged();
                EditBudgetCommand.RaiseCanExecuteChanged();
            }
        }
        public int NewBudgetCount
        {
            get { return _newBudgetCount; }
            set
            {
                _newBudgetCount = value;
                OnPropertyChanged();
            }
        }
        public string RecurringTransactionAmountString
        {
            get { return _recurringTransactionAmountString; }
            set
            {
                _recurringTransactionAmountString = value;
                OnPropertyChanged();
                AddRecurringTransactionCommand.RaiseCanExecuteChanged();
            }
        }
        public int RecurringTransactionAmount
        {
            get { return _recurringTransactionAmount; }
            set
            {
                _recurringTransactionAmount = value;
                OnPropertyChanged();
            }
        }
        public string RecurringTransactionDescription
        {
            get { return _recurringTransactionDescription; }
            set
            {
                _recurringTransactionDescription = value;
                OnPropertyChanged();
                AddRecurringTransactionCommand.RaiseCanExecuteChanged();
            }
        }
        public string RecurringTransactionEndDateString
        {
            get { return _recurringTransactionEndDateString; }
            set
            {
                _recurringTransactionEndDateString = value;
                OnPropertyChanged();
                AddRecurringTransactionCommand.RaiseCanExecuteChanged();
            }
        }
        public DateTime RecurringTransactionEndDate
        {
            get { return _recurringTransactionEndDate; }
            set
            {
                _recurringTransactionEndDate = value;
                OnPropertyChanged();
            }
        }
        public RecurringPeriod RecurringPeriod
        {
            get { return _recurringPeriod; }
            set
            {
                _recurringPeriod = value;
                OnPropertyChanged();
            }
        }
        public bool RecurringTransactionSettingsPageVisible
        {
            get { return _recurringTransactionSettingsPageVisible; }
            set
            {
                _recurringTransactionSettingsPageVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsDailyChecked
        {
            get { return _isDailyChecked; }
            set
            {
                _isDailyChecked = value;
                OnPropertyChanged();
            }
        }
        public bool IsWeeklyChecked
        {
            get { return _isWeeklyChecked; }
            set
            {
                _isWeeklyChecked = value;
                OnPropertyChanged();
            }
        }
        public bool IsMonthlyChecked
        {
            get { return _isMonthlyChecked; }
            set
            {
                _isMonthlyChecked = value;
                OnPropertyChanged();
            }
        }
        public bool IsYearlyChecked
        {
            get { return _isYearlyChecked; }
            set
            {
                _isYearlyChecked = value;
                OnPropertyChanged();
            }
        }
        public List<RecurringTransaction> RecurringTransactionList
        {
            get { return _recurringTransactionList; }
            set
            {
                _recurringTransactionList = value;
                OnPropertyChanged();
            }
        }
        public RecurringTransaction SelectedRecurringTransaction
        {
            get { return _selectedRecurringTransaction; }
            set
            {
                _selectedRecurringTransaction = value;
                OnPropertyChanged();
            }
        }
        public Timer RecurringTransactionTimer
        {
            get { return _recurringTransactionTimer; }
            set
            {
                _recurringTransactionTimer = value;
                OnPropertyChanged();
            }
        }
        public bool ReportPageVisible
        {
            get { return _reportPageVisible; }
            set
            {
                _reportPageVisible = value;
                OnPropertyChanged();
            }
        }
        public List<Transaction> MonthlyTransactions
        {
            get { return _monthlyTransactions; }
            set
            {
                _monthlyTransactions = value;
                OnPropertyChanged();
            }
        }
        public int MonthlyIncome
        {
            get { return _monthlyIncome; }
            set 
            { 
                _monthlyIncome = value;
                OnPropertyChanged(); 
            }
        }
        public int MonthlyExpense
        {
            get { return _monthlyExpense; }
            set
            {
                _monthlyExpense = value;
                OnPropertyChanged();
            }
        }
        public int MonthlyBalance
        {
            get { return _monthlyBalance; }
            set
            {
                _monthlyBalance = value;
                OnPropertyChanged();
            }
        }
        public string SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                _selectedMonth = value;
                OnPropertyChanged();
                CreateReport();
            }
        }
        public List<string> MonthOptions
        {
            get { return _monthOptions; }
            set 
            { 
                _monthOptions = value; 
                OnPropertyChanged(); 
            }
        }
        public RelayCommand LoginCommand { get; }
        public RelayCommand RegisterCommand { get; }
        public RelayCommand CreateBudgetCommand { get; }
        public RelayCommand AddIncomeCommand { get; }
        public RelayCommand AddExpenseCommand { get; }
        public RelayCommand SortByCategoryCommand { get; }
        public RelayCommand SortByDateCommand { get; }
        public RelayCommand AddCategoryCommand { get; }
        public RelayCommand DeleteCategoryCommand { get; }
        public RelayCommand EditBudgetCommand { get; }
        public RelayCommand AddRecurringTransactionCommand { get; }
        public RelayCommand DeleteRecurringTransactionCommand { get; }
        public RelayCommand ExportReportCommand { get; }
        public RelayCommand CreateReportCommand { get; }
        public MainViewModel()
        {
            for (int i = 0; i < 12; i++)
            {
                var date = DateTime.Now.AddMonths(-i);
                MonthOptions.Add($"{date:MMMM yyyy}");
            }
            SelectedMonth = MonthOptions.FirstOrDefault();

            categoryRepository.AddDefault();
            LoginCommand = new RelayCommand(OnLogin, () => CanLogin);
            RegisterCommand = new RelayCommand(OnRegister, () => CanRegister);
            CreateBudgetCommand = new RelayCommand(OnCreateBudget, () => CanCreateBudget);
            AddIncomeCommand = new RelayCommand(OnAddIncome, () => CanAddTransaction);
            AddExpenseCommand = new RelayCommand(OnAddExpense, () => CanAddTransaction);
            SortByCategoryCommand = new RelayCommand(OnSortByCategory, () => true);
            SortByDateCommand = new RelayCommand(OnSortByDate, () => true);
            AddCategoryCommand = new RelayCommand(OnAddCategory, () => CanAddCategory);
            DeleteCategoryCommand = new RelayCommand(OnDeleteCategory, () => true);
            EditBudgetCommand = new RelayCommand(OnEditBudget, () => CanEditBudget);
            AddRecurringTransactionCommand = new RelayCommand(OnAddRecurringTransaction, () => CanAddRecurringTransaction);
            DeleteRecurringTransactionCommand = new RelayCommand(OnDeleteRecurringTransaction, () => true);
            ExportReportCommand = new RelayCommand(ExportReport, () => CanExportReport);
            CreateReportCommand = new RelayCommand(CreateReport, () => true);
        }
        public bool CanLogin => !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        public bool CanRegister => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Login) && Email.Contains("@");
        public bool CanCreateBudget => !string.IsNullOrWhiteSpace(BudgetCountString) && !string.IsNullOrWhiteSpace(BudgetEndDateString);
        public bool CanAddTransaction => !string.IsNullOrWhiteSpace(TransactionDescription) && !string.IsNullOrWhiteSpace(TransactionAmountString) && SelectedCategory != null;
        public bool CanAddCategory => !string.IsNullOrWhiteSpace(CategoryName) && !string.IsNullOrWhiteSpace(Icon);
        public bool CanEditBudget => !string.IsNullOrWhiteSpace(NewBudgetCountString);
        public bool CanAddRecurringTransaction => !string.IsNullOrWhiteSpace(RecurringTransactionAmountString) && !string.IsNullOrWhiteSpace(RecurringTransactionDescription) && !string.IsNullOrWhiteSpace(RecurringTransactionEndDateString);
        public bool CanExportReport => !SelectedMonth.IsNullOrEmpty();

        public int GetMonth(string monthName)
        {
            var months = new[] { "январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь" };
            return Array.IndexOf(months, monthName) + 1;
        }
        public void CreateReport()
        {
            if (SelectedMonth.IsNullOrEmpty())
            {
                return;
            }
            var parts = SelectedMonth.Split(' ');
            var month = GetMonth(parts[0]);
            var year = int.Parse(parts[1]);
            var start = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
            var end = start.AddMonths(1);
            var transactions = _db.Transactions
                .Where(x => x.UserId == UserId && x.Date >= start && x.Date <= end)
                .Include(x => x.Category)
                .OrderByDescending(x => x.Date)
                .ToList();
            MonthlyTransactions = transactions;
            MonthlyIncome = transactions.Where(t => t.transactionType == TransactionType.Income).Sum(t => t.Amount);
            MonthlyExpense = transactions.Where(t => t.transactionType == TransactionType.Expense).Sum(t => t.Amount);
            MonthlyBalance = MonthlyIncome - MonthlyExpense;
            OnPropertyChanged(nameof(MonthlyTransactions));
            OnPropertyChanged(nameof(MonthlyIncome));
            OnPropertyChanged(nameof(MonthlyExpense));
            OnPropertyChanged(nameof(MonthlyBalance));
        }

        public async void ExportReport()
        {
            var mainWindow = Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime; // Получение главного окна
            if (MonthlyTransactions == null || !MonthlyTransactions.Any())
            {
                return;
            }
            var dialog = new SaveFileDialog // Создание диалога сохранения окна
            {
                Title = "Сохранить отчёт",
                DefaultExtension = "csv",
                Filters = new List<FileDialogFilter> { new() { Name = "CSV", Extensions = { "csv" } } }
            };
            var path = await dialog.ShowAsync(mainWindow.MainWindow); // Открытие диалога
            if (path.IsNullOrEmpty())
            {

            }
            using var writer = new StreamWriter(path, false, Encoding.UTF8);
            writer.WriteLine("Дата;Сумма;Описание;Тип;Категория");
            foreach (var t in MonthlyTransactions)
            {
                writer.WriteLine($"{t.Date:dd.MM.yyyy};{t.Amount}₽;{t.Description};{(t.transactionType == TransactionType.Income ? "Доход" : "Расход")};{t.Category?.Name ?? "Без категории"}");
            }
            writer.WriteLine(";;;;");
            writer.WriteLine($"Итого доходов;{MonthlyIncome}₽");
            writer.WriteLine($"Итого расходов;{MonthlyExpense}₽");
            writer.WriteLine($"Баланс;{MonthlyBalance}₽");
        }

        public TransactionType GetTransactionType()
        {
            TransactionType transactionType;
            if (IsIncomeChecked == true)
            {
                transactionType = TransactionType.Income;
                return transactionType;
            }
            else
            {
                transactionType = TransactionType.Expense;
                return transactionType;
            }
        }
        public RecurringPeriod GetRecurringPeriod()
        {
            RecurringPeriod recurringPeriod;
            if (IsDailyChecked == true)
            {
                recurringPeriod = RecurringPeriod.Daily;
                return recurringPeriod;
            }
            if (IsWeeklyChecked == true)
            {
                recurringPeriod = RecurringPeriod.Weekly;
                return recurringPeriod;
            }
            if (IsMonthlyChecked == true)
            {
                recurringPeriod = RecurringPeriod.Monthly;
                return recurringPeriod;
            }
            if (IsYearlyChecked == true)
            {
                recurringPeriod = RecurringPeriod.Yearly;
                return recurringPeriod;
            }
            else
            {
                throw new Exception("Ошибка выбора периода!");
            }
        }
        public void OnEditBudget()
        {
            try
            {
                NewBudgetCount = Convert.ToInt32(NewBudgetCountString);
            }
            catch { throw new Exception("Вы ввели некорректное число!"); }
            var budget = _db.Budgets.FirstOrDefault(x => x.UserId == UserId);
            budget.LimitAmount = NewBudgetCount;
            _db.Update(budget);
            _db.SaveChanges();
            NewBudgetCountString = "Успешно изменено!";
            BudgetCount = NewBudgetCount;
            BudgetPercent = _db.Budgets.FirstOrDefault(x => x.UserId == UserId).GetProgressPercent();
            OnPropertyChanged(nameof(BudgetPercent));
            OnPropertyChanged(nameof(BudgetCount));
            OnPropertyChanged(nameof(NewBudgetCountString));
        }
        public void StartSubscriptionCheck()
        {
            RecurringTransactionTimer = new Timer(_ =>
            {
                recurringTransactionRepository.RecurringPay(UserId);
                Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                {
                    LoadTransactions();
                    LoadBudgetPercent();
                    LoadStatus();
                    LoadBalance();
                });
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
        }
        public void UpdateHasBudget()
        {
            var budget = _db.Budgets.FirstOrDefault(x => x.UserId == UserId);
            if (budget != null)
            {
                HasBudget = true;
            }
        }
        public void UpdateBudget()
        {
            var budget = _db.Budgets.FirstOrDefault(x => x.UserId == UserId);
            if (budget != null)
            {
                BudgetCount = budget.LimitAmount;
            }
            else
            {
                HasBudget = false;
                BudgetCount = 0;
                BudgetPercent = 0;
                BudgetStatus = "No budget";
            }
        }
        public void LoadBudgetPercent()
        {
            var budget = _db.Budgets.FirstOrDefault(x => x.UserId == UserId);
            if (budget != null)
            {
                BudgetPercent = budget.GetProgressPercent();
                OnPropertyChanged(nameof(BudgetPercent));
            }
        }
        public void UpdateBalance(int amount, TransactionType type)
        {
            var user = _db.Users.Find(UserId);
            if (type == TransactionType.Income)
            {
                user.Balance += amount;
            }
            else
            {
                if (user.Balance - amount < 0)
                {
                    throw new Exception("Не хватает средств на балансе!");
                }
                user.Balance -= amount;
                UpdateBudgetSpent(amount);
            }
            _db.SaveChanges();
            Balance = user.Balance;
            OnPropertyChanged(nameof(Balance));
        }
        public void UpdateBudgetSpent(int amount)
        {
            var budget = _db.Budgets.FirstOrDefault(x => x.UserId == UserId);
            if (budget != null)
            {
                budget.SpentAmount += amount;
                _db.SaveChanges();
                LoadBudgetPercent();
            }
        }
        public void LoadCategories()
        {
            CategoryList = _db.Categories.ToList();
            IncomeCategoryList = _db.Categories
                .Where(c => c.TransactionType == TransactionType.Income)
                .ToList();
            ExpenseCategoryList = _db.Categories
                .Where(c => c.TransactionType == TransactionType.Expense)
                .ToList();
            OnPropertyChanged(nameof(CategoryList));
            OnPropertyChanged(nameof(IncomeCategoryList));
            OnPropertyChanged(nameof(ExpenseCategoryList));
        }
        public void LoadTransactions()
        {
            TransactionList = _db.Transactions
                .Where(x => x.UserId == UserId)
                .ToList();
            OnPropertyChanged(nameof(TransactionList));
        }
        public void LoadRecurringTransactions()
        {
            RecurringTransactionList = _db.RecurringTransactions
                .Where(x => x.UserId == UserId)
                .ToList();
            OnPropertyChanged(nameof(RecurringTransactionList));
        }
        public void LoadStatus()
        {
            BudgetStatus = budgetRepository.ChangeStatus(UserId);
            OnPropertyChanged(nameof(BudgetStatus));
        }
        public void LoadBalance()
        {
            var user = _db.Users.Find(UserId);
            if (user != null)
            {
                Balance = user.Balance;
            }
            OnPropertyChanged(nameof(Balance));
        }
        public void CheckRecurringTransactionPeriod()
        {
            var recurringTransactions = _db.RecurringTransactions
               .Where(r => r.IsActive == true && r.UserId == UserId)
               .ToList();
            foreach (var recurringTransaction in recurringTransactions)
            {
                recurringTransactionRepository.ChangeStatusRecurringTransaction(recurringTransaction);
            }
        }
        public async void OnLogin()
        {
            var user = userRepository.Login(Login, Password);
            if (user != null)
            {
                UserId = user.Id;
                Balance = user.Balance;
            }
            LoginPageVisible = false;
            HomePageVisible = true;
            OnPropertyChanged(nameof(LoginPageVisible));
            OnPropertyChanged(nameof(HomePageVisible));
            OnPropertyChanged(nameof(BudgetStatus));
            recurringTransactionRepository.RecurringPay(UserId);
            StartSubscriptionCheck();
            UpdateHasBudget();
            UpdateBudget();
            LoadCategories();
            LoadTransactions();
            LoadBalance();
            LoadStatus();
            LoadBudgetPercent();
            LoadRecurringTransactions();
            CheckRecurringTransactionPeriod();
            CreateReport();
        }
        public void OnRegister()
        {
            var user = userRepository.Register(Login, Password, Email);
            if (user != null)
            {
                UserId = user.Id;
                Balance = user.Balance;
            }
            RegisterPageVisible = false;
            LoginPageVisible = false;
            HomePageVisible = true;
            OnPropertyChanged(nameof(RegisterPageVisible));
            OnPropertyChanged(nameof(LoginPageVisible));
            OnPropertyChanged(nameof(HomePageVisible));
            UpdateHasBudget();
            UpdateBudget();
            LoadCategories();
            LoadTransactions();
            LoadBalance();
            LoadBudgetPercent();
            StartSubscriptionCheck();
        }
        public void OnCreateBudget()
        {
            try
            {
                BudgetCount = Convert.ToInt32(BudgetCountString);
                BudgetEndDate = Convert.ToDateTime(BudgetEndDateString);
            }
            catch { throw new Exception("Вы ввели некорректное число или дату! (ДД.ММ.ГГГГ)!"); }
            if (BudgetEndDate <= DateTime.UtcNow)
            {
                throw new Exception("Вы ввели некорректную дату!");
            }
            if (BudgetCount <= 0)
            {
                throw new Exception("Вы ввели некорректное число!");
            }
            if (_db.Budgets.Any(x => x.UserId == UserId))
            {
                throw new Exception("У вас уже есть бюджет!");
            }
            budgetRepository.AddBudget(UserId, BudgetCount, BudgetEndDate);
            HasBudget = true;
            BudgetSettingsPageVisible = false;
            BudgetCreatePageVisible = false;
            HomePageVisible = true;
            OnPropertyChanged(nameof(BudgetCreatePageVisible));
            OnPropertyChanged(nameof(BudgetSettingsPageVisible));
            OnPropertyChanged(nameof(HomePageVisible));
            OnPropertyChanged(nameof(HasBudget));
            LoadBudgetPercent();
        }
        public void OnAddIncome()
        {
            try
            {
                TransactionAmount = Convert.ToInt32(TransactionAmountString);
            }
            catch { throw new Exception("Вы ввели некорректное число!"); }
            if (TransactionAmount <= 0)
            {
                throw new Exception("Вы ввели некорректное число!");
            }
            incomeRepository.AddIncome(UserId, TransactionAmount, TransactionDescription, SelectedCategory.Id);
            UpdateBalance(TransactionAmount, TransactionType.Income);
            LoadTransactions();
            LoadStatus();
            LoadBudgetPercent();
            OnPropertyChanged(nameof(Balance));
            OnPropertyChanged(nameof(TransactionList));
        }
        public void OnAddRecurringTransaction()
        {
            try
            {
                RecurringTransactionAmount = Convert.ToInt32(RecurringTransactionAmountString);
                RecurringTransactionEndDate = Convert.ToDateTime(RecurringTransactionEndDateString);
            }
            catch { throw new Exception("Вы ввели некорректное число или дату! (ДД.ММ.ГГГГ)"); }
            RecurringPeriod = GetRecurringPeriod();
            recurringTransactionRepository.AddRecurringTransaction(UserId, RecurringTransactionAmount, RecurringTransactionDescription, RecurringPeriod, RecurringTransactionEndDate);
            LoadRecurringTransactions();
        }
        public void OnDeleteRecurringTransaction()
        {
            if (SelectedRecurringTransaction == null)
            {
                throw new Exception("Вы не выбрали подписку!");
            }
            var recurring = _db.RecurringTransactions.Find(SelectedRecurringTransaction.Id);
            if (recurring != null)
            {
                recurringTransactionRepository.DeleteRecurringTransaction(recurring);
            }
            RecurringTransactionSettingsPageVisible = false;
            HomePageVisible = true;
            OnPropertyChanged(nameof(RecurringTransactionSettingsPageVisible));
            OnPropertyChanged(nameof(HomePageVisible));
            LoadRecurringTransactions();
        }
        public void OnAddExpense()
        {
            try
            {
                TransactionAmount = Convert.ToInt32(TransactionAmountString);
            }
            catch { throw new Exception("Вы ввели некорректное число!"); }
            if (TransactionAmount <= 0)
            {
                throw new Exception("Вы ввели некорректное число!");
            }
            if (Balance - TransactionAmount < 0)
            {
                throw new Exception("Недостаточно средств на балансе!");
            }
            expenseRepository.AddExpense(UserId, TransactionAmount, TransactionDescription, SelectedCategory.Id);
            UpdateBalance(TransactionAmount, TransactionType.Expense);
            LoadTransactions();
            LoadStatus();
            LoadBudgetPercent();
            OnPropertyChanged(nameof(Balance));
            OnPropertyChanged(nameof(TransactionList));
        }
        public void OnSortByCategory()
        {
            TransactionList = transactionRepository.SortByCategory(UserId);
            OnPropertyChanged(nameof(TransactionList));

        }
        public void OnSortByDate()
        {
            TransactionList = transactionRepository.SortByDate(UserId);
            OnPropertyChanged(nameof(TransactionList));
        }
        public void OnAddCategory()
        {
            if (Icon.Length > 2)
            {
                throw new Exception("Иконка не может быть длинее 1 символа!");
            }
            TransactionType transactionType = GetTransactionType();
            categoryRepository.AddCategory(CategoryName, Icon, transactionType);
            CategorySettingsPageVisible = false;
            HomePageVisible = true;
            OnPropertyChanged(nameof(CategorySettingsPageVisible));
            OnPropertyChanged(nameof(HomePageVisible));
            LoadCategories();
        }
        public void OnDeleteCategory()
        {
            if (SelectedCategory == null)
            {
                throw new Exception("Вы не выбрали категорию!");
            }
            categoryRepository.DeleteCategory(SelectedCategory);
            CategorySettingsPageVisible = false;
            HomePageVisible = true;
            OnPropertyChanged(nameof(CategorySettingsPageVisible));
            OnPropertyChanged(nameof(HomePageVisible));
            LoadCategories();
        }
    }
}