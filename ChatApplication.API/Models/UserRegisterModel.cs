using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.API.Models
{
    public class UserRegisterModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Biography { get; set; }
        public DateTime Birthday { get; set; }
    }
}
