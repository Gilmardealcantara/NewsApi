using System;
using NewsApi.Application.Entities;
using Newtonsoft.Json;

namespace NewsApi.Application.Dtos
{
    public abstract class CommentRequest
    {
        public string Text { get; set; }

        [JsonIgnore]
        public Guid NewsId { get; set; }

        [JsonIgnore]
        public AuthorRequest Author { get; set; }
    }

    public class CreateCommentRequest : CommentRequest
    {
        public Comment ToComment(Author author) => new Comment(this.Text, author);
    }

    public class UpdateCommentRequest : CommentRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public Comment ToComment(Author author) => new Comment(this.Id, this.Text, author);
    }

}