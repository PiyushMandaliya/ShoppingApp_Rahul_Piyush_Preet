using ShoppingApp.Entities;
using ShoppingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Utility;
using Utility.Monads;

namespace ShoppingApp.ViewModel.AdminViewModel
{
    public class CategoryViewModel : ViewModel
    {

        private ICategoryService categoryService;
        public ObservableCollection<Category> Categories { get; }

        public event Action<string> addCategoryError;

        public DelegateCommand AddCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        


        private string categoryName;
        public string CategoryName
        {
            get => categoryName;
            set
            {
                categoryName = value;
                NotifyPropertyChanged(nameof(CategoryName));

            }
        }

        private Category selectedCategory;
        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                NotifyPropertyChanged(nameof(SelectedCategory));

            }
        }

        public CategoryViewModel(ICategoryService service)
        {
            categoryService = service;
            Categories = getCategoryList();
            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand(Delete);


        }

        private ObservableCollection<Category> getCategoryList()
        {
            List<Category> categories = this.categoryService.GetAllCategory();
            ObservableCollection<Category> allCategories = new ObservableCollection<Category>();
            foreach (Category category in categories)
                allCategories.Add(category);

            return allCategories;
        }

        private void Add(object _)
        {
            if (CanAddCategory())
            {
                Category category = new Category(CategoryName);
                Result result = categoryService.AddCategory(category);
                if (result.Successful)
                {
                    Categories.Add(category);
                    CategoryName = "";
                }
                else
                    addCategoryError.Invoke(result.ErrorMessage);

            }
        }

        private void Delete(object _)
        {
            if (selectedCategory == null)
                addCategoryError.Invoke("Please select category");
            else
            {
                Result result = categoryService.RemoveCategory(selectedCategory);
                if (result.Successful)
                {
                    Categories.Remove(selectedCategory);
                    selectedCategory = null;
                }
                else
                    addCategoryError.Invoke(result.ErrorMessage);

            }
        }

        private bool CanAddCategory()
        {
            if (string.IsNullOrWhiteSpace(CategoryName))
            {
                addCategoryError.Invoke("Please enter valid category name");
                return false;
            }
            return true;
        }
    }
}