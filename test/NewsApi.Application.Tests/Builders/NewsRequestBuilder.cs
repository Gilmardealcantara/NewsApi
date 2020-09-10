using System;
using NewsApi.Application.Dtos;

namespace NewsApi.Application.Tests.Builders
{
    public class NewsRequestBuilder
    {
        private readonly NewsRequest _instance;

        public NewsRequestBuilder()
        {
            _instance = new CreateNewsRequest
            {
                Title = "Breaking News",
                Content = "Any thing intresting",
                Author = new AuthorRequestBuilder().Build()
            };
        }

        public NewsRequestBuilder(Guid id)
        {
            _instance = new UpdateNewsRequest
            {
                Id = id,
                Title = "Breaking News",
                Content = "Any thing intresting",
                Author = new AuthorRequestBuilder().Build()
            };
        }

        public NewsRequestBuilder WithTitle(string title)
        {
            _instance.Title = title;
            return this;
        }

        public NewsRequestBuilder WithContent(string content)
        {
            _instance.Content = content;
            return this;
        }

        public NewsRequestBuilder WithAuthor(AuthorRequest author)
        {
            _instance.Author = author;
            return this;
        }

        public CreateNewsRequest BuildCreate() => (CreateNewsRequest)_instance;
        public UpdateNewsRequest BuildUpdate() => (UpdateNewsRequest)_instance;
        public NewsRequest Build() => _instance;
    }
}