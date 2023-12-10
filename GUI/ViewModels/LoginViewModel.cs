using BLL.Interfaces;
using BLL.Services;
using DAL.Contexts;
using DAL.Interfaces;
using GUI.Commands;
using GUI.Services;
using GUI.Views;
using Microsoft.Extensions.DependencyInjection;
using RM_Project1.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        // Fields
        private IUserSVC?_userSVC;
        private EmailSender? _emailSender;

        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _newPass = string.Empty;
        private string _reNewPass = string.Empty;
        private string? _email;
        private string? _code;
        private UserControl? _childView;
        private LoginView? _loginView;
        private ResetPasswordView? _resetPasswordView;
        private ConfirmEmailView? confirmEmailView;

        private string? _usernameOrPassWrongNotification;
        private string? _emailWrongNotification;
        private string? _codeWrongNotification;
        private string? _passwordNotFitNotification;


        // Properties
        public string UserName
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string NewPass
        {
            get => _newPass;
            set
            {
                _newPass = value;
                OnPropertyChanged(nameof(NewPass));
            }
        }
        public string ReNewPass
        {
            get => _reNewPass;
            set
            {
                _reNewPass = value;
                OnPropertyChanged(nameof(ReNewPass));
            }
        }
        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string? Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged(nameof(Code));
            }
        }
        public UserControl? ChildView
        {
            get => _childView;
            set
            {
                _childView = value;
                OnPropertyChanged(nameof(ChildView));
            }
        }
        public LoginView? LoginView
        {
            get => _loginView;
            set
            {
                _loginView = value;
                OnPropertyChanged(nameof(LoginView));
            }
        }
        public ResetPasswordView? ResetPasswordView
        {
            get => _resetPasswordView;
            set
            {
                _resetPasswordView = value;
                OnPropertyChanged(nameof(ResetPasswordView));
            }
        }
        public ConfirmEmailView? ConfirmEmailView
        {
            get => confirmEmailView;
            set
            {
                confirmEmailView = value;
                OnPropertyChanged(nameof(ConfirmEmailView));
            }
        }
        public string? UsernameOrPassWrongNotification
        {
            get => _usernameOrPassWrongNotification;
            set
            {
                _usernameOrPassWrongNotification = value;
                OnPropertyChanged(nameof(UsernameOrPassWrongNotification));
            }
        }
        public string? EmailWrongNotification
        {
            get => _emailWrongNotification;
            set
            {
                _emailWrongNotification = value;
                OnPropertyChanged(nameof(EmailWrongNotification));
            }
        }
        public string? CodeWrongNotification
        {
            get => _codeWrongNotification;
            set
            {
                _codeWrongNotification = value;
                OnPropertyChanged(nameof(CodeWrongNotification));
            }
        }
        public string? PasswordNotFitNotification
        {
            get => _passwordNotFitNotification;
            set
            {
                _passwordNotFitNotification = value;
                OnPropertyChanged(nameof(PasswordNotFitNotification));
            }
        }


        // Commands
        public ICommand? LoginCommand { get; set; }
        public ICommand? ShowLoginViewCommand { get; set; }
        public ICommand? ShowResetPasswordViewCommand { get; set; }
        public ICommand? SendCodeCommand { get; set; }
        public ICommand? ConfirmEmailCommand { get; set; }
        public ICommand? UpdatePasswordCommand { get; set; }


        // Constructors
        public LoginViewModel()
        {
            UserName = String.Empty; 
            Password = String.Empty;

            var svc = ServiceConfiguration.GetLoginServiceProvider().GetService<IUserSVC>();
            if (svc != null)
                _userSVC = svc;

            LoginCommand = new RelayCommand(ExecuteLoginCommand);
            ShowLoginViewCommand = new RelayCommand(ExecuteShowLoginViewCommand);
            ShowResetPasswordViewCommand = new RelayCommand(ExecuteShowResetPasswordViewCommand);
            SendCodeCommand = new RelayCommand(ExecuteSendCodeCommand);
            ConfirmEmailCommand = new RelayCommand(ExecuteConfirmEmailCommand, CanExecuteConfirmEmailCommand);
            UpdatePasswordCommand = new RelayCommand(ExecuteUpdatePasswordCommand);

            ExecuteShowLoginViewCommand(null);
        }


        // Methods
        private void ExecuteUpdatePasswordCommand(object? obj)
        {
            if (_userSVC == null)
            {
                PasswordNotFitNotification = "Somethings went wrong! Try later!";
                return;
            }
            PasswordNotFitNotification = string.Empty;
            if (_emailSender == null)
                return;
            if (NewPass.Length == 0)
            {
                PasswordNotFitNotification = "Please enter new password";
                return;
            }
            if (NewPass != ReNewPass)
            {
                PasswordNotFitNotification = "Password is not fit each orther";
                return;
            }
            if (_userSVC.UpdatePassword(_emailSender.EmailTO, NewPass))
            {
                MessageBox.Show("Your password is updated!");
                ExecuteShowLoginViewCommand(null);
            }
            else
                MessageBox.Show("Error when update");
        }

        private bool CanExecuteConfirmEmailCommand(object? obj)
        {
            if (Code?.Length != 6)
                return false;
            if (_emailSender == null)
                return false;
            if (_emailSender.EmailTO != Email)
                return false;
            return true;    
        }

        private void ExecuteConfirmEmailCommand(object? obj)
        {
            CodeWrongNotification = string.Empty;
            if (Code == null)
                return;
            if (_emailSender?.CheckCode(Code) == false)
            {
                CodeWrongNotification = "The code was wrong or expired";
                return;
            }
            ChildView = new SetNewPasswordView();
        }

        private void ExecuteSendCodeCommand(object? obj)
        {
            if (_userSVC == null)
            {
                EmailWrongNotification = "Somethings went wrong! Try later!";
                return;
            }
            EmailWrongNotification = string.Empty;
            if (Email == null || Email.Length == 0)
                return;
            var username = _userSVC.GetUserNameByEmail(Email);
            if (username == null)
            {
                EmailWrongNotification = "No Accout fit your Email!";
                return;
            }
            UserName = username;
            _emailSender = new EmailSender(Email);
            ConfirmEmailView = new ConfirmEmailView();
            ChildView = ConfirmEmailView;
            MessageBox.Show(_emailSender.Code);
            MessageBox.Show(_emailSender.TimeCreate.ToString());
            MessageBox.Show(_emailSender.EmailTO.ToString());
        }

        private void ExecuteShowResetPasswordViewCommand(object? obj)
        {
            Email = string.Empty;
            EmailWrongNotification = string.Empty;
            ResetPasswordView = new ResetPasswordView();
            ChildView = ResetPasswordView;
        }

        private void ExecuteShowLoginViewCommand(object? obj)
        {
            UsernameOrPassWrongNotification = string.Empty;
            LoginView = new LoginView();
            ChildView = LoginView;
        }

        private void ExecuteLoginCommand(object? obj)
        {
            UsernameOrPassWrongNotification = string.Empty;
            if (_userSVC == null) return;
            var e = _userSVC.Login(UserName, Password);
            UserSection.SetCurrentUser(e);
            if (UserSection.Instance == null)
            {
                UsernameOrPassWrongNotification = "Username or Password was wrong";
                return;
            }
            UsernameOrPassWrongNotification = string.Empty;
            WindowManager.ShowMainWindow();
            WindowManager.CloseWindow(this);
        }

    }
}
