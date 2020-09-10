using System;
using NewsApi.Application.Entities;

namespace NewsApi.Application.Tests.Builders
{
    public class NewsBuilder
    {
        private readonly Entities.News _instance;

        public NewsBuilder()
        {
            var author = new Author("gilmardealcantara@gmail.com", "Gilmar Alcantara");
            _instance = new News("Breaking News", "Any thing intresting", author);
        }

        public NewsBuilder(Guid id)
        {
            var author = new Author("gilmardealcantara@gmail.com", "Gilmar Alcantara");
            _instance = new News(id, "Breaking News", "Any thing intresting", author);
        }

        public Entities.News Build() => _instance;
    }
}