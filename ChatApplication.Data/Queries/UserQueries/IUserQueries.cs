using ChatApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Data.Queries.UserQueries
{
    public interface IUserQueries
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(Guid guid);
        Task<User> GetUserAsync(string username, string password);
    }
}
