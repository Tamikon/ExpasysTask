using ASP_Homework_Product.Models;
using System;
using System.Collections.Generic;

namespace ASP_Homework_Product
{
    public interface IOrdersRes
    {
        void Add(Order order);
        List<Order> GetAll();
        Order TryGetById(Guid orderId);
        void UpdateStatus(Guid orderId, OrderStatus newStatus);
    }
}