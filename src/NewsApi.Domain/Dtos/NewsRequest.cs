using System;
using NewsApi.Domain.Entities;

namespace NewsApi.Domain.Dtos
{
    public class CreateNewsRequest
    {
        public string Title { get; set; } = null!;
        public News ToNews() => new News(Title);
    }

    public class UpdateNewsRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public News ToNews() => new News(this.Id, Title);
    }
}