using ASP_Homework_Product.Models;
using System.Collections.Generic;

namespace ASP_Homework_Product
{
    public interface IUsersManager
    {
        void Add(UserAccount user);
        void ChangePassword(string userName, string newPassword);
        List<UserAccount> GetAll();
        UserAccount TryGetByName(string name);
    }
}