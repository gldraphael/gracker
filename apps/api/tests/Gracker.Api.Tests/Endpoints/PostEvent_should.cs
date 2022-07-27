using Gracker.Api.Endpoints;
using Gracker.MessageContracts;
using MassTransit;
using System.Net;
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

        (await Harness.Published.Any<EventReceived>(x => true)).ShouldBeTrue();
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

public class PostEvent_PublishMessage_should : ApiTestBed
{
    [Theory]
    [MemberData(nameof(ValidArgs))]
    public async Task Publish_for_valid_values(DateTime dateTime, string fingerprint, string timezone, IPAddress? ip)
    {
        await PostEvent.PublishMessage(Bus, dateTime, fingerprint, timezone, ip);
        (await Harness.Published.Any<EventReceived>(x => true)).ShouldBeTrue();
    }

    public static IEnumerable<object[]> ValidArgs()
    {
        // Random default value
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        yield return new object[] { DateTime.UtcNow, "unique-1x", "Asia/Calcutta", (IPAddress?)null };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        // IP Address combinations
        yield return new object[] { DateTime.UtcNow, "unique-2x", "Asia/Calcutta", IPAddress.Any };
        yield return new object[] { DateTime.UtcNow, "unique-3x", "Asia/Calcutta", IPAddress.IPv6Any };
        yield return new object[] { DateTime.UtcNow, "unique-4x", "Asia/Calcutta", IPAddress.Loopback };
        yield return new object[] { DateTime.UtcNow, "unique-5x", "Asia/Calcutta", IPAddress.IPv6Loopback };
        yield return new object[] { DateTime.UtcNow, "unique-6x", "Asia/Calcutta", IPAddress.Parse("66.249.79.96") };
        yield return new object[] { DateTime.UtcNow, "unique-7x", "Asia/Calcutta", IPAddress.Parse("2001:4860:4801:44::ca:4b") };
    }
}
