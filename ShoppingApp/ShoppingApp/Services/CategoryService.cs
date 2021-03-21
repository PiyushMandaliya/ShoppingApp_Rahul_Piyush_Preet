using ShoppingApp.Data;
using ShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Monads;

namespace ShoppingApp.Services
{
    //Author: Piyushkumar Mandaliya
    public interface ICategoryService
    {
        Result<Category> GetCategory(long id);
        List<Category> GetAllCategory();
        public Result AddCategory(Category category);
        public Result RemoveCategory(Category category);
    }

    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository repository;

        public CategoryService(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        public Result<Category> GetCategory(long id)
        {
            Category category= repository.Get(id);
            if (category == null)
                return Result<Category>.Error("Fail to get category: " + id);
            else
                return Result<Category>.Success(category);
        }


        public List<Category> GetAllCategory()
        {
            return repository.GetAll();
        }

        public Result AddCategory(Category category)
        {
            if (category == null)
                return Result.Error("Fail to add category");
            if (!repository.Exists(category.CategoryName))
            {
                repository.Add(category);
                return Result.Success();
            }
            return Result.Error("Category you entered is already exist.");


        }

        public Result RemoveCategory(Category category)
        {
            if (category == null)
                return Result.Error("Fail to remove category");

            if (!repository.Remove(category))
            {
                return Result<Category>.Error("Fail to remove : " + category.CategoryName);
            }
            else
                return Result.Success();
        }
    }
}
