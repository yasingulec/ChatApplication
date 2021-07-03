using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Entities
{
    public class User
    {
        public User()
        {
            Roles = new List<Role>();
        }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Biography { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastActivityDate { get; set; }
        public List<Role> Roles { get; set; }

    }
}
