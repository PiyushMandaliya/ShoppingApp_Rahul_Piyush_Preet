using ShoppingApp.Data;
using ShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Monads;

namespace ShoppingApp.Services
{

    //Author: Piyushkumar Mandaliya
    public interface IProductService
    {
        Result<Product> GetProduct(long id);
        IList<Product> GetAllProducts();
        Result AddProduct(Product product);
        Result RemoveProduct(Product product);
        Result UpdateProduct(Product product);
    }

    class ProductService : IProductService
    {
        private readonly IProductRepository repository;

        public ProductService(IProductRepository repository)
        {
            this.repository = repository;
        }

        public Result AddProduct(Product product)
        {
            if (product == null)
                return Result.Error("Fail to add product");

            repository.Add(product);
            Result result = Result.Success();
            return result;
        }

        public IList<Product> GetAllProducts()
        {
            return repository.GetAll();
        }

        public Result<Product> GetProduct(long id)
        {
            throw new NotImplementedException();
        }

        public Result RemoveProduct(Product product)
        {
            if (repository.Remove(product))
                return Result.Success();

            return Result.Error("Fail to remove product");

        }

        public Result UpdateProduct(Product product)
        {
            if (repository.Update(product))
                return Result.Success();

            return Result.Error("Fail to update product");
        }
    }
}
