using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingApp.UserViewModel
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public UserViewModel(long id, string firstName, string lastName)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }

    /// <summary>
    /// Author: Rahul Mistry
    /// </summary>
    public static class UserContext
    {
        public static UserViewModel LoggedinUser { get; set; }
    }
}
