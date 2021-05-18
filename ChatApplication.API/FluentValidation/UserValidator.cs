using ChatApplication.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.API.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username).Length(3, 20).Matches("^[a-zA-Z0-9_][a-zA-Z0-9_.]*").NotEmpty();
            RuleFor(x => x.Email).Length(50).EmailAddress().NotEmpty();
            RuleFor(x => x.PasswordHash).Length(8,20).Matches("^[a-zA-Z0-9_][a-zA-Z0-9_.]*").NotEmpty();
            RuleFor(x => x.Biography).Length(1000);
        }
    }
}
