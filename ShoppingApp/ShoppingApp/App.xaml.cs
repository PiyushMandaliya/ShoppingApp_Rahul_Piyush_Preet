using ShoppingApp.Data;
using ShoppingApp.Services;
using ShoppingApp.View;
using ShoppingApp.View.AdminView;
using ShoppingApp.View.UserView;
using ShoppingApp.ViewModel.UserViewModel;
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
            Window window = new MainMenuView();
            window.Show();
        }
    }
}
