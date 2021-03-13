using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Repository
{
    class UserRepository : BaseRepository
    {
        private readonly string connectionString;
        public UserRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["shoppingappdb"].ConnectionString;
        }

        public List<User> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {

                    command.CommandText = "select * from dbo.Users";

                    SqlDataReader reader = command.ExecuteReader();
                    List<User> users = new List<User>();

                    while (reader.Read())
                    {
                        User user = ExtractNextBook(reader);
                        users.Add(user);
                    }

                    return users;
                }
            }
        }
        private User ExtractNextBook(SqlDataReader reader)
        {
            long id = reader.GetInt64(0);
            string firstName = reader.GetString(1);
            string lastName = reader.GetString(2);
            return new User(id, firstName, lastName);
        }
    }
}
