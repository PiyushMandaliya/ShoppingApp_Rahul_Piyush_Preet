using ShoppingApp.Entities;
using ShoppingApp.Services;
using ShoppingApp.UserViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Utility;
using Utility.Monads;

namespace ShoppingApp.ViewModel.UserViewModel
{
    /// <summary>
    /// Author: Rahul Mistry
    /// </summary>
    public class ProductViewModel : ViewModel
    {
        const long userId = 1000;

        public event Action<string> addMessage;

        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        #region Product
        public ObservableCollection<Product> Products
        {
            get
            {
                return getProducts();
            }
        }

        public DelegateCommand AddToCartCommand { get; }

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
        #endregion

        #region Cart
        private CartViewModel cart;
        public CartViewModel Cart
        {
            get => cart;
            set
            {
                cart = value;
                NotifyPropertyChanged(nameof(Cart));
            }
        }
        #endregion

        private int qty;
        public int Qty
        {
            get => qty;
            set
            {
                qty = value;
                NotifyPropertyChanged(nameof(Qty));
            }
        }

        public ProductViewModel(IProductService productService, ICartService cartService)
        {
            this._productService = productService;
            this._cartService = cartService;

            Cart = new CartViewModel(cartService);
            AddToCartCommand = new DelegateCommand(AddToCart);
        }

        private ObservableCollection<Product> getProducts()
        {
            IList<Product> productServiceData = this._productService.GetAllProducts();
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            foreach (Product product in productServiceData)
                products.Add(product);

            return products;
        }

        private void AddToCart(Object _)
        {
            if (SelectedProduct == null)
            {
                addMessage.Invoke("Please select product");
                return;
            }
            if (selectedProduct.InventoryCount <= 0)
            {
                addMessage.Invoke("Product not available");
            }

            if (Qty >= SelectedProduct.InventoryCount)
            {
                addMessage.Invoke("QTY should not be greater than product's available QTY");
                return;
            }
            if (CanAddToCart())
            {
                Cart cart = new Cart(UserContext.LoggedinUser.Id, SelectedProduct.Id, Qty);
                Result result = _cartService.AddCart(cart);
                if (result.Successful)
                {
                    Qty = 0;
                    NotifyPropertyChanged(nameof(Qty));
                    NotifyPropertyChanged(nameof(SelectedProduct));
                    NotifyPropertyChanged(nameof(Cart));
                    NotifyPropertyChanged(nameof(Products));
                    addMessage.Invoke("Product added to cart");
                }
                else
                    addMessage.Invoke(result.ErrorMessage);
            }
        }

        private bool CanAddToCart()
        {
            return validateQty(Qty);
        }
        private bool validateQty(long qty)
        {
            if (qty == decimal.Zero)
            {
                addMessage.Invoke("Please enter valid QTY");
                return false;
            }
            return true;
        }
    }
}
