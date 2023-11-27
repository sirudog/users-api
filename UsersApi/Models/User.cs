using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UsersApi.Models
{
    public class User
    {
        public User(string id, string name, string username, string email, string phone, string website, Address address, Company company) =>
            (Id, Name, Username, Email, Phone, Website, Address, Company) = (id, name, username, email, phone, website, address, company);  

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } // auto-incremented integer IDs are not supported by MongoDB

        public string Name { get; private set; }

        public string Username { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public string Website { get; private set; }

        public Address Address { get; private set; }

        public Company Company { get; private set; }
    }
}
