using Gracker.Api.Endpoints;
using Gracker.MessageContracts;
using MassTransit.Testing;
using System.Net.Http.Json;

namespace Gracker.Api.Tests.Endpoints;

public class PostEvent_should : IClassFixture<ApiTestBed>
{
    [Fact]
    public async Task Return_2xx()
    {
        using var request = JsonContent.Create<PostEvent.EventRequest>(new(Fingerprint: "unique-0x", Timezone: "Asia/Calcutta"));
        var response = await client.PostAsync("/v1/event", request);

        response.EnsureSuccessStatusCode();

        (await harness.Published.Any<EventReceived>(x => true)).ShouldBeTrue(); ;
    }


    readonly HttpClient client;
    readonly ITestHarness harness;
    public PostEvent_should(ApiTestBed fixture)
    {
        client = fixture.HttpClient;
        harness = fixture.Harness;
    }
}
