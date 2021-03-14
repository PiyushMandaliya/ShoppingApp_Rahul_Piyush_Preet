using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Models
{
    public class Admin
    {
        long AdminId;
        string userName;
        string FirstName;
        string LastName;
        string Password;

        public Admin(long AdminId, string userName, string FirstName, string LastName, string Password)
        {
            this.AdminId = AdminId;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.userName = userName;
            this.Password = Password;
        }

    }
}
