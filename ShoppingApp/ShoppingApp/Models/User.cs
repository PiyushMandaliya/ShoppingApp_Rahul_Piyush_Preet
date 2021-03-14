using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Models
{

    /// <summary>
    /// Fields should be same as database
    /// </summary>
    class User
    {
        public long Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Country { get;}
        public string City { get; }
        public DateTime DateOfBirth { get;}

        public User(long id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }


    }
}
