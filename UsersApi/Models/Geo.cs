namespace UsersApi.Models
{
    public class Geo
    {
        public Geo(string lat, string lng) =>
            (Lat, Lng) = (lat, lng);

        public string Lat { get; }

        public string Lng { get; }
    }
}
