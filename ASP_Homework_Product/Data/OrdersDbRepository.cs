using ASP_Homework_Product.Data;
using ASP_Homework_Product.Models;
using ASP_Homework_Product;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
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
        Console.WriteLine($"Order added: Id={order.Id}, ItemsCount={order.Items.Count}");
    }

    public List<Order> GetAll(string userId)
    {
        return _context.Orders
            .Include(o => o.User)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product) // Гарантируем загрузку Product
            .Where(o => o.UserId == userId)
            .ToList();
    }

    public Order TryGetById(Guid orderId, string userId)
    {
        return _context.Orders
            .Include(o => o.User)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .AsNoTracking()
            .FirstOrDefault(o => o.Id == orderId && o.UserId == userId);
    }

    public List<Order> GetAllAdmin()
    {
        return _context.Orders
            .Include(o => o.User)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .ToList();
    }

    public Order TryGetByIdAdmin(Guid orderId)
    {
        return _context.Orders
            .Include(o => o.User)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .AsNoTracking()
            .FirstOrDefault(o => o.Id == orderId);
    }

    public void UpdateStatus(Guid orderId, OrderStatus newStatus)
    {
        var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
        if (order != null)
        {
            order.Status = newStatus;
            _context.SaveChanges();
        }
    }
}