using Microsoft.AspNetCore.Mvc.Testing;

namespace Gracker.Api.Tests.Endpoints;

public class RootUrl_should : IClassFixture<ApiTestBed>
{
    [Fact]
    public async Task Serve_swagger_files()
    {
        var http = api.CreateClient();
        var response = await http.GetAsync("/");

        response.EnsureSuccessStatusCode();
        Assert.Contains(expectedSubstring: "swagger", await response.Content.ReadAsStringAsync(), StringComparison.OrdinalIgnoreCase);
    }

    readonly WebApplicationFactory<Program> api;
    public RootUrl_should(ApiTestBed fixture)
    {
        api = fixture.Api;
    }
}
