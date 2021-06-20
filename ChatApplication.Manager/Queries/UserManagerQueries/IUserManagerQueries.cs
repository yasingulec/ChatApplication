using ChatApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Manager.Queries.UserManagerQueries
{
   public interface IUserManagerQueries
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(Guid guid);
        Task<User> GetUserAsync(string username, string password);
    }
}
