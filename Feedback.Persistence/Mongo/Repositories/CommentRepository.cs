
using Feedback.Domain.Entities;
using Feedback.Persistence.Mongo.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Feedback.Persistence.Mongo.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IMongoCollection<CommentModel> _comments;

        public CommentRepository(IOptions<MongoDbSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _comments = mongoDatabase.GetCollection<CommentModel>(databaseSettings.Value.CollectionName);
        }

        public async Task AddAsync(CommentModel comment)
        {
            await _comments.InsertOneAsync(comment);
        }
    }
}