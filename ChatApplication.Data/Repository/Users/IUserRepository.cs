using ChatApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Data.Repository.Users
{
   public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUser(Guid id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(Guid id);
    }
}
