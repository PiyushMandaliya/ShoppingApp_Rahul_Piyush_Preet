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
        public ProductsView(ProductViewModel productViewModel)
        {
            InitializeComponent();
            UserContext.LoggedinUser = new UserViewModel.UserViewModel(10000, "Rahul", "Mistry");
            productViewModel.addMessage += showMessage;
            productViewModel.Cart.addMessage += showMessage;
            DataContext = productViewModel;
        }

        private void showMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
