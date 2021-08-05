using ChatApplication.API.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.API.FluentValidation
{
    public class UserLoginValidator:AbstractValidator<UserAuthenticationModel>
    {
        public UserLoginValidator()
        {
            RuleFor(x=>x.Username).Length(3, 20).Matches("^[a-zA-Z0-9_][a-zA-Z0-9_.]*").NotEmpty();
            RuleFor(x => x.Password).Length(8, 20).Matches("^[a-zA-Z0-9_][a-zA-Z0-9_.]*").NotEmpty();
        }
    }
}
