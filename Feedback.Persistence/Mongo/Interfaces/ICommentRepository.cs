
using Feedback.Domain.Entities;

namespace Feedback.Persistence.Mongo.Interfaces
{
    public interface ICommentRepository
    {
        Task AddAsync(CommentModel comment);
    }
}