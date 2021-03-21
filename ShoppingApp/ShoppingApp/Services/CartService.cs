using ShoppingApp.Data;
using ShoppingApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Monads;

namespace ShoppingApp.Services
{

    public interface ICartService
    {
        Result<Cart> GetCart(long id);
        IList<Cart> GetAllCarts(long userId);
        Result AddCart(Cart cart);
        Result RemoveCart(Cart cart);
        Result UpdateCart(Cart cart);
    }

    class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService()
        {
            this._cartRepository = new CartRepository();
            this._productRepository = new ProductRepository();
        }

        public Result AddCart(Cart cart)
        {
            if (cart == null)
                return Result.Error("Fail to add cart");

            Product product = _productRepository.Get(cart.ProductId);
            if (product == null)
                return Result.Error("Product not exist");

            Cart existingCart = _cartRepository.GetByProductId(cart.ProductId);
            if (existingCart == null)
                _cartRepository.Add(cart);
            else
            {
                existingCart.ItemCount += cart.ItemCount;
                _cartRepository.Update(existingCart);
            }

            if (product != null)
            {
                product.InventoryCount -= cart.ItemCount;
                _productRepository.Update(product);
            }

            Result result = Result.Success();
            return result;
        }

        public IList<Cart> GetAllCarts(long userId)
        {
            return _cartRepository.GetAll(userId);
        }

        public Result<Cart> GetCart(long id)
        {
            throw new NotImplementedException();
        }

        public Result RemoveCart(Cart cart)
        {
            if (_cartRepository.Remove(cart))
                return Result.Success();

            return Result.Error("Fail to remove cart");

        }

        public Result UpdateCart(Cart cart)
        {
            if (_cartRepository.Update(cart))
                return Result.Success();

            return Result.Error("Fail to update cart");
        }
    }
}
