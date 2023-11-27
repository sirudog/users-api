using FluentValidation;

namespace UsersApi.Models.Validation
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(company => company.Name).NotEmpty();
            RuleFor(company => company.CatchPhrase).NotEmpty();
            RuleFor(company => company.Bs).NotEmpty();
        }
    }
}
