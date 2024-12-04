using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feedback.Api.Controllers;
using Feedback.Application.Logic.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Feedback.Api.Test.ControllerTests
{
    public class CommentControllerTests
    {
                [Fact]
        public async Task CreateComment_ReturnsOkResult_WhenCommandIsHandledSuccessfully()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var command = new CreateCommentCommand("liveId", "name", "comment"); 
            var controller = new CommentController();

            // Act
            var result = await controller.CreateComment(mediator, command);
            
            // Assert
            await mediator.Received(1).Send(command);
            var okResult = Assert.IsType<OkResult>(result);
        }
    }
}