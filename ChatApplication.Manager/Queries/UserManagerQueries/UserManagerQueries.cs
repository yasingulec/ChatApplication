using ChatApplication.Data.Queries.UserQueries;
using ChatApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Manager.Queries.UserManagerQueries
{
    public class UserManagerQueries : IUserManagerQueries
    {
        private readonly IUserQueries _userQueries;
        public UserManagerQueries(IUserQueries userQueries)
        {
            _userQueries = userQueries;
        }
        public async Task<User> GetUserAsync(Guid guid)
        {
            var user = await _userQueries.GetUserAsync(guid);
            return user;
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            var user = await _userQueries.GetUserAsync(username, password);
            return user;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _userQueries.GetUsersAsync();
            return users;
        }
    }
}
