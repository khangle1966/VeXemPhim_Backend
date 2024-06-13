namespace MovieTicketAPI.Services
{
    using MongoDB.Driver;
    using MovieTicketAPI.Models;
    using MovieTicketAPI.Data;
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;

    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly ILogger<UserService> _logger;

        public UserService(IMongoClient client, IMongoDbSettings settings, ILogger<UserService> logger)
        {
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(nameof(User));
            _logger = logger;
        }

        public List<User> Get()
        {
            _logger.LogInformation("Fetching all users from the database.");
            return _users.Find(user => true).ToList();
        }

        public User Get(string id)
        {
            _logger.LogInformation($"Fetching user with id: {id}.");
            return _users.Find(user => user.Id == id).FirstOrDefault();
        }

        public User Create(User user)
        {
            _logger.LogInformation("Inserting a new user into the database.");
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn)
        {
            _logger.LogInformation($"Updating user with id: {id}.");
            _users.ReplaceOne(user => user.Id == id, userIn);
        }

        public void Remove(string id)
        {
            _logger.LogInformation($"Removing user with id: {id}.");
            _users.DeleteOne(user => user.Id == id);
        }

        public User GetByUsername(string username)
        {
            _logger.LogInformation($"Fetching user with username: {username}.");
            return _users.Find(user => user.Username == username).FirstOrDefault();
        }
    }
}