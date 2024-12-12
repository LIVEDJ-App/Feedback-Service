using Feedback.Infrastructure.RabbitMQ;
using MediatR;

namespace Feedback.Application.Events
{
    public class CommentCreatedEventHandler : INotificationHandler<CommentCreatedEvent>
    {
        private readonly IEventPublisher _eventPublisher;

        public CommentCreatedEventHandler(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public async Task Handle(CommentCreatedEvent notification, CancellationToken cancellationToken)
        {
            _eventPublisher.Publish(notification, "CommentCreatedQueue", "CommentChannel");
            Console.WriteLine($"Published CommentCreatedEvent for {notification.CommentId}");
            await Task.CompletedTask;
        }
    }
}