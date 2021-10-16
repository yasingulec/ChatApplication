using ChatApplication.Data.Commands.UserCommands;
using ChatApplication.Entities;
using ChatApplication.Entities.DTO;
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

        public async Task<bool> ChangePassword(ChangePasswordDTO passwordDTO)
        {
            int changedRows = 0;
            try
            {
                if (passwordDTO.newPassword == passwordDTO.confirmPassword)
                    changedRows = await _userCommands.ChangePasswordAsync(passwordDTO);
                if (changedRows > 0)
                    return true;
                return false;
            }
            catch (Exception )
            {

                throw;
            }


        }
    }
}
