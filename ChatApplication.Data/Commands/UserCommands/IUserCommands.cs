using ChatApplication.Entities;
using ChatApplication.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Data.Commands.UserCommands
{
    public interface IUserCommands
    {
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid guid);
        Task<int> ChangePasswordAsync(ChangePasswordDTO user);
    }
}
