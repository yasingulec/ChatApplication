using AutoMapper;
using ChatApplication.API.Models;
using ChatApplication.API.Models.Common;
using ChatApplication.Entities;
using ChatApplication.Manager.Commands.UserCommands;
using ChatApplication.Manager.Queries.UserManagerQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserManagerCommands _userManagerCommands;
        private IUserManagerQueries _userManagerQueries;
        private readonly IMapper _mapper;
        public AccountController(IUserManagerCommands userManagerCommands, IUserManagerQueries userManagerQueries, IMapper mapper)
        {
            _userManagerCommands = userManagerCommands;
            _userManagerQueries = userManagerQueries;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel userModel)
        {
            var user = await _userManagerQueries.GetUserAsync(userModel.Username, userModel.PasswordHash);
            if (user != null)
                return BadRequest(new Response<string>
                {
                    Data = null,
                    isSuccess = false,
                    Message = $"Bu {nameof(userModel.Username)}'e sahip kullanıcı sistemde kayıtlı."
                });

            var mappedUser = _mapper.Map<User>(userModel);
            await _userManagerCommands.AddUserAsync(mappedUser);

            return Ok();
        }
    }
}
