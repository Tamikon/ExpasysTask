using System.ComponentModel.DataAnnotations;

namespace ASP_Homework_Product.Models
{
    public class UserDeliveryInfo
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
    }  
}
