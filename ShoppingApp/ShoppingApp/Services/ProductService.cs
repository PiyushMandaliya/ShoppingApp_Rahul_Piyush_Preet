using ShoppingApp.Data;
using ShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Monads;

namespace ShoppingApp.Services
{

    public interface IProductService
    {
        Result<Product> GetProduct(long id);
        IList<Product> GetAllProducts();

        Result AddProduct(Product product);
        Result RemoveProduct(Product product);
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
            throw new NotImplementedException();
        }
    }
}
