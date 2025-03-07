using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ASP_Homework_Product.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("User")]
        public Guid UserDeliveryInfoId { get; set; }
        public UserDeliveryInfo User { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public OrderStatus Status { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;
        public decimal Cost => Items?.Sum(x => x.Cost) ?? 0;
    }
}