using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace ShoppingApp.Repository.AdminRepository
{
    class AdminRepository : BaseRepository
    {
        Admin admin;

        public AdminRepository() : base()
        {

        }

        public bool Login(string userName, string password)
        {
            Admin admin = Get(userName,password);
            if (admin != null)
                return true;
            return false;
        }


        public Admin Get(string userName, string password)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT AdminId, UserName, FirstName, LastName, Password " +
                "FROM Admin " + 
                " where UserName = @UserName AND Password = @Password";
            command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
            command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return ExtractNextAdmin(reader);
            }
            return null;
        }


        private Admin ExtractNextAdmin(SqlDataReader reader)
        {

            long id = reader.GetInt64(0);
            string userName = reader.GetString(1);
            string firstName = reader.GetString(2);
            string lastName = reader.GetString(3);
            string password = reader.GetString(4);

            return new Admin(id, userName, firstName, lastName, password);
        }
    }
}
