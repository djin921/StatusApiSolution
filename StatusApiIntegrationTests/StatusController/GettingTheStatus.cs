using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StatusApiIntegrationTests.StatusController
{
    public class GettingTheStatus : IClassFixture<TestingWebApiFactory<Program>>
    {

        private readonly HttpClient _client;

        public GettingTheStatus(TestingWebApiFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetA200StatusResponse()
        {
            var response = await _client.GetAsync("/status");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task HasCorrectMediaType()
        {
            var response = await _client.GetAsync("/status");

            Assert.Equal("application/json", response.Content?.Headers?.ContentType?.MediaType);
        }

        [Fact]
        public async Task HasCorrectEntity()
        {
            var response = await _client.GetAsync("/status");

            var responseMessage = await response.Content.ReadAsAsync<GetStatusResponse>();

            Assert.Equal("The Server is Great.. Thanks", responseMessage?.message);
            Assert.Equal(new DateTime(1969, 4, 20, 23, 59, 00), responseMessage?.lastChecked);
        }
    }

    public class GetStatusResponse
    {
        public string message { get; set; }
        public DateTime lastChecked { get; set; }
    }

}