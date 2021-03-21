using System;
using System.Collections.Generic;
using System.Text;
using Utility;

namespace ShoppingApp.ViewModel
{
    public class MainMenuViewModel
    {
        public event Action AdminLoginAction, UserLoginAction;


        public DelegateCommand AdminLoginCommand { get; }
        public DelegateCommand UserLoginCommand { get; }

        public MainMenuViewModel()
        {
            AdminLoginCommand = new DelegateCommand(AdminLogin);
            UserLoginCommand = new DelegateCommand(UserLogin);
        }

        private void AdminLogin(object _)
        {
            AdminLoginAction?.Invoke();
        }

        private void UserLogin(object _)
        {
            UserLoginAction?.Invoke();
        }
    }
}
