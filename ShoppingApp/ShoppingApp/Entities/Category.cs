using System;
using System.ComponentModel;
using Utility.Entities;

namespace ShoppingApp.Entities
{
    //Author : Piyushkumar Mandaliya
    public class Category: Entity, INotifyPropertyChanged
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

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

        public Category(string categoryName)
            : this(default,default,default,categoryName)
        {

        }

        public Category(long id, DateTime dateCreated, DateTime dateModified, string categoryName):
            base(id,dateCreated,dateModified)
        {
            CategoryName = categoryName;
        }

        public Category(long id,  string categoryName)
        {
            this.Id = id;
            CategoryName = categoryName;
        }

    }
}
