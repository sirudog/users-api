using FluentValidation;

namespace UsersApi.Models.Validation
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.Street).NotEmpty();
            RuleFor(address => address.City).NotEmpty();
            RuleFor(address => address.Suite).NotEmpty();
            RuleFor(address => address.Zipcode).NotEmpty();
            RuleFor(address => address.Geo).NotNull().SetValidator(new GeoValidator());
        }
    }
}
