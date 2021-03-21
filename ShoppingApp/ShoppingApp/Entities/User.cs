using System;
using Utility.Authentication;
using Utility.Entities;




namespace ShoppingApp.Entities
{
    //Author: Preet Rajesh Kansara
    public class User:  Entity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SelectedSecurityQuestion { get; set; }
        public string Answer { get; set; }
        public PasswordHash Password { get; set; }
        public byte[] Salt => Password.Salt;
        public byte[] Hash => Password.Hash;

        public User(string userName, string firstName, string lastName, string selectedSecurityQuestion, string answer, PasswordHash password)
            : this(default, default, default, userName, firstName, lastName, selectedSecurityQuestion, answer, password) { }


        public User(long id, DateTime dateCreated, DateTime dateModified, string username, PasswordHash password)
            : base(id, dateCreated, dateModified)
        {
            UserName = username;
            Password = password;
        }


        public User(long id, DateTime dateCreated, DateTime dateModified, string userName, string firstName, string lastName, string selectedSecurityQuestion, string answer, PasswordHash password)
            : base(id, dateCreated, dateModified)
        {
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            SelectedSecurityQuestion = selectedSecurityQuestion;
            Answer = answer;
            Password = password;
        }
    }
}
