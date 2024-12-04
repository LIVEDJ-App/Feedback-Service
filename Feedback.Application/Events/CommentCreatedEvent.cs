using MediatR;

namespace Feedback.Application.Events
{
    public class CommentCreatedEvent : INotification
    {
        public Guid CommentId { get; set; }
        public string LivestreamId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Content { get; set; } = null!;

    }
}