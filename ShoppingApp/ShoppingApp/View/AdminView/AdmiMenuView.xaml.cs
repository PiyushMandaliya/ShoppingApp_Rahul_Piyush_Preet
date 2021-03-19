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
    public partial class AdmiMenuView : Window
    {

        CategoryRepository categoryRepository;
        CategoryService categoryService;
        ProductRepository productRepository;
        ProductService productService;

        public AdmiMenuView()
        {
            InitializeComponent();
            setInitialData();
            DataContext = CreateViewModel();
        }

        private AdminMenuViewModel CreateViewModel()
        {
            AdminMenuViewModel adminMenuViewModel = new AdminMenuViewModel();
            adminMenuViewModel.orderHistoryAction += OpenOrderHistoryWindow;
            adminMenuViewModel.manageCategoryAction += OpenManageCategoryWindow;
            adminMenuViewModel.manageProductAction += OpenManageProductWindow;
            adminMenuViewModel.logoutAction += OpenLoginWindow;
            return adminMenuViewModel;
        }

        private void setInitialData()
        {
            categoryRepository = new CategoryRepository();
            categoryService = new CategoryService(categoryRepository);

            productRepository = new ProductRepository();
            productService = new ProductService(productRepository);
        }

        private void OpenLoginWindow()
        {

        }

        private void OpenManageCategoryWindow()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel(categoryService);
            Window window = new CategoryView(categoryViewModel);
            window.Show();
        }
        private void OpenManageProductWindow()
        {

            ProductViewModel productViewModel = new ProductViewModel(categoryService, productService);
            Window window = new ProductView(productViewModel);
            window.Show();
        }

        private void OpenOrderHistoryWindow()
        {

        }
    }
}
