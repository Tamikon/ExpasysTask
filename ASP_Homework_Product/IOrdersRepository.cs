using ASP_Homework_Product.Models;
using System;
using System.Collections.Generic;

namespace ASP_Homework_Product
{
    public interface IOrdersRepository
    {
        void Add(Order order);
        List<Order> GetAll(string userId); // Для обычных пользователей
        Order TryGetById(Guid orderId, string userId); // Для обычных пользователей
        List<Order> GetAllAdmin(); // Для админа (все заказы)
        Order TryGetByIdAdmin(Guid orderId); // Для админа (без фильтра по userId)
        void UpdateStatus(Guid orderId, OrderStatus newStatus);
    }
}