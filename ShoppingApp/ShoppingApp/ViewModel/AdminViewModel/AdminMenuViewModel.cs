using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Utility;

namespace ShoppingApp.ViewModel.AdminViewModel
{

    //Author: Piyushkumar Mandaliya
    public class AdminMenuViewModel
    {
        public event Action manageCategoryAction, manageProductAction, logoutAction;

        public DelegateCommand ManageCategoryCommand { get; }
        public DelegateCommand ManageProductCommand { get; }
        public DelegateCommand LogoutCommand { get; }


        public AdminMenuViewModel()
        {
            ManageCategoryCommand = new DelegateCommand(ManageCategory);
            ManageProductCommand = new DelegateCommand(ManageProduct);
            LogoutCommand = new DelegateCommand(Logout);
        }

        private void ManageCategory(object _)
        {
            manageCategoryAction?.Invoke();
        }

        private void ManageProduct(object _)
        {
            manageProductAction?.Invoke();
        }
        
        private void Logout(object _)
        {
            logoutAction?.Invoke();
        }
    }
}
