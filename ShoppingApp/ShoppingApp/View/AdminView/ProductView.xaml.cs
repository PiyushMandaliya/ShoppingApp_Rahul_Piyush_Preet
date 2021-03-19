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
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        public ProductView(ProductViewModel productViewModel)
        {
            InitializeComponent();

            productViewModel.addProductError += showError;
            DataContext = productViewModel;
        }

        private void showError(string message)
        {
            MessageBox.Show(message);
        }
    }
}
