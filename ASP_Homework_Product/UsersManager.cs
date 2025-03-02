using ASP_Homework_Product.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;

namespace ASP_Homework_Product
{
    public class UsersManager : IUsersManager
    {
        private static readonly List<UserAccount> users = new List<UserAccount>();

        public UsersManager()
        {
            users.Add(new UserAccount { Name = "admin", Password = "admin", Phone = "1234567890" });
        }


        public List<UserAccount> GetAll()
        {
            return users;
        }
        public void Add(UserAccount user)
        {
            users.Add(user);
        }

        public UserAccount TryGetByName(string name)
        {
            return users.FirstOrDefault(x => x.Name == name);
        }

        public void ChangePassword(string userName, string newPassword)
        {
            var account = TryGetByName(userName);
            if (account == null)
            {
                return;
            }
            account.Password = newPassword;
        }
    }
}
