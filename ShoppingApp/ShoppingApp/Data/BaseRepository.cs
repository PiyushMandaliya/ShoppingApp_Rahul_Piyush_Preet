using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ShoppingApp.Data
{
    /// <summary>
    /// Author: Rahul Mistry
    /// </summary>
    public abstract class BaseRepository
    {
        protected readonly string connectionString;

        public BaseRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["shoppingappdb"].ConnectionString;
        }
    }
}
