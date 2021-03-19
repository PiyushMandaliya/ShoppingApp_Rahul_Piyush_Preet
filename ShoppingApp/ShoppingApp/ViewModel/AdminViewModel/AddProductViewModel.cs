using ShoppingApp.Entities;
using ShoppingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace ShoppingApp.ViewModel.AdminViewModel
{
    public class AddProductViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        private Category selectedCategory;
        public Category SelectedCategory
        {
            set
            {
                selectedCategory = value;
                NotifyPropertyChanged(nameof(SelectedCategory));
            }
            get => selectedCategory;
        }

        public ObservableCollection<Category> CategoryList { get; }


        private string title;
        public string Title
        {
            set
            {
                title = value;
                NotifyPropertyChanged(nameof(Title));
            }
            get => title;
        }


        private string description;
        public string Description
        {
            set
            {
                description = value;
                NotifyPropertyChanged(nameof(Description));
            }
            get => description;
        }

        private decimal price;
        public decimal Price
        {
            set
            {
                price = value;
                NotifyPropertyChanged(nameof(Price));
            }
            get => price;
        }

        private int inventoryCount;
        public int InventoryCount
        {
            set
            {
                inventoryCount = value;
                NotifyPropertyChanged(nameof(InventoryCount));
            }
            get => inventoryCount;
        }

        private ICategoryService categoryService;


        public AddProductViewModel(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            CategoryList = getCategories();

        }

        private ObservableCollection<Category> getCategories( )
        {
            List<Category> categories = categoryService.GetAllCategory();
            ObservableCollection<Category> allCategories = new ObservableCollection<Category>();
            foreach (Category category in categories)
                allCategories.Add(category);

            return allCategories;
        }

        public void ClearDate()
        {
            Title = null;
            SelectedCategory = null;
            Description = null;
            Price = 0;
            InventoryCount = 0;
        }
    }
}