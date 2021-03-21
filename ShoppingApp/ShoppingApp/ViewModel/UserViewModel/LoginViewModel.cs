using ShoppingApp.Entities;
using ShoppingApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using Utility.Monads;

namespace ShoppingApp.ViewModel.UserViewModel
{
    //Author: Preet Rajesh Kansara

    public class LoginViewModel : ViewModel
    {
        private IUserService userService;
        public event Action<string> LogInError;
        public event Action SignupAction;
        public event Action<long> LoginSuccessfullAction;

        public DelegateCommand LoginCommand { get; }
        public DelegateCommand SignUpCommand { get; }
        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                NotifyPropertyChanged(nameof(userName));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                NotifyPropertyChanged(nameof(password));
            }
        }

        public LoginViewModel(IUserService userService)
        {
            this.userService = userService;
            LoginCommand = new DelegateCommand(Login);
            SignUpCommand = new DelegateCommand(SignupPage);
        }

        private void Login(object _)
        {
            if (isLoginSuccessful())
            {
                Result<User> result = userService.LogIn(userName, password);
                if (result.Successful)
                {
                    long userId = result.Data.Id;
                    LoginSuccessfullAction?.Invoke(userId);
                
                }
                else
                    LogInError?.Invoke(result.ErrorMessage);
            }
        }
        

        

        private void SignupPage(object _)
        {
            SignupAction?.Invoke();
        }

        private bool isLoginSuccessful()
        {
            return IsValidateUserName() && IsValidatePassword();
        }

        private bool IsValidateUserName()
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                LogInError?.Invoke("Please enter UserName");
                return false;
            }
            return true;
        }

        private bool IsValidatePassword()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                LogInError?.Invoke("Please enter password!");
                return false;
            }
            return true;
        }
    }
}
