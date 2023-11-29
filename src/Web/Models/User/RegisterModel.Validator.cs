using FluentValidation;

namespace Api.Models.User
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.ProvinceId).NotNull().GreaterThan(0);
            RuleFor(x => x.Login).EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
        }
    }
}
