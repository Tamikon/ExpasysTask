using System;
using System.ComponentModel.DataAnnotations;

namespace ASP_Homework_Product.Models
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid? OrderId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public decimal Cost => Product?.Cost * Amount ?? 0;
    }
}
