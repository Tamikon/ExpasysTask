using System;
using System.Collections.Generic;

namespace ASP_Homework_Product.Models
{
    public class Order
    {
        public Guid Id {  get; set; }
        public UserDeliveryInfo User { get; set; }
        public List<CartItem> Items { get; set; }

        public Order()
        {
            Guid Id = Guid.NewGuid();
        }
    }
}
