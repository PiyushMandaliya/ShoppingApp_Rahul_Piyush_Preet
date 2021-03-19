using ShoppingApp.Data;
using ShoppingApp.Services;
using ShoppingApp.View.AdminView;
using ShoppingApp.ViewModel.AdminViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShoppingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CategoryRepository categoryRepository = new CategoryRepository();
            CategoryService categoryService = new CategoryService(categoryRepository);
            CategoryViewModel categoryViewModel = new CategoryViewModel(categoryService);
            Window window = new CategoryView(categoryViewModel);
            //window.Show();



            ProductRepository productRepository = new ProductRepository();
            ProductService productService = new ProductService(productRepository);

            ProductViewModel productViewModel = new ProductViewModel(categoryService,productService);
            
            ((Window)new ProductView(productViewModel)).Show();
        }
    }
}
