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
    public class ProductViewModel: ViewModel
    {

        public event Action<string> addProductError;
        private IProductService productService;

        public ObservableCollection<Product> Products { get; }

        public DelegateCommand AddCommand { get; }

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

        private bool CanAddProduct()
        {
            return validateProductTitle() && validateDescription() && validateCategory() && validatePrice() && validateInventory();
        }


        private bool validateProductTitle()
        {
            if (string.IsNullOrWhiteSpace(addProductViewModel.Title))
            {
                addProductError.Invoke("Please enter valid title");
                return false;
            }
            return true;
        }

        private bool validateDescription()
        {
            if (string.IsNullOrWhiteSpace(addProductViewModel.Description))
            {
                addProductError.Invoke("Please enter valid description");
                return false;
            }
            return true;
        }

        private bool validateCategory()
        {
            if(addProductViewModel.SelectedCategory == null)
            {
                addProductError.Invoke("Please select category");
                return false;
            }
            return true;
        }

        private bool validatePrice()
        {
            if(addProductViewModel.Price <= 0)
            {
                addProductError.Invoke("Please enter valid price");
                return false;
            }
            return true;
        }
        private bool validateInventory()
        {
            if (addProductViewModel.InventoryCount <= 0)
            {
                addProductError.Invoke("Please enter valid item count");
                return false;
            }
            return true;
        }
    }
}
