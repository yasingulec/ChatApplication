using ChatApplication.Data.Queries.UserQueries;
using ChatApplication.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Manager.Queries.UserManagerQueries
{
    public class UserManagerQueries : IUserManagerQueries
    {
        private readonly IUserQueries _userQueries;
        private readonly IConfiguration _configuration;
        public UserManagerQueries(IUserQueries userQueries, IConfiguration configuration)
        {
            _userQueries = userQueries;
            _configuration = configuration;
        }

        public  string GetToken(User user)
        {
            var claims = new List<Claim>
            {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("Username", user.Username),
                    new Claim("Email", user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(7), signingCredentials: signIn);
            string loginToken = new JwtSecurityTokenHandler().WriteToken(token);
            return loginToken;
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
