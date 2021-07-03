using ChatApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.API.Models
{
    public class UserAuthenticationResponseModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastActivityDate { get; set; }
        public List<Role> Roles { get; set; }
        public string Token { get; set; }
    }
}
