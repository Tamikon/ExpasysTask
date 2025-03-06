using System.ComponentModel.DataAnnotations;

namespace ASP_Homework_Product.Models
{
    public class UserAccount
    {
        [Key]
        public string Name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool IsBlocked { get; set; }
    }
}
