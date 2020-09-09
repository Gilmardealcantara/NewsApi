using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using NewsApi.Api.Configurations;
using NewsApi.Application.Dtos;
using Newtonsoft.Json.Linq;
using Xunit;

namespace NewsApi.Api.IntegrationTests
{
    public class NewsIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly ApplicationConfig _applicationConfig;
        private readonly HttpClient _client;

        public NewsIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _applicationConfig = factory.ApplicationConfig;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        }

        [Fact]
        public async Task GetNews_WhenOk_ReturnSuccess()
        {
            var response = await _client.GetAsync("/news");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var data = await response.Content.ReadAsAsync<NewsListItem[]>();
            data.Should()
                .NotBeNullOrEmpty()
                .And.NotContainNulls(x => x.Title)
                .And.NotContainNulls(x => x.ContentPreview);
        }

        [Fact]
        public async Task GetNewsById_whenOk_ReturnSuccess()
        {
            var response = await _client.GetAsync("/news/3b2c1964-cdd4-423e-9919-c22bd8182dd9");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var data = await response.Content.ReadAsAsync<JObject>();

            data.Should().NotBeNull()
                .And.ContainKey("title")
                .And.ContainKey("content")
                .And.ContainKey("comments")
                .And.Contain("numComments", 5);

            data["comments"].ToObject<JArray>()
                .Should().NotBeNullOrEmpty()
                .And.NotContainNulls(x => x["text"].ToString())
                .And.NotContainNulls(x => x["author"].ToString());
        }

        [Fact]
        public async Task GetNewsById_WhenNewsNotExists_ReturnBadNoContent()
        {
            var response = await _client.GetAsync("/news/3b2c1964-cdd4-423e-9919-c22bd8182dd1");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PostNews_WhenNotHaveAToken_ReturnUnauthorized()
        {
            var response = await _client.PostAsJsonAsync("/news", new { });
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task PostNews_WhenOk_ReturnNewsWithId()
        {
            var payload = new
            {
                Title = "Test title",
                Content = "Test Content",
                Author = new
                {
                    userName = "gilmardealcantara@gmail.com",
                    name = "Gilmar de Alcantara",
                }
            };
            var token = TokenFactory.GetToken(_applicationConfig.Authorization);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            var response = await _client.PostAsJsonAsync("/news", payload);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsAsync<JObject>();
            result.ContainsKey("id");
        }
    }
}
