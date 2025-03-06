using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ASP_Homework_Product.Models
{

    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal Cost => Items?.Sum(x => x.Cost) ?? 0;
        public decimal Amount => Items?.Sum(x => x.Amount) ?? 0;
    }
}
