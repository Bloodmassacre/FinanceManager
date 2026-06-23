using FinanceManager.Models;
using FinanceManager.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Commands;

namespace FinanceManager.ViewModels
{
    public class MainViewModel
    {
        private readonly UserRepository userRepository = new UserRepository();
        private readonly BudgetRepository budgetRepository = new BudgetRepository();
        private readonly ExpenseRepository expenseRepository = new ExpenseRepository();
        private readonly IncomeRepository incomeRepository = new IncomeRepository();
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private int _userId;
        private string _login;
        private string _password;
        private string _email;
        private bool _homePageVisible = true;
        private bool _registerPageVisible = false;
        private bool _loginPageVisible = false;
        private bool _budgetPageVisible = false;
        private bool _budgetSettingsPageVisible = false;
        private string _budgetCountString;
        private int _budgetCount;
        private string _transactionAmountString;
        private string _transactionDescription;
        private int _transactionAmount;
        private int _balance;

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
        public bool BudgetPageVisible
        {
            get { return _budgetPageVisible; }
            set
            {
                _budgetPageVisible = value;
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
        public RelayCommand LoginCommand { get; }
        public RelayCommand RegisterCommand { get; }
        public RelayCommand CreateBudgetCommand { get; }
        public RelayCommand AddIncomeCommand { get; }
        public RelayCommand AddExpenseCommand { get; }
        public MainViewModel()
        {
            LoginCommand = new RelayCommand(OnLogin, () => CanLogin);
            RegisterCommand = new RelayCommand(OnRegister, () => CanRegister);
            CreateBudgetCommand = new RelayCommand(OnCreateBudget, () => CanCreateBudget);
            AddIncomeCommand = new RelayCommand(OnAddIncome, () => CanAddTransaction);
            AddExpenseCommand = new RelayCommand(OnAddExpense, () => CanAddTransaction);
        }
        public bool CanLogin => !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        public bool CanRegister => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Login) && Email.Contains("@");
        public bool CanCreateBudget => !string.IsNullOrWhiteSpace(BudgetCountString);
        public bool CanAddTransaction => !string.IsNullOrWhiteSpace(TransactionDescription) && !string.IsNullOrWhiteSpace(TransactionAmountString);
        public void OnLogin()
        {
            userRepository.Login(Login, Password);
            LoginPageVisible = false;
            HomePageVisible = true;
            OnPropertyChanged(nameof(LoginPageVisible));
            OnPropertyChanged(nameof(HomePageVisible));
        }
        public void OnRegister()
        {
            userRepository.Register(Login, Password, Email);
            RegisterPageVisible = false;
            BudgetPageVisible = true;
            OnPropertyChanged(nameof(RegisterPageVisible));
            OnPropertyChanged(nameof(BudgetPageVisible));
        }
        public void OnCreateBudget()
        {
            try
            {
                BudgetCount = Convert.ToInt32(BudgetCountString);
            }
            catch { throw new Exception("Вы ввели некорректное число!"); }
            if (BudgetCount <= 0)
            {
                throw new Exception("Вы ввели некорректное число!");
            }
            budgetRepository.AddBudget(BudgetCount);
            BudgetPageVisible = false;
            HomePageVisible = true;
            OnPropertyChanged(nameof(BudgetPageVisible));
            OnPropertyChanged(nameof(HomePageVisible));
        }
        public void OnAddIncome()
        {
            try
            {
                BudgetCount = Convert.ToInt32(TransactionAmountString);
            }
            catch { throw new Exception("Вы ввели некорректное число!"); }
            if (TransactionAmount <= 0)
            {
                throw new Exception("Вы ввели некорректное число!");
            }
            incomeRepository.AddIncome(TransactionAmount, TransactionDescription);
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
            expenseRepository.AddExpense(TransactionAmount, TransactionDescription);
        }
    }
}
