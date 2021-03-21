

using System;
using System.ComponentModel;
using Utility.Entities;

namespace ShoppingApp.Entities
{
    public class Cart : Entity, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private long userId;
        public long UserId
        {
            set
            {
                userId = value;
                NotifyPropertyChanged(nameof(UserId));
            }
            get => userId;
        }

        private long productId;
        public long ProductId
        {
            set
            {
                productId = value;
                NotifyPropertyChanged(nameof(ProductId));
            }
            get => productId;
        }

        private int itemCount;
        public int ItemCount
        {
            set
            {
                itemCount = value;
                NotifyPropertyChanged(nameof(ItemCount));
            }
            get => itemCount;
        }

        private Product product;
        public Product Product
        {
            set
            {
                product = value;
                NotifyPropertyChanged(nameof(Product));

            }
            get => product;
        }

        public Cart(long userId, long productId, int itemCount)
            : this(default, default, default, userId, productId, itemCount)
        { }
        public Cart(long id, DateTime dateCreated, DateTime dateModified, long userId, long productId, int itemCount)
           : base(id, dateCreated, dateModified)
        {
            this.UserId = userId;
            this.ProductId = productId;
            this.ItemCount = itemCount;
        }

        public Cart(long id, DateTime dateCreated, DateTime dateModified, long userId, long productId, int itemCount, string productName, decimal price)
            : base(id, dateCreated, dateModified)
        {
            this.UserId = userId;
            this.ProductId = productId;
            this.ItemCount = itemCount;
            this.Product = new Product(productName, price);
        }
    }
}
