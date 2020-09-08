using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NewsApi.Application.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        [Fact]
        public async Task GetNewsById_ReturnSuccess()
        {
            var response = await _client.GetAsync("/news/3b2c1964-cdd4-423e-9919-c22bd8182dd9");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var data = await response.Content.ReadAsAsync<JObject>();

            data.Should().NotBeNull()
                .And.ContainKey("title")
                .And.ContainKey("content")
                .And.ContainKey("comments")
                .And.Contain("numComments", 5)
            ;
            data["comments"].ToObject<JArray>()
                .Should().NotBeNullOrEmpty()
                .And.NotContainNulls(x => x["text"].ToString())
                .And.NotContainNulls(x => x["author"].ToString());
        }
    }
}
