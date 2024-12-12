using Feedback.Application.Logic.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Api.Controllers
{
    [ApiController]
    [Route("feedback-service/v1/comment")]
    public class CommentController : ControllerBase
    {
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateComment([FromServices] IMediator mediator, [FromBody] CreateCommentCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }
    }
}