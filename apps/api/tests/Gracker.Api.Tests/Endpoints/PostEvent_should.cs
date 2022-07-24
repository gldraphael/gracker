using Gracker.Api.Endpoints;
using Gracker.MessageContracts;
using MassTransit;
using System.Net.Http.Json;

namespace Gracker.Api.Tests.Endpoints;

public class PostEvent_should : ApiTestBed
{
    [Fact]
    public async Task Return_2xx()
    {
        using var request = JsonContent.Create<PostEvent.EventRequest>(new(Fingerprint: "unique-0x", Timezone: "Asia/Calcutta"));
        var response = await HttpClient.PostAsync("/v1/event", request);

        response.EnsureSuccessStatusCode();

        (await Harness.Published.Any<EventReceived>(x => true)).ShouldBeTrue(); ;
    }

    [Fact]
    public async Task Publish_an_event()
    {
        using var request = JsonContent.Create<PostEvent.EventRequest>(new(Fingerprint: "unique-0x", Timezone: "Asia/Calcutta"));
        var response = await HttpClient.PostAsync("/v1/event", request);

        response.EnsureSuccessStatusCode();

        var publishedEvents = await Harness.Published.SelectAsync<EventReceived>(x => true, default).ToListAsync();
        publishedEvents.Any().ShouldBeTrue();
        publishedEvents.ShouldHaveSingleItem();
    }
}
