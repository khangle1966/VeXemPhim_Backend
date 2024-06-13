namespace MovieTicketAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using MovieTicketAPI.Models;
    using MovieTicketAPI.Services;
    using MongoDB.Driver;
    using MovieTicketAPI.Data;
    using MongoDB.Bson;

    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly MovieService _movieService;
        private readonly IMongoDbSettings _settings;

        public MoviesController(MovieService movieService, IMongoDbSettings settings)
        {
            _movieService = movieService;
            _settings = settings;
        }

        [HttpGet]
        public ActionResult<List<Movie>> Get() => _movieService.Get();

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            try
            {
                var client = new MongoClient(_settings.ConnectionString);
                var database = client.GetDatabase(_settings.DatabaseName);
                return Ok("Connection to MongoDB Atlas successful!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Connection failed: {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public ActionResult<Movie> Get(string id)
        {
            var movie = _movieService.Get(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpPost]
        public ActionResult<Movie> Create(Movie movie)
        {
            _movieService.Create(movie);

            return CreatedAtRoute("GetMovie", new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Movie movieIn)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest("Invalid ID format.");
            }

            var movie = _movieService.Get(id);

            if (movie == null)
            {
                return NotFound();
            }

            _movieService.Update(id, movieIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("ID cannot be null or empty");
            }

            var movie = _movieService.Get(id);

            if (movie == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(movie.Id))
            {
                return BadRequest("Movie ID cannot be null or empty");
            }

            _movieService.Remove(movie.Id);

            return NoContent();
        }
        [HttpGet("search")]
        public ActionResult<List<Movie>> Search([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Query cannot be null or empty.");
            }

            var movies = _movieService.Search(query);
            return Ok(movies);
        }
    }
}
