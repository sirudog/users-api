using FluentValidation;

namespace UsersApi.Models.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Id).Length(24).When(user => user.Id is not null);
            RuleFor(user => user.Name).NotEmpty();
            RuleFor(user => user.Username).NotEmpty();
            RuleFor(user => user.Email).EmailAddress();
            RuleFor(user => user.Phone).NotEmpty();
            RuleFor(user => user.Website).NotEmpty();
            RuleFor(user => user.Company).NotNull().SetValidator(new CompanyValidator());
            RuleFor(user => user.Address).NotNull().SetValidator(new AddressValidator());
        }
    }
}
