using ShoppingApp.Data;
using ShoppingApp.Services;
using ShoppingApp.ViewModel.AdminViewModel;
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

namespace ShoppingApp.View.AdminView
{
    /// <summary>
    /// Interaction logic for AdminLoginView.xaml
    /// </summary>
    public partial class AdminLoginView : Window
    {
        public AdminLoginView()
        {
            InitializeComponent();
            DataContext = createViewModel();
        }


        private AdminLoginViewModel createViewModel()
        {
            UserRepository userRepository = new UserRepository();
            UserService userService = new UserService(userRepository);
            AdminLoginViewModel adminLoginViewModel = new AdminLoginViewModel(userService);
            adminLoginViewModel.LogInError += showErrorMessage;
            adminLoginViewModel.LoginSuccessfullAction += OpenAdminMenuWindow;
            return adminLoginViewModel;
        }

        public void showErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        private void OpenAdminMenuWindow()
        {
            Window window = new AdmiMenuView();
            window.Show();
            this.Close();
        }
    }
}
