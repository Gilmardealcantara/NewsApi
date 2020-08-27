using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NewsApi.Domain.Dtos;
using Xunit;

namespace NewsApi.Api.IntegrationTests
{
    public class NewsIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public NewsIntegrationTests(WebApplicationFactory<Startup> factory)
            => _client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        [Fact]
        public async Task GetNews_ReturnSuccess()
        {
            var response = await _client.GetAsync("/news");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var data = await response.Content.ReadAsAsync<NewsListItem[]>();
            data.Should()
                .NotBeNullOrEmpty()
                .And.NotContainNulls(x => x.Title)
                .And.NotContainNulls(x => x.ContentPreview);
        }
    }
}
