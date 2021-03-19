﻿using ShoppingApp.ViewModel.AdminViewModel;
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
    /// Interaction logic for CategoryView.xaml
    /// </summary>
    public partial class CategoryView : Window
    {
        public CategoryView(CategoryViewModel viewModel)
        {
            InitializeComponent();

            viewModel.addCategoryError += addCategoryError;
            DataContext = viewModel;
        }

        private void addCategoryError(string ErrorMessage)
        {
            MessageBox.Show(ErrorMessage);
        }
    }
}
