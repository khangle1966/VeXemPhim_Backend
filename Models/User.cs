namespace MovieTicketAPI.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Username")]
        [Required]
        public string Username { get; set; } = string.Empty;

        [BsonElement("Password")]
        [Required]
        public string Password { get; set; } = string.Empty;

        [BsonElement("Role")]
        [Required]
        public string Role { get; set; } = "User"; // Default role is User
    }
}
