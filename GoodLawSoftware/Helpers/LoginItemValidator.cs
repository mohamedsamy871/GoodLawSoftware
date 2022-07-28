using Core.Entities;
using FluentValidation;

namespace GoodLawSoftware.Helpers
{
    public class LoginItemValidator:AbstractValidator<LoginItem>
    {
        public LoginItemValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().Length(8,50).EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty().Length(4, 25);
        }
    }
}
