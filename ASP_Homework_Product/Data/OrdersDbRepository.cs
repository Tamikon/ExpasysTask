using ASP_Homework_Product.Data;
using ASP_Homework_Product.Models;
using ASP_Homework_Product;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;

public class OrdersDbRepository : IOrdersRepository
{
    private readonly AppDbContext _context;

    public OrdersDbRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public List<Order> GetAll()
    {
        return _context.Orders
            .Include(o => o.User)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .ToList();
    }

    public Order TryGetById(Guid orderId)
    {
        var order = _context.Orders
            .Include(o => o.User)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .AsNoTracking()
            .FirstOrDefault(o => o.Id == orderId);
        Console.WriteLine($"TryGetById: OrderId={orderId}, Status={order?.Status}");
        return order;
    }

    public void UpdateStatus(Guid orderId, OrderStatus newStatus)
    {
        var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
        if (order != null)
        {
            Console.WriteLine($"Before update: OrderId={orderId}, Status={order.Status}");
            order.Status = newStatus;
            Console.WriteLine($"After update: OrderId={orderId}, Status={order.Status}");
            _context.Entry(order).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
                Console.WriteLine($"Saved: OrderId={orderId}, Status={order.Status}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving: {ex.Message}");
                throw;
            }
        }
        else
        {
            Console.WriteLine($"Order not found: OrderId={orderId}");
        }
    }
}