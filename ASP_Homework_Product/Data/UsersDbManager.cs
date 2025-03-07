using ASP_Homework_Product.Data;
using ASP_Homework_Product.Models;
using ASP_Homework_Product;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class UsersDbManager : IUsersManager
{
    private readonly AppDbContext _context;

    public UsersDbManager(AppDbContext context)
    {
        _context = context;
    }

    public void Add(UserAccount user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void ChangePassword(string userName, string newPassword)
    {
        var user = TryGetByName(userName);
        if (user != null)
        {
            user.Password = newPassword;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }

    public List<UserAccount> GetAll()
    {
        return _context.Users.ToList();
    }

    public UserAccount TryGetByName(string name)
    {
        return _context.Users.FirstOrDefault(u => u.Name == name);
    }

    public void SetRole(string userName, UserRole role)
    {
        var user = TryGetByName(userName);
        if (user != null)
        {
            user.Role = role;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}