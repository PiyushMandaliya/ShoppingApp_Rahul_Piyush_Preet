using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Repository
{
    /// <summary>
    /// SQL Connection
    /// Close Connection
    /// Base DB Operations
    /// </summary>
    public class BaseRepository
    {
        public readonly string connectionString;

        public BaseRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["shoppingappdb"].ConnectionString;
        }

        void Insert() { }
        void Update() { }
        void GetAll() { }
        void GetById() { }
    }
}
