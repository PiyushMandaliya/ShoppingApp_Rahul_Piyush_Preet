using ShoppingApp.Data;
using ShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Authentication;
using Utility.Monads;

namespace ShoppingApp.Services
{
    public interface IUserService
    {
        Result RegisterUser(User user);
        Result<User> LogIn(string userName, string password);

        Result AdminLogin(string userName, string password);
        
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository service;

        public UserService(IUserRepository repository)
        {
            this.service = repository;
        }

        public Result RegisterUser(User user)
        {
            if (service.Exists(user.UserName))
                return Result.Error("UserName already Exists... Try Again!");

            else
            {
                service.Add(user);
                return Result.Success();
            }
        }

        public Result<User> LogIn(string userName, string password)
        {
            User user = service.Get(userName);
            if (user != null)
            {
                bool isSuccess = PasswordUtility.CheckPassword(password, user.Password);

                if (!isSuccess)
                    return Result<User>.Error("Password you entered is incorrect.");
                else
                    return Result<User>.Success(user);
            }
            return Result<User>.Error("Username you entered is incorrect.");
        }

        public Result AdminLogin(string userName, string password)
        {
            User user = service.GetAdmin(userName);
            if (user != null)
            {
                bool isSuccess = PasswordUtility.CheckPassword(password, user.Password);

                if (!isSuccess)
                    return Result<User>.Error("Password you entered is incorrect.");
                else
                    return Result<User>.Success(user);
            }
            return Result<User>.Error(".");
        }

    }
}
