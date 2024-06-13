namespace MovieTicketAPI.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;

    public class Seat
    {
        [BsonElement("Number")]
        public string Number { get; set; } = string.Empty;

        [BsonElement("IsBooked")]
        public bool IsBooked { get; set; } = false;

        [BsonElement("Price")]
        public decimal Price { get; set; } = 130000; // Giá mặc định là 130.000đ
    }

    public class Showtime
    {
        [BsonElement("Time")]
        public DateTime Time { get; set; }

        [BsonElement("Seats")]
        public List<Seat> Seats { get; set; } = new List<Seat>();
    }

    public class Movie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("Genre")]
        public string Genre { get; set; } = string.Empty;

        [BsonElement("ReleaseDate")]
        public DateTime ReleaseDate { get; set; }

        [BsonElement("Image")]
        public string Image { get; set; } = string.Empty;

        [BsonElement("Showtimes")]
        public List<Showtime> Showtimes { get; set; } = new List<Showtime>();
    }
}
