using ShoppingApp.Entities;
using ShoppingApp.Services;
using ShoppingApp.View.AdminView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using Utility;
using Utility.Monads;

namespace ShoppingApp.ViewModel.AdminViewModel
{

    //Author: Piyushkumar Mandaliya
    public class ProductViewModel: ViewModel
    {

        public event Action<string> addProductError;
        private IProductService productService;

        public ObservableCollection<Product> Products { get; }

        public DelegateCommand AddCommand { get; }
        public DelegateCommand UpdateCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        public AddProductViewModel addProductViewModel { get; }

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                NotifyPropertyChanged(nameof(SelectedProduct));
            }
        }

        public ProductViewModel(ICategoryService categoryService,IProductService productService)
        {
            addProductViewModel = new AddProductViewModel(categoryService);
            this.productService = productService;
            Products = getProducts();

            AddCommand = new DelegateCommand(Add);
            UpdateCommand = new DelegateCommand(Update);
            DeleteCommand = new DelegateCommand(Delete);
        }

        private ObservableCollection<Product> getProducts()
        {
            IList<Product> productServiceData = this.productService.GetAllProducts();
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            foreach (Product product in productServiceData)
                products.Add(product);

            return products;
        }

        private void Add(Object _)
        {
            if (CanAddProduct())
            {
                Product product = new Product(addProductViewModel.Title,addProductViewModel.Description,addProductViewModel.SelectedCategory.Id,addProductViewModel.Price,addProductViewModel.InventoryCount);
                
                Result result = productService.AddProduct(product);
                if(result.Successful)
                {
                    Products.Add(product);
                    addProductViewModel.ClearDate();
                }
                else
                    addProductError.Invoke(result.ErrorMessage);
            }
        }

        private void Update(Object _)
        {
            if (SelectedProduct == null && canUpdateProduct())
                addProductError.Invoke("Please select employee for delete");
            else
            {
                if (canUpdateProduct())
                {
                    Result result = productService.UpdateProduct(SelectedProduct);
                    if (result.Successful)
                    {

                        addProductError.Invoke("Date Updated succesfully");
                    }
                    else
                        addProductError.Invoke(result.ErrorMessage);
                }
            }
        }
        private void Delete(Object _)
        {
            if (SelectedProduct == null)
                addProductError.Invoke("Please select employee for delete");
            else
            {
                Result result = productService.RemoveProduct(SelectedProduct);
                if (result.Successful)
                {
                    Products.Remove(SelectedProduct);
                    SelectedProduct = null;
                }
                else
                    addProductError.Invoke(result.ErrorMessage);
            }
        }

        private bool CanAddProduct()
        {
            return validateProductTitle(addProductViewModel.Title) && validateDescription(addProductViewModel.Description) && validateCategory(addProductViewModel.SelectedCategory) && validatePrice(addProductViewModel.Price) && validateInventory(addProductViewModel.InventoryCount);
        }


        private bool canUpdateProduct()
        {
            return validateProductTitle(SelectedProduct.Title) && validateDescription(SelectedProduct.Description) && validateCategory(SelectedProduct.Category) && validatePrice(SelectedProduct.Price) && validateInventory(SelectedProduct.InventoryCount);
        }

        private bool validateProductTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                addProductError.Invoke("Please enter valid title");
                return false;
            }
            return true;
        }

        private bool validateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                addProductError.Invoke("Please enter valid description");
                return false;
            }
            return true;
        }

        private bool validateCategory(Category category)
        {
            if(category == null)
            {
                addProductError.Invoke("Please select category");
                return false;
            }
            return true;
        }

        private bool validatePrice(decimal price)
        {
            if(price <= 0)
            {
                addProductError.Invoke("Please enter valid price");
                return false;
            }
            return true;
        }
        private bool validateInventory(int ItemCount)
        {
            if (ItemCount <= 0)
            {
                addProductError.Invoke("Please enter valid item count");
                return false;
            }
            return true;
        }
    }
}
