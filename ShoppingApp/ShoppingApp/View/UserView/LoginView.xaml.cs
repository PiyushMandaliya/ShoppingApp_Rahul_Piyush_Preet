using ShoppingApp.Data;
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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    /// 
    
    public partial class LoginView
    {
        private IUserService UserService;
        public LoginView(IUserService userService)
        {
            this.UserService = userService;
            InitializeComponent();
            LoginViewModel loginViewModel = new LoginViewModel(userService);
            loginViewModel.LogInError += showErrorMessage;
            loginViewModel.SignupAction += OpenRegistrationWindow;
            loginViewModel.LoginSuccessfullAction += OpenProductWindow;
            DataContext = loginViewModel;
        }

        public void showErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        private void OpenRegistrationWindow()
        {
            Window window = new RegistrationView(UserService);
            window.Show();
            this.Close();
        }

        private void OpenProductWindow(long userId)
        {
            ProductRepository productRepository = new ProductRepository();
            ProductService productService = new ProductService(productRepository);
            CartService cartService = new CartService();
            ProductViewModel productViewModel =  new ProductViewModel(productService, cartService,userId);
            ProductsView productsView = new ProductsView(productViewModel);
            productsView.Show();
            this.Close();
        }

    }
}
