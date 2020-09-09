using System;
using Newtonsoft.Json;

namespace NewsApi.Application.Dtos
{
    public abstract class CommentRequest
    {
        public string Text { get; set; }

        [JsonIgnore]
        public AuthorRequest Author { get; set; }
    }

    public class CreateCommentRequest : CommentRequest { }
    public class UpdateCommentRequest : CommentRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }

}