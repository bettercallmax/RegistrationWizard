using FluentValidation;

namespace Api.Models.User
{
    public class UserLoginModelValidator : AbstractValidator<UserLoginModel>
    {
        public UserLoginModelValidator()
        {
            RuleFor(x => x.Login).EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
        }
    }
}
