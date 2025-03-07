using ASP_Homework_Product.Models;
using System.Collections.Generic;

public interface IUsersManager
{
    void Add(UserAccount user);
    void ChangePassword(string userName, string newPassword);
    List<UserAccount> GetAll();
    UserAccount TryGetByName(string name);
    void SetRole(string userName, UserRole role);
}