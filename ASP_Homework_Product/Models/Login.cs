using System.ComponentModel.DataAnnotations;

namespace ASP_Homework_Product.Models
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
