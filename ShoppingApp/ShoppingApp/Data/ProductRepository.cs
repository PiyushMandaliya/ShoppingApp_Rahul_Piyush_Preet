using ShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ShoppingApp.Data
{

    //Author : Piyushkumar Mandaliya

    public interface IProductRepository

    {
        Product Get(long id);
        IList<Product> GetAll();
        void Add(Product product);
        bool Remove(Product product);
        bool Update(Product product);

    }

    class ProductRepository : BaseRepository, IProductRepository
    {

        private static readonly string SelectCommandCore =
        "SELECT TOP(1000) Id, DateCreated, DateModified, "
        + "CategoryId, Title, Description, Price, "
        + "InventoryCount,Title "
        + "FROM dbo.Products ";

        public ProductRepository() : base()
        {
        }

        public void Add(Product product)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText =
                "insert into dbo.Products " +
                "(DateCreated, DateModified, CategoryId, Title, Description, Price, InventoryCount) " +
                "OUTPUT INSERTED.Id " +
                "values(@DateCreated, @DateModified, @CategoryId, @Title, @Description, @Price, @InventoryCount)";

            product.DateCreated = product.DateModified = DateTime.UtcNow;

            command.Parameters.Add("@DateCreated", SqlDbType.DateTime2).Value = product.DateCreated;
            command.Parameters.Add("@DateModified", SqlDbType.DateTime2).Value = product.DateModified;
            command.Parameters.Add("@CategoryId", SqlDbType.BigInt).Value = product.CategoryId;
            command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = product.Title;
            command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = product.Description;
            command.Parameters.Add("@Price", SqlDbType.Decimal).Value = product.Price;
            command.Parameters.Add("@InventoryCount", SqlDbType.Int).Value = product.InventoryCount;

            product.Id = (long)command.ExecuteScalar();

        }

        public Product Get(long id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = SelectCommandCore + "WHERE Id = @Id ";
            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                return ExtractNextProduct(reader);

            return null;

        }

        public IList<Product> GetAll()
        {
            List<Product> categories = new List<Product>();
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "select dbo.Products.Id, dbo.Products.DateCreated, dbo.Products.DateModified, dbo.Products.CategoryId, dbo.Products.Title, dbo.Products.Description, " +
                "dbo.Products.Price, dbo.Products.InventoryCount, dbo.Category.Name FROM dbo.Products"
                + " INNER JOIN dbo.Category ON (CategoryId = dbo.Category.Id)";

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product product = ExtractNextProduct(reader);
                categories.Add(product);
            }

            return categories;
        }

        public bool Remove(Product product)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE from dbo.Products WHERE Id = @Id ";
            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = product.Id;

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        public bool Update(Product product)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "UPDATE dbo.Products " +
                "SET DateModified = @DateModified, " +
                "Title = @Title, " +
                "Description = @Description, " +
                "CategoryId = @CategoryId, " +
                "Price = @Price, " +
                "InventoryCount = @InventoryCount " +
                                 "WHERE Id = @Id ";

            product.DateModified = DateTime.UtcNow;

            command.Parameters.Add("@DateModified", SqlDbType.DateTime2).Value = product.DateModified;
            command.Parameters.Add("@CategoryId", SqlDbType.BigInt).Value = product.CategoryId;
            command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = product.Title;
            command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = product.Description;
            command.Parameters.Add("@Price", SqlDbType.Decimal).Value = product.Price;
            command.Parameters.Add("@InventoryCount", SqlDbType.Int).Value = product.InventoryCount;
            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = product.Id;

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;

        }

        private Product ExtractNextProduct(SqlDataReader reader)
        {
            long id = reader.GetInt64(0);
            DateTime dateCreated = reader.GetDateTime(1);
            DateTime dateModified = reader.GetDateTime(2);
            long categoryId = reader.GetInt64(3);

            string title = reader.GetString(4);
            string description = reader.GetString(5);
            decimal price = reader.GetDecimal(6);
            int inventoryCount = reader.GetInt32(7);
            string categoryName = reader.GetString(8);

            return new Product(id, dateCreated, dateModified,
                title, description, categoryId, price, inventoryCount, categoryName);
        }
    }
}
