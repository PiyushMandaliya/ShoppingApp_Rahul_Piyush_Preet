using ShoppingApp.Entities;
using ShoppingApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using Utility.Monads;

namespace ShoppingApp.ViewModel.AdminViewModel
{

    //Author: Piyushkumar Mandaliya
    public class AdminLoginViewModel: ViewModel
    {
        private IUserService userService;
        public event Action<string> LogInError;
        public event Action LoginSuccessfullAction;
        public DelegateCommand LoginCommand { get; }


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

        public AdminLoginViewModel(IUserService userService)
        {
            this.userService = userService;
            LoginCommand = new DelegateCommand(Login);
        }

        private void Login(object _)
        {
            if (isLoginSuccessful())
            {
                Result result = userService.AdminLogin(UserName, Password);
                if (result.Successful)
                {
                    LoginSuccessfullAction?.Invoke();

                }
                else
                    LogInError?.Invoke(result.ErrorMessage);
            }
        }

        private bool isLoginSuccessful()
        {
            return ValidateUserName() && ValidatePassword();
        }

        private bool ValidateUserName()
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                LogInError?.Invoke("Please enter UserName");
                return false;
            }
            return true;
        }

        private bool ValidatePassword()
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
