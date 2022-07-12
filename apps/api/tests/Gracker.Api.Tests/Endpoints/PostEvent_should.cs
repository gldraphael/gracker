using Gracker.Api.Endpoints;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Gracker.Api.Tests.Endpoints;

public class PostEvent_should : IClassFixture<ApiTestBed>
{
    [Fact]
    public async Task Return_2xx()
    {
        using var client = api.CreateClient();
        using var request = JsonContent.Create<PostEvent.EventRequest>(new(Fingerprint: "unique-0x", Timezone: ""));
        var response = await client.PostAsync("/v1/event", request);

        response.EnsureSuccessStatusCode();
    }


    readonly WebApplicationFactory<Program> api;
    public PostEvent_should(ApiTestBed fixture)
    {
        api = fixture.Api;
    }
}
