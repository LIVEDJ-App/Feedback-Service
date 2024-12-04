using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Feedback.Domain.Entities
{
    public class CommentModel
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public string LivestreamId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}