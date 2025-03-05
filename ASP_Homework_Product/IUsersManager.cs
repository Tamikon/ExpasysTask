using ASP_Homework_Product.Models;
using System.Collections.Generic;

namespace ASP_Homework_Product
{
    public interface IUsersManager
    {
        void Add(UserAccount user);
        void ChangePassword(string userName, string newPassword);
        List<UserAccount> GetAll();
        void SetBlockedStatus(string name, bool isBlocked);
        UserAccount TryGetByName(string name);
    }
}