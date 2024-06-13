namespace MovieTicketAPI.Services
{
    using MongoDB.Driver;
    using MovieTicketAPI.Data;
    using MovieTicketAPI.Models;
    using Microsoft.Extensions.Logging;
    using System.Linq;

    public class TicketService
    {
        private readonly IMongoCollection<Ticket> _tickets;
        private readonly IMongoCollection<Movie> _movies;
        private readonly ILogger<TicketService> _logger;

        public TicketService(IMongoClient client, IMongoDbSettings settings, ILogger<TicketService> logger)
        {
            var database = client.GetDatabase(settings.DatabaseName);
            _tickets = database.GetCollection<Ticket>(nameof(Ticket));
            _movies = database.GetCollection<Movie>(nameof(Movie));
            _logger = logger;
        }

        public List<Ticket> Get() => _tickets.Find(ticket => true).ToList();

        public Ticket? Get(string id) => _tickets.Find(ticket => ticket.Id == id).FirstOrDefault();

        public Ticket? Create(Ticket ticket)
        {
            var movie = _movies.Find(m => m.Id == ticket.MovieId).FirstOrDefault();
            if (movie != null)
            {
                var showtime = movie.Showtimes.FirstOrDefault(st => st.Time == ticket.Showtime);
                if (showtime != null)
                {
                    var seat = showtime.Seats.FirstOrDefault(s => s.Number == ticket.SeatNumber);
                    if (seat != null && !seat.IsBooked)
                    {
                        seat.IsBooked = true;
                        _movies.ReplaceOne(m => m.Id == movie.Id, movie);
                        _tickets.InsertOne(ticket);
                        return ticket;
                    }
                }
            }
            _logger.LogWarning("Failed to create ticket. Movie or Showtime not found, or seat already booked.");
            return null; // Explicitly return null if the operation fails
        }

        public void Remove(string id) => _tickets.DeleteOne(ticket => ticket.Id == id);
    }
}
