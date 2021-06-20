using AutoMapper;
using ChatApplication.API.Models;
using ChatApplication.API.Models.Common;
using ChatApplication.Manager.Queries.UserManagerQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserManagerQueries _userManagerQueries;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthController(IUserManagerQueries userManagerQueries, IConfiguration configuration, IMapper mapper)
        {
            _userManagerQueries = userManagerQueries;
            _configuration = configuration;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetToken(UserAuthenticationModel model)
        {

            var user = await _userManagerQueries.GetUserAsync(model.Username, model.Password);
            var mappedUser = _mapper.Map<UserAuthenticationResponseModel>(user);
            if (user == null)
                return BadRequest();

            var claims = new List<Claim>
            {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Username", user.Username),
                    new Claim("Email", user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(7), signingCredentials: signIn);
            string loginToken = new JwtSecurityTokenHandler().WriteToken(token);
            mappedUser.Token = loginToken;
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
