using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feedback.Application.Events;
using Feedback.Application.Logic.Commands;
using Feedback.Domain.Entities;
using Feedback.Infrastructure.RabbitMQ;
using Feedback.Persistence.Mongo.Interfaces;
using MediatR;
using NSubstitute;

namespace Feedback.Application.Test.Comments
{
    public class CreateCommentCommandTests
    {
        [Fact]
        public async Task Handle_ShouldAddCommentAndPublishEvent()
        {
            // Arrange
            var repository = Substitute.For<ICommentRepository>();
            var publisher = Substitute.For<IPublisher>();
            var commandHandler = new CreateCommentCommandHandler(repository, publisher);
            var command = new CreateCommentCommand("streamId", "user", "content");

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.LivestreamId, result.LivestreamId);
            Assert.Equal(command.UserName, result.UserName);
            Assert.Equal(command.Content, result.Content);

            await repository.Received(1).AddAsync(Arg.Is<CommentModel>(l => 
                l.LivestreamId == command.LivestreamId && l.UserName == command.UserName && l.Content == command.Content));
            
            await publisher.Received(1).Publish(Arg.Is<CommentCreatedEvent>(e => 
                e.LivestreamId == command.LivestreamId && e.UserName == command.UserName && e.Content == command.Content), CancellationToken.None);
        }
        
        [Fact]
        public async Task Handle_ShouldPublishEventToQueue()
        {
            // Arrange
            var eventPublisher = Substitute.For<IEventPublisher>();
            var eventHandler = new CommentCreatedEventHandler(eventPublisher);
            var livestreamEvent = new CommentCreatedEvent
            {
                CommentId = Guid.NewGuid(),
                LivestreamId = "12ab34cd",
                UserName = "Test User",
                Content = "Content"
            };

            // Act
            await eventHandler.Handle(livestreamEvent, CancellationToken.None);

            // Assert
            eventPublisher.Received(1).Publish(
                livestreamEvent,
                "CommentCreatedQueue",
                "CommentChannel");
        }
    }
}