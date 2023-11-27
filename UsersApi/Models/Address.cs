namespace UsersApi.Models
{
    public class Address
    {
        public Address(string street, string suite, string city, string zipcode, Geo geo) =>
            (Street, Suite, City, Zipcode, Geo) = (street, suite, city, zipcode, geo);

        public string Street { get; }

        public string Suite { get; }

        public string City { get; }

        public string Zipcode { get; }

        public Geo Geo { get; }
    }
}
