using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ASP_Homework_Product.Models
{
    public class Product
    {
        private static int idCounter = 0;

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        [Range(0, 1000000, ErrorMessage = "Цена должна быть в пределах от 0 до 1 000 000 руб.")]
        public decimal Cost { get; set; }

        public string ImagePath { get; set; }
        public Product()
        {
            Id = idCounter;
            idCounter++;
        }
        public Product(string name, string description, int cost, string imagePath): this()
        {
            Name = name;
            Description = description;
            Cost = cost;
            ImagePath = imagePath;
        }
        public override string ToString()
        {
            return $"{Id}\n{Name}\n{Cost}";
        }
    }
}