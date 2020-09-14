using System;
using NewsApi.Application.Dtos;

namespace NewsApi.Application.Tests.Builders
{
    public class CommentRequestBuilder
    {
        private readonly CommentRequest _instance;

        public CommentRequestBuilder()
        {
            _instance = new CreateCommentRequest
            {
                Text = "Legal",
                Author = new AuthorRequestBuilder().Build()
            };
        }

        public CommentRequestBuilder(Guid id)
        {
            _instance = new UpdateCommentRequest
            {
                Id = id,
                NewsId = Guid.NewGuid(),
                Text = "Legal",
                Author = new AuthorRequestBuilder().Build()
            };
        }

        public CommentRequestBuilder WithNewsId(Guid newsId)
        {
            _instance.NewsId = newsId;
            return this;
        }

        public CommentRequestBuilder WithText(string text)
        {
            _instance.Text = text;
            return this;
        }

        public CommentRequestBuilder WithAuthor(AuthorRequest author)
        {
            _instance.Author = author;
            return this;
        }

        public CreateCommentRequest BuildCreate() => (CreateCommentRequest)_instance;
        public UpdateCommentRequest BuildUpdate() => (UpdateCommentRequest)_instance;
        public CommentRequest Build() => _instance;
    }

}