namespace MovieTicketAPI.Services
{
    using MongoDB.Driver;
    using MovieTicketAPI.Data;
    using MovieTicketAPI.Models;
    using Microsoft.Extensions.Logging;
    using MongoDB.Bson;

    public class MovieService
    {
        private readonly IMongoCollection<Movie> _movies;
        private readonly ILogger<MovieService> _logger;

        public MovieService(IMongoClient client, IMongoDbSettings settings, ILogger<MovieService> logger)
        {
            var database = client.GetDatabase(settings.DatabaseName);
            _movies = database.GetCollection<Movie>(nameof(Movie));
            _logger = logger;
        }

        public List<Movie> Get()
        {
            _logger.LogInformation("Fetching all movies from the database.");
            return _movies.Find(movie => true).ToList();
        }

        public Movie Get(string id)
        {
            _logger.LogInformation($"Fetching movie with id: {id}.");
            return _movies.Find(movie => movie.Id == id).FirstOrDefault();
        }

        public Movie Create(Movie movie)
        {
            _logger.LogInformation("Inserting a new movie into the database.");
            _movies.InsertOne(movie);
            return movie;
        }

        public void Update(string id, Movie movieIn)
        {
            _logger.LogInformation($"Updating movie with id: {id}.");
            movieIn.Id = id; // Đảm bảo rằng ID không thay đổi
            _movies.ReplaceOne(movie => movie.Id == id, movieIn);
        }

        public void Remove(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "ID cannot be null or empty");
            }

            _logger.LogInformation($"Removing movie with id: {id}.");
            _movies.DeleteOne(movie => movie.Id == id);
        }
        public List<Movie> Search(string query)
        {
            _logger.LogInformation($"Searching for movies with query: {query}.");
            var filter = Builders<Movie>.Filter.Or(
                Builders<Movie>.Filter.Regex("Title", new MongoDB.Bson.BsonRegularExpression(query, "i")),
                Builders<Movie>.Filter.Regex("Genre", new MongoDB.Bson.BsonRegularExpression(query, "i"))
            );
            return _movies.Find(filter).ToList();
        }
    }
}
