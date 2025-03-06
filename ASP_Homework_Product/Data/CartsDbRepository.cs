using ASP_Homework_Product.Data;
using ASP_Homework_Product.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ASP_Homework_Product
{
    public class CartsDbRepository : ICartsRepository
    {
        private readonly AppDbContext _context;

        public CartsDbRepository(AppDbContext context)
        {
            _context = context;
        }

        public Cart TryGetByUserId(string userId)
        {
            return _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.UserId == userId);
        }

        public void Add(Product product, string userId)
        {
            var cart = TryGetByUserId(userId);
            if (cart == null)
            {
                cart = new Cart { Id = Guid.NewGuid(), UserId = userId, Items = new List<CartItem>() };
                var newItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.Id,
                    ProductId = product.Id,
                    Amount = 1
                };
                cart.Items.Add(newItem);
                _context.Carts.Add(cart);
            }
            else
            {
                var item = cart.Items.FirstOrDefault(i => i.ProductId == product.Id);
                if (item != null)
                {
                    item.Amount++;
                    _context.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    var newItem = new CartItem
                    {
                        Id = Guid.NewGuid(),
                        CartId = cart.Id,
                        ProductId = product.Id,
                        Amount = 1
                    };
                    _context.CartItems.Add(newItem);
                }
            }
            _context.SaveChanges();
        }

        public void DecreaseAmount(int productId, string userId)
        {
            var cart = TryGetByUserId(userId);
            if (cart != null)
            {
                var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
                if (item != null)
                {
                    item.Amount--;
                    if (item.Amount <= 0)
                    {
                        _context.CartItems.Remove(item);
                    }
                    else
                    {
                        _context.Entry(item).State = EntityState.Modified;
                    }
                    _context.SaveChanges();
                }
            }
        }

        public void Clear(string userId)
        {
            var cart = TryGetByUserId(userId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                _context.SaveChanges();
            }
        }
    }
}