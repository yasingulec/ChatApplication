using AutoMapper;
using ChatApplication.API.Models;
using ChatApplication.API.Models.Common;
using ChatApplication.Manager.Queries.UserManagerQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApplication.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserManagerQueries _userManagerQueries;
        private readonly IMapper _mapper;
        public AuthController(IUserManagerQueries userManagerQueries, IMapper mapper)
        {
            _userManagerQueries = userManagerQueries;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetToken(UserAuthenticationModel model)
        {
            var user = await _userManagerQueries.GetUserAsync(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { 
                    message=$"{nameof(model.Username)} veya {nameof(model.Password)} hatalı."
                });

            _mapper.Map<List<RoleResponseModel>>(user.Roles);
            var mappedUser = _mapper.Map<UserAuthenticationResponseModel>(user);

            mappedUser.Token = _userManagerQueries.GetToken(user);
            Response<UserAuthenticationResponseModel> response = new Response<UserAuthenticationResponseModel>
            {
                Data = mappedUser,
                isSuccess = true,
                Message = "Token başarılı şekilde alındı."
            };

            return Ok(response);
        }
    }
}
