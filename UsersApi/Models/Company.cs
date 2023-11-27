namespace UsersApi.Models
{
    public class Company
    {
        public Company(string name, string catchPhrase, string bs) => 
            (Name, CatchPhrase, Bs) = (name, catchPhrase, bs);

        public string Name { get; }

        public string CatchPhrase { get; }

        public string Bs { get; }
    }
}
