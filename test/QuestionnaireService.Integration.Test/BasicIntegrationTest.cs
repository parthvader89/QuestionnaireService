using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using QuestionnaireService.Api;
using QuestionnaireService.Contracts;
using Xunit.Sdk;

namespace QuestionnaireService.Integration.Test;

public class BasicIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public BasicIntegrationTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task BasicHappyPathTest()
    {
        var client = _factory.CreateClient();
        var json = File.ReadAllTextAsync("data/happy-path-request.json");
        var request = await json;
        var httpResponse = await client.
            PostAsync("/scoring", new StringContent(request, 
                    Encoding.UTF8, 
                    "application/json"));

        var result = JsonConvert.
            DeserializeObject<QuestionnaireResponse>(
                await httpResponse.Content.ReadAsStringAsync());

        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}