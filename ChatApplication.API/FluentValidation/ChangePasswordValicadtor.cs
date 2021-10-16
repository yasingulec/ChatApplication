using ChatApplication.API.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.API.FluentValidation
{
    public class ChangePasswordValicadtor:AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordValicadtor()
        {
            RuleFor(x => x.userName).Length(3, 20).Matches("^[a-zA-Z0-9_][a-zA-Z0-9_.]*").NotEmpty();
            RuleFor(x => x.oldPassword).Length(8, 20).Matches("^[a-zA-Z0-9_][a-zA-Z0-9_.]*").NotEmpty();
            RuleFor(x => x.newPassword).Length(8, 20).Matches("^[a-zA-Z0-9_][a-zA-Z0-9_.]*").NotEmpty();
            RuleFor(x => x.confirmPassword).Length(8, 20).Matches("^[a-zA-Z0-9_][a-zA-Z0-9_.]*").NotEmpty();


        }
    }
}
