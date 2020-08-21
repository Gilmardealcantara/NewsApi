using NewsApi.Domain.Entities;

namespace NewsApi.Domain.Dtos
{
    public class CreateNewsRequest
    {
        public string? Title { get; set; }
        public News ToNews() => new News(Title ?? "");
    }
}