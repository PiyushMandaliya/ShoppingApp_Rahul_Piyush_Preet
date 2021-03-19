

using System;
using System.ComponentModel;
using Utility.Entities;

namespace ShoppingApp.Entities
{
    public class Product: Entity, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private long categoryId;
        public long CategoryId
        {
            set
            {
                categoryId = value;
                NotifyPropertyChanged(nameof(CategoryId));
            }
            get => categoryId;
        }

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


        private Category category;
        public Category Category
        {
            set
            {
                category = value;
                NotifyPropertyChanged(nameof(Category));

            }
            get => category;
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

        private string categoryName;
        public string CategoryName
        {
            set
            {
                categoryName = value;
                NotifyPropertyChanged(nameof(CategoryName));
            }
            get => categoryName;
        }

        public Product() { }

        public Product(string title, string description, long categoryId, decimal price,
                         int inventoryCount)
            : this(default, default, default,  title, description, categoryId, price, inventoryCount)
        { }

        public Product(long id, DateTime dateCreated,  DateTime dateModified, string title,   string description, long categoryId, decimal price,  int inventoryCount)
            : base(id, dateCreated, dateModified)
        {
            this.CategoryId = categoryId;
            this.Title = title;
            this.Description = description;
            this.Price = price;
            this.InventoryCount = inventoryCount;
            this.categoryName = "";
        }

        public Product(long id, DateTime dateCreated, DateTime dateModified, string title, string description, long categoryId, decimal price, int inventoryCount, string categoryName)
            : base(id, dateCreated, dateModified)
        {
            this.CategoryId = categoryId;
            Category = new Category(categoryId,categoryName);
            this.Title = title;
            this.Description = description;
            this.Price = price;
            this.InventoryCount = inventoryCount;
            this.CategoryName = categoryName;
        }
    }
}
