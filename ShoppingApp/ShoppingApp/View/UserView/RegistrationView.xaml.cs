using ShoppingApp.Services;
using ShoppingApp.ViewModel.UserViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShoppingApp.View.UserView
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView
    {
        private IUserService UserService;
        public RegistrationView(IUserService userService)
        {
            UserService = userService;
            InitializeComponent();
            RegistrationViewModel registrationViewModel = new RegistrationViewModel(userService);
            registrationViewModel.RegistrationError += showErrorMessage;
            registrationViewModel.LogInAction += OpenLogIn;
            DataContext = registrationViewModel;
        }

        public void showErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        private void OpenLogIn()
        {
            Window window = new LoginView(UserService);
            window.Show();
            this.Close();
        }
    }
}
