using ShoppingApp.Entities;
using ShoppingApp.Services;
using ShoppingApp.UserViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Utility;
using Utility.Monads;

namespace ShoppingApp.ViewModel.UserViewModel
{
    /// <summary>
    /// Author: Rahul Mistry
    /// </summary>
    public class CartViewModel : ViewModel
    {
        public event Action<string> addMessage;


        private ICartService cartService;
        public DelegateCommand RemoveCommand { get; }
        public DelegateCommand CheckoutCommand { get; }


        private ObservableCollection<Cart> items;

        public ObservableCollection<Cart> Items
        {
            get
            {
                return getCartList();
            }
            set
            {
                items = value;
                NotifyPropertyChanged(nameof(Items));
            }
        }

        private Cart selectedItem;
        public Cart SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                NotifyPropertyChanged(nameof(SelectedItem));
            }
        }


        public CartViewModel(ICartService cartService)
        {
            this.cartService = cartService;
            RemoveCommand = new DelegateCommand(Remove);
            CheckoutCommand = new DelegateCommand(Checkout);
        }

        public decimal TotalPrice
        {
            get
            {
                decimal total = decimal.Zero;
                foreach (var item in Items)
                {
                    total += item.Product.Price * item.ItemCount;
                }
                return total;
            }
        }
        public decimal TotalItems
        {
            get
            {
                return Items.Count;
            }
        }
        public decimal TotalQty
        {
            get
            {
                decimal total = decimal.Zero;
                foreach (var item in Items)
                {
                    total += item.ItemCount;
                }
                return total;
            }
        }
        private ObservableCollection<Cart> getCartList()
        {
            IList<Cart> carts = this.cartService.GetAllCarts(UserContext.LoggedinUser.Id);
            ObservableCollection<Cart> allCarts = new ObservableCollection<Cart>();
            foreach (Cart cart in carts)
                allCarts.Add(cart);

            return allCarts;
        }

        private void Checkout(Object _)
        {
            if (Items.Count <= 0)
            {
                addMessage?.Invoke("Cart empty");
                return;
            }

            IList<Cart> carts = cartService.GetAllCarts(UserContext.LoggedinUser.Id);
            foreach (var cart in carts)
            {
                cartService.RemoveCart(cart);
            }
            notifyChange();
            addMessage?.Invoke("Checkout done");
        }

        private void Remove(Object _)
        {
            if (SelectedItem == null)
            {
                addMessage?.Invoke("Please select cart item");
                return;
            }
            Result result = cartService.RemoveCart(SelectedItem);
            if (result.Successful)
            {
                notifyChange();
                addMessage?.Invoke("Cart item removed");
            }
            else
                addMessage?.Invoke(result.ErrorMessage);
        }

        private void notifyChange()
        {
            NotifyPropertyChanged(nameof(Items));
            NotifyPropertyChanged(nameof(TotalQty));
            NotifyPropertyChanged(nameof(TotalItems));
            NotifyPropertyChanged(nameof(TotalPrice));
        }
    }
}
