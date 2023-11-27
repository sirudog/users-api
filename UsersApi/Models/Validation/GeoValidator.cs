using FluentValidation;

namespace UsersApi.Models.Validation
{
    public class GeoValidator : AbstractValidator<Geo>
    {
        public GeoValidator()
        {
            RuleFor(geo => geo.Lng).NotEmpty();
            RuleFor(geo => geo.Lat).NotEmpty();
        }
    }
}
