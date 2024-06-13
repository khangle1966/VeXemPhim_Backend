namespace MovieTicketAPI.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("MovieId")]
        public string MovieId { get; set; } = string.Empty;

        [BsonElement("Showtime")]
        public DateTime Showtime { get; set; }

        [BsonElement("SeatNumber")]
        public string SeatNumber { get; set; } = string.Empty;

        [BsonElement("Price")]
        public decimal Price { get; set; } = 130000; // Giá mặc định là 130.000đ
    }
}
