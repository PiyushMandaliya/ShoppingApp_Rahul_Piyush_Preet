using ShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ShoppingApp.Data
{

    //Author: Piyushkumar Mandaliya

    public interface ICategoryRepository
    {
        Category Get(long id);
        List<Category> GetAll();
        void Add(Category category);
        bool Remove(Category category);
        bool Update(Category category);
        bool Exists(string categoryName);

    }


    class CategoryRepository :BaseRepository, ICategoryRepository
    {

        public CategoryRepository()
        {
        }

        public void Add(Category category)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText =
                "insert into dbo.Category (DateCreated, DateModified, Name) "
                   + "OUTPUT INSERTED.Id "
                   + "values(@DateCreated, @DateModified, @Name)";
            category.DateCreated = category.DateModified = DateTime.UtcNow;
            command.Parameters.Add("@DateCreated", SqlDbType.DateTime2).Value = category.DateCreated;
            command.Parameters.Add("@DateModified", SqlDbType.DateTime2).Value = category.DateModified;

            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = category.CategoryName;
            category.Id = (long)command.ExecuteScalar();
        }

        public Category Get(long id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "select Id, DateCreated, DateModified, Name"
                + " FROM dbo.Category"
                + " WHERE Id = @Id ";
            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                return ExtractNextCategory(reader);
            return null;
        }

        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "select Id, DateCreated, DateModified, Name"
                + " FROM dbo.Category";

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Category category = ExtractNextCategory(reader);
                categories.Add(category);
            }

            return categories;
        }

        public bool Remove(Category category)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText =
                            "delete from dbo.Category "
                            + "where Id = @Id";

            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = category.Id;

            int rowsAffected = command.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        public bool Update(Category category)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();

            command.CommandText =
                "update dbo.Category "
                + "set Name = @Name, DateModified = @DateModified "
                + "where Id = @Id ";

            category.DateModified = DateTime.UtcNow;
            command.Parameters.Add("@DateModified", SqlDbType.DateTime2).Value = category.DateModified;
            command.Parameters.Add("@Name", SqlDbType.DateTime2).Value = category.CategoryName;

            int rowsAffected = command.ExecuteNonQuery();
            return (rowsAffected > 0);
        }



        private Category ExtractNextCategory(SqlDataReader reader)
        {
            long id = reader.GetInt64(0);
            DateTime dateCreated = reader.GetDateTime(1);
            DateTime dateModified = reader.GetDateTime(2);
            string Name = reader.GetString(3);

            return new Category(id, dateCreated, dateModified,Name);
        }


        public bool Exists(string categoryName)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText =
                "select count(Name) from dbo.Category "
                + "where Name = @CategoryName ";

            command.Parameters.Add("@CategoryName", SqlDbType.NVarChar).Value = categoryName;

            int count = (int)command.ExecuteScalar();
            return (count > 0);
        }


    }
}
