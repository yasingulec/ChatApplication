using ChatApplication.Entities;
using ChatApplication.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Manager.Commands.UserCommands
{
    public interface IUserManagerCommands
    {
        Task AddUserAsync(User user);
        Task<bool> ChangePassword(ChangePasswordDTO passwordDTO);

    }
}
