using FinanceManager.Commands;
using FinanceManager.Data;
using FinanceManager.Enums;
using FinanceManager.Models;
using FinanceManager.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

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
        public RelayCommand LoginCommand { get; }
        public RelayCommand RegisterCommand { get; }
        public RelayCommand CreateBudgetCommand { get; }
        public RelayCommand AddIncomeCommand { get; }
        public RelayCommand AddExpenseCommand { get; }
        public RelayCommand SortByCategoryCommand { get; }
        public RelayCommand SortByDateCommand { get; }
        public RelayCommand AddCategoryCommand { get; }
        public RelayCommand DeleteCategoryCommand {  get; }
        public RelayCommand EditBudgetCommand { get; }
        public MainViewModel()
        {
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
        }
        public bool CanLogin => !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        public bool CanRegister => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Login) && Email.Contains("@");
        public bool CanCreateBudget => !string.IsNullOrWhiteSpace(BudgetCountString) && !string.IsNullOrWhiteSpace(BudgetEndDateString);
        public bool CanAddTransaction => !string.IsNullOrWhiteSpace(TransactionDescription) && !string.IsNullOrWhiteSpace(TransactionAmountString);
        public bool CanAddCategory => !string.IsNullOrWhiteSpace(CategoryName) && !string.IsNullOrWhiteSpace(Icon);
        public bool CanEditBudget => !string.IsNullOrWhiteSpace(NewBudgetCountString);
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
        public void OnEditBudget()
        {
            try
            {
                NewBudgetCount = Convert.ToInt32(NewBudgetCountString);
            }
            catch { throw new Exception("Вы ввели некорректное число!"); }
            var budget = _db.Budgets.FirstOrDefault();
            budget.LimitAmount = NewBudgetCount;
            _db.Update(budget);
            _db.SaveChanges();
            NewBudgetCountString = "Успешно изменено!";
            BudgetCount = NewBudgetCount;
            BudgetPercent = _db.Budgets.FirstOrDefault().GetProgressPercent();
            OnPropertyChanged(nameof(BudgetPercent));
            OnPropertyChanged(nameof(BudgetCount));
            OnPropertyChanged(nameof(NewBudgetCountString));
        }
        public void UpdateHasBudget()
        {
            var budget = _db.Budgets.FirstOrDefault();
            if (budget != null)
            {
                HasBudget = true;
            }
        }
        public void UpdateBudget()
        {
            var budget = _db.Budgets.FirstOrDefault();
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
            if (_db.Budgets.FirstOrDefault() != null)
            {
                BudgetPercent = _db.Budgets.FirstOrDefault().GetProgressPercent();
                OnPropertyChanged(nameof(BudgetPercent));
            }
        }
        public void UpdateBalance(int amount, TransactionType type)
        {
            var user = _db.Users.FirstOrDefault();
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
            var budget = _db.Budgets.FirstOrDefault();
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
            TransactionList = _db.Transactions.ToList();
            OnPropertyChanged(nameof(TransactionList));
        }
        public void LoadStatus()
        {
            BudgetStatus = budgetRepository.ChangeStatus();
            OnPropertyChanged(nameof(BudgetStatus));
        }
        public void LoadBalance()
        {
            Balance = _db.Users.FirstOrDefault().Balance;
            OnPropertyChanged(nameof(Balance));
        }
        public void OnLogin()
        {
            userRepository.Login(Login, Password);
            LoginPageVisible = false;
            HomePageVisible = true;
            OnPropertyChanged(nameof(LoginPageVisible));
            OnPropertyChanged(nameof(HomePageVisible));
            OnPropertyChanged(nameof(BudgetStatus));
            UpdateHasBudget();
            UpdateBudget();
            LoadCategories();
            LoadTransactions();
            LoadBalance();
            LoadStatus();
            LoadBudgetPercent();
        }
        public void OnRegister()
        {
            userRepository.Register(Login, Password, Email);
            RegisterPageVisible = false;
            HomePageVisible = true;
            OnPropertyChanged(nameof(RegisterPageVisible));
            OnPropertyChanged(nameof(HomePageVisible));
            UpdateHasBudget();
            UpdateBudget();
            LoadCategories();
            LoadTransactions();
            LoadBalance();
            LoadBudgetPercent();
        }
        public void OnCreateBudget()
        {
            try
            {
                BudgetCount = Convert.ToInt32(BudgetCountString);
                BudgetEndDate = Convert.ToDateTime(BudgetEndDateString);
            }
            catch { throw new Exception("Вы ввели некорректное или дату! (ДД.ММ.ГГГГ)!"); }
            if (BudgetEndDate <= DateTime.UtcNow)
            {
                throw new Exception("Вы ввели некорректную дату!");
            }
            if (BudgetCount <= 0)
            {
                throw new Exception("Вы ввели некорректное число!");
            }
            if (_db.Budgets.Any())
            {
                throw new Exception("У вас уже есть бюджет!");
            }
            budgetRepository.AddBudget(BudgetCount, BudgetEndDate);
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
            if (SelectedCategory == null)
            {
                throw new Exception("Вы не выбрали категорию!");
            }
            incomeRepository.AddIncome(TransactionAmount, TransactionDescription, SelectedCategory.Id);
            UpdateBalance(TransactionAmount, TransactionType.Income);
            LoadTransactions();
            LoadStatus();
            LoadBudgetPercent();
            OnPropertyChanged(nameof(Balance));
            OnPropertyChanged(nameof(TransactionList));
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
            if (SelectedCategory == null)
            {
                throw new Exception("Вы не выбрали категорию!");
            }
            expenseRepository.AddExpense(TransactionAmount, TransactionDescription, SelectedCategory.Id);
            UpdateBalance(TransactionAmount, TransactionType.Expense);
            LoadTransactions();
            LoadStatus();
            LoadBudgetPercent();
            OnPropertyChanged(nameof(Balance));
            OnPropertyChanged(nameof(TransactionList));
        }
        public void OnSortByCategory()
        {
            TransactionList = transactionRepository.SortByCategory();
            OnPropertyChanged(nameof(TransactionList));

        }
        public void OnSortByDate()
        {
            TransactionList = transactionRepository.SortByDate();
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
