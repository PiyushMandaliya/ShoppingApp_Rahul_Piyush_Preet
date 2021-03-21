using ShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Utility.Authentication;

namespace ShoppingApp.Data
{
    //Author: Preet Rajesh Kansara
    public interface IUserRepository
    {
        User Get(string userName);
        User GetAdmin(string userName);

        bool Exists(long id);
        bool Exist(string type);
        bool Exists(string userName);
        void Add(User user);
        bool Update(User user);
    }

    //Author: Preet Rajesh Kansara

    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;
        public UserRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["shoppingappdb"].ConnectionString;
        }

        public void Add(User user)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "insert into dbo.Users " +
                "(DateCreated, DateModified, FirstName, LastName, UserName, SecurityQuestion, Answer, Type, Salt, Hash) " +
                "output inserted.Id " +
                "values(@DateCreated, @DateModified, @FirstName, @LastName, @UserName, @SelectedQuestion , @Answer, @Type, @Salt, " +
                "@Hash) ";

            user.DateCreated = user.DateModified = DateTime.UtcNow;

            command.Parameters.Add("@DateCreated", SqlDbType.DateTime2).Value = user.DateCreated;
            command.Parameters.Add("@DateModified", SqlDbType.DateTime2).Value = user.DateModified;
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = user.FirstName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = user.LastName;
            command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
            command.Parameters.Add("@SelectedQuestion", SqlDbType.NVarChar).Value = user.SelectedSecurityQuestion.ToString();
            command.Parameters.Add("@Answer", SqlDbType.NVarChar).Value = user.Answer;
            command.Parameters.Add("@Type", SqlDbType.NVarChar).Value = "user";
            command.Parameters.Add("@Salt", SqlDbType.VarBinary).Value = user.Salt;
            command.Parameters.Add("@Hash", SqlDbType.VarBinary).Value = user.Hash;

            user.Id = (long)command.ExecuteScalar();
        }

        public bool Update(User user)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "update dbo.Users " +
                "set DateModified = '@DateModified', FirstName = '@FirstName', LastName = '@LastName', UserName = '@UserName', " +
                "SecurityQuestion = '@SecurityQuestion', Answer = '@Answer', Type = '@Type', Salt = @Salt, " +
                "Hash = 0x7C7E4460B857A4DB6034D9FE990602CC144BCC703020E9273CFC68DB85C2F3A6 " +
                "where Id = @Id ";

            user.DateModified = DateTime.UtcNow;

            command.Parameters.Add("@DateCreated", SqlDbType.DateTime2).Value = user.DateCreated;
            command.Parameters.Add("@DateModified", SqlDbType.DateTime2).Value = user.DateModified;
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = user.FirstName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = user.LastName;
            command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
            command.Parameters.Add("@SelectedQuestion", SqlDbType.NVarChar).Value = user.SelectedSecurityQuestion;
            command.Parameters.Add("@Answer", SqlDbType.NVarChar).Value = user.Answer;
            command.Parameters.Add("@Salt", SqlDbType.VarBinary).Value = user.Salt;
            command.Parameters.Add("@Hash", SqlDbType.VarBinary).Value = user.Hash;

            int rowsAffected = command.ExecuteNonQuery();
            return (rowsAffected > 0);
        }

        public bool Exists(long id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText =
                "select count(UserName) from dbo.Users "
                + "where UserName = @UserName ";

            command.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;

            int count = (int)command.ExecuteScalar();
            return (count > 0);
        }

        public bool Exists(string userName)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText =
                "select count(UserName) from dbo.Users "
                + "where UserName = @UserName ";

            command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;

            int count = (int)command.ExecuteScalar();
            return (count > 0);
        }

        public bool Exist(string type)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText =
                "select count(Type) from dbo.Users "
                + "where Type = @Type ";

            command.Parameters.Add("@Type", SqlDbType.NVarChar).Value = type;

            int count = (int)command.ExecuteScalar();
            return (count > 0);
        }

        public User Get(string userName)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "select Id, DateCreated, DateModified, FirstName, LastName, UserName, SecurityQuestion, Answer Type, Salt, Hash from dbo.Users " +
                "where UserName = @UserName AND Type = @Type";

            command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = userName;

            command.Parameters.Add("@Type", SqlDbType.NVarChar).Value = "user";

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                return ExtractNextUser(reader);

            return null;
        }


        //Author: Piyushkumar Mandaliya
        public User GetAdmin(string userName)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "select Id, DateCreated, DateModified, FirstName, LastName, UserName, SecurityQuestion, Answer Type, Salt, Hash from dbo.Users " +
                "where UserName = @UserName AND Type = @Type";

            command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = userName;
            command.Parameters.Add("@Type", SqlDbType.NVarChar).Value = "admin";
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                return ExtractNextUser(reader);

            return null;
        }


        private User ExtractNextUser(SqlDataReader reader)
        {
            long id = reader.GetInt64(0);
            DateTime dateCreated = reader.GetDateTime(1);
            DateTime dateModified = reader.GetDateTime(2);
            string userName = reader.GetString(3);
            string firstName = reader.GetString(4);
            string lastName = reader.GetString(5);
            string selectedSecurityQuestion = reader.GetString(6);
            string answers = reader.GetString(7);
            byte[] salt = (byte[])reader.GetValue(8);
            byte[] hash = (byte[])reader.GetValue(9);
            PasswordHash password = new PasswordHash(salt, hash);

            return new User(userName, firstName, lastName, selectedSecurityQuestion, answers, password);
        }

    }
}
