using ChatApplication.Data.Commands.UserCommands;
using ChatApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Manager.Commands.UserCommands
{
    public class UserManagerCommands : IUserManagerCommands
    {
        private IUserCommands _userCommands;
        public UserManagerCommands(IUserCommands userCommands)
        {
            _userCommands = userCommands;
        }
        public async Task AddUserAsync(User user)
        {
            await _userCommands.AddUserAsync(user);
        }
    }
}
