using ShoppingApp.Entities;
using ShoppingApp.Extension;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ShoppingApp.Data
{

    /// <summary>
    /// Author: Rahul Mistry
    /// </summary>

    public interface ICartRepository

    {
        Cart Get(long id);
        Cart GetByProductId(long productId);
        IList<Cart> GetAll(long userId);
        void Add(Cart Cart);
        bool Remove(Cart Cart);
        bool Update(Cart Cart);

    }

    class CartRepository : BaseRepository, ICartRepository
    {

        private static readonly string SelectCommandCore = "SELECT Id, DateCreated, DateModified,UserId, ProductId, ItemCount FROM dbo.Cart ";

        public CartRepository() : base()
        {
        }

        public void Add(Cart Cart)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText =
                "insert into dbo.Cart " +
                "(DateCreated, DateModified, UserId, ProductId, ItemCount) " +
                "OUTPUT INSERTED.Id " +
                "values(@DateCreated, @DateModified, @UserId, @ProductId, @ItemCount)";

            Cart.DateCreated = Cart.DateModified = DateTime.UtcNow;

            command.Parameters.Add("@DateCreated", SqlDbType.DateTime2).Value = Cart.DateCreated;
            command.Parameters.Add("@DateModified", SqlDbType.DateTime2).Value = Cart.DateModified;
            command.Parameters.Add("@UserId", SqlDbType.BigInt).Value = Cart.UserId;
            command.Parameters.Add("@ProductId", SqlDbType.BigInt).Value = Cart.ProductId;
            command.Parameters.Add("@ItemCount", SqlDbType.BigInt).Value = Cart.ItemCount;

            Cart.Id = (long)command.ExecuteScalar();

        }

        public Cart Get(long id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = SelectCommandCore + " WHERE Id = @Id ";
            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                return ExtractNextCart(reader);

            return null;

        }

        public Cart GetByProductId(long productId)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = SelectCommandCore + "WHERE ProductId = @productId ";
            command.Parameters.Add("@productId", SqlDbType.BigInt).Value = productId;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                return ExtractNextCart(reader);

            return null;

        }

        public IList<Cart> GetAll(long userId)
        {
            List<Cart> categories = new List<Cart>();
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "select dbo.Cart.Id, dbo.Cart.DateCreated, dbo.Cart.DateModified, dbo.Cart.UserId,dbo.Cart.ProductId,dbo.Cart.ItemCount, " +
                "dbo.Products.Title,dbo.Products.Price FROM dbo.Cart"
                + " INNER JOIN dbo.Products ON (dbo.Cart.ProductId = dbo.Products.Id) WHERE dbo.Cart.UserId = @UserId";
            command.Parameters.Add("@UserId", SqlDbType.BigInt).Value = userId;

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Cart Cart = ExtractNextCart(reader);
                categories.Add(Cart);
            }

            return categories;
        }

        public bool Remove(Cart Cart)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE from dbo.Cart WHERE Id = @Id ";
            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = Cart.Id;

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        public bool Update(Cart Cart)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "UPDATE dbo.Cart " +
                "SET DateModified = @DateModified, " +
                "UserId = @UserId, " +
                "ProductId = @ProductId, " +
                "ItemCount = @ItemCount " +
                                 "WHERE Id = @Id ";

            Cart.DateModified = DateTime.UtcNow;

            command.Parameters.Add("@DateModified", SqlDbType.DateTime2).Value = Cart.DateModified;
            command.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = Cart.UserId;
            command.Parameters.Add("@ProductId", SqlDbType.NVarChar).Value = Cart.ProductId;
            command.Parameters.Add("@ItemCount", SqlDbType.Int).Value = Cart.ItemCount;
            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = Cart.Id;

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;

        }

        private Cart ExtractNextCart(SqlDataReader reader)
        {
            long id = reader.GetInt64(0);
            DateTime dateCreated = reader.GetDateTime(1);
            DateTime dateModified = reader.GetDateTime(2);
            long userId = reader.GetInt64(3);
            long productId = reader.GetInt64(4);
            int itemCount = reader.GetInt32(5);

            string title = DataRecordExtensions.HasColumn(reader, "Title") ? reader.GetString(6) : string.Empty;
            decimal price = DataRecordExtensions.HasColumn(reader, "Price") ? reader.GetDecimal(7):decimal.Zero;

            return new Cart(id, dateCreated, dateModified, userId, productId, itemCount, title, price);
        }
    }
}

