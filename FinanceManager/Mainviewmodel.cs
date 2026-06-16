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

namespace FinanceManager.MainViewModel
{
    public class MainViewModel
    {
        private readonly UserRepository userRepository = new UserRepository();
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private int _userId;
        private string _login;
        private string _password;
        private string _email;
        private bool _homePageVisible;
        private bool _registerPageVisible;
        private bool _loginPageVisible;

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
            get { return _homePageVisible; }
            set
            {
                _homePageVisible = value;
                OnPropertyChanged();
            }
        }
        public bool RegisterPageVisible
        {
            get { return _homePageVisible; }
            set
            {
                _homePageVisible = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand LoginCommand { get; }
        public RelayCommand RegisterCommand { get; }
        public MainViewModel()
        {
            LoginCommand = new RelayCommand(OnLogin, () => CanLogin);
            RegisterCommand = new RelayCommand(OnRegister, () => CanRegister);
        }
        public bool CanLogin => !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        public bool CanRegister => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Login);
        public void OnLogin()
        {
            userRepository.Login(Email, Password);
            HomePageVisible = true;
            LoginPageVisible = false;
            OnPropertyChanged(nameof(HomePageVisible));
            OnPropertyChanged(nameof(LoginPageVisible));
        }
        public void OnRegister()
        {
            userRepository.Register(Login, Password, Email);
            HomePageVisible = true;
            RegisterPageVisible = false;
            OnPropertyChanged(nameof(HomePageVisible));
            OnPropertyChanged(nameof(RegisterPageVisible));

        }
    }
}
