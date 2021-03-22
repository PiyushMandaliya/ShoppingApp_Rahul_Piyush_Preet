using ShoppingApp.UserViewModel;
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
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : Window
    {
        public ProductsView()
        {
        }

        public ProductsView(ProductViewModel productViewModel)
        {
            InitializeComponent();
            productViewModel.addMessage += showMessage;
            productViewModel.Cart.addMessage += showMessage;
            productViewModel.logout += logoutHandler;
            DataContext = productViewModel;
        }

        private void showMessage(string message)
        {
            MessageBox.Show(message);
        }
        private void logoutHandler(bool isLogout)
        {
            if (isLogout)
            {
                this.Hide();
                UserContext.LoggedinUser = null;
                Window window = new MainMenuView();
                window.Show();
            }
        }
    }
}
