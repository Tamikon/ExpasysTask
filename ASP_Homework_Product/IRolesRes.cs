using ASP_Homework_Product.Areas.Admin.Models;
using ASP_Homework_Product.Models;
using System.Collections.Generic;

namespace ASP_Homework_Product
{
    public interface IRolesRes
    {
        List<Role> GetAll();
        Role TryGetByName(string userId);
        void Add(Role role);
        void Remove(string name);
    }
}