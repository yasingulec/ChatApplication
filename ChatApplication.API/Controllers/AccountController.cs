using AutoMapper;
using ChatApplication.API.Models;
using ChatApplication.API.Models.Common;
using ChatApplication.Entities;
using ChatApplication.Entities.DTO;
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
            var user = await _userManagerQueries.GetUserByUsername(userModel.Username);
            if (user != null)
                return BadRequest(new Response
                {
                    isSuccess = false,
                    Message = $"Bu {nameof(userModel.Username)}'e sahip kullanıcı sistemde kayıtlı."
                });

            var mappedUser = _mapper.Map<User>(userModel);
            await _userManagerCommands.AddUserAsync(mappedUser);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel userModel)
        {
            var user = _userManagerQueries.GetUserAsync(userModel.userName, userModel.oldPassword);
            if (user == null)
                return BadRequest(new Response
                {
                    isSuccess = false,
                    Message = $"Bu {nameof(userModel.userName)}'e sahip kullanıcı sistemde bulunamadı."
                });
            var mappedUser = _mapper.Map<ChangePasswordDTO>(userModel);
            bool _isSuccess = await _userManagerCommands.ChangePassword(mappedUser);
            if (_isSuccess == true)
                return Ok(new Response { 
                isSuccess=_isSuccess,
                Message=$"Şifreniz başarı ile güncellenmiştir."
                });
            return BadRequest(new Response
            {
                isSuccess = _isSuccess,
                Message = $"Şifreniz güncellenirken bir hata ile karşılaşıldı."
            });

        }
    }
}
