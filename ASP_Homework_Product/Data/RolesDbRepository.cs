using ASP_Homework_Product.Data;
using ASP_Homework_Product.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ASP_Homework_Product
{
    public class RolesDbRepository : IRolesRepository
    {
        private readonly AppDbContext _context;

        public RolesDbRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public List<Role> GetAll()
        {
            return _context.Roles.ToList();
        }

        public void Remove(string roleName)
        {
            var role = _context.Roles.Find(roleName);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }

        public Role TryGetByName(string name)
        {
            return _context.Roles.FirstOrDefault(r => r.Name == name);
        }
    }
}