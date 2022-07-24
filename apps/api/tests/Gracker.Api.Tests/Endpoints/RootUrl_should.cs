namespace Gracker.Api.Tests.Endpoints;

public class RootUrl_should : IClassFixture<ApiTestBed>
{
    [Fact]
    public async Task Serve_swagger_files()
    {
        var response = await http.GetAsync("/");

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.ShouldContain(expected: "swagger");
    }

    readonly HttpClient http;
    public RootUrl_should(ApiTestBed fixture)
    {
        http = fixture.HttpClient;
    }
}
