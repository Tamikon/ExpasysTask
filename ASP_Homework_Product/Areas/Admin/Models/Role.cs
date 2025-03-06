using System.ComponentModel.DataAnnotations;

namespace ASP_Homework_Product.Areas.Admin.Models
{
    public class Role
    {
        [Key]
        public string Name { get; set; }
    }
}
