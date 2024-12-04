using Feedback.Domain.Entities;
using MediatR;

namespace Feedback.Application.Logic.Commands
{
    public class CreateCommentCommand : IRequest<CommentModel>
    {
        public string LivestreamId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }

        public CreateCommentCommand(string livestreamId, string userName, string content)
        {
            LivestreamId = livestreamId;
            UserName = userName;
            Content = content;
        }
    }
}