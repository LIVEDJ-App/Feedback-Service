using Feedback.Application.Events;
using Feedback.Domain.Entities;
using Feedback.Persistence.Mongo.Interfaces;
using MediatR;

namespace Feedback.Application.Logic.Commands
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentModel>
    {
        private readonly ICommentRepository _repository;
        private readonly IPublisher _publisher;

        public CreateCommentCommandHandler(ICommentRepository repository, IPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public async Task<CommentModel> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
        {
            var comment = new CommentModel
            {
                Id = Guid.NewGuid(),
                LivestreamId = command.LivestreamId,
                UserName = command.UserName,
                Content = command.Content
            };
            await _repository.AddAsync(comment);

            var commentCreatedEvent = new CommentCreatedEvent
            {
                CommentId = comment.Id,
                LivestreamId = comment.LivestreamId,
                UserName = comment.UserName,
                Content = comment.Content
            };
            await _publisher.Publish(commentCreatedEvent, cancellationToken);

            return comment;
        }

    }
}