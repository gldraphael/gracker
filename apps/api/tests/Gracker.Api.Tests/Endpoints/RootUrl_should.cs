namespace Gracker.Api.Tests.Endpoints;

public class RootUrl_should : ApiTestBed
{
    [Fact]
    public async Task Serve_swagger_files()
    {
        var response = await HttpClient.GetAsync("/");

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.ShouldContain(expected: "swagger");
    }
}
