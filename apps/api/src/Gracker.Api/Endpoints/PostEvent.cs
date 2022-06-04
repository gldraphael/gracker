using Gracker.MessageContracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Gracker.Api.Endpoints;

public static class PostEvent
{
    public static void Map(WebApplication app)
    {
        app.MapPost("v1/event", async ([FromBody]EventRequest body, HttpContext c, [FromServices] IBus bus) =>
        {
            var fingerprint = body.Fingerprint;
            var timezone = body.Timezone;

            var timestamp = DateTime.UtcNow;

            var ipAddress = c.Connection.RemoteIpAddress;
            var acceptLanguage = c.Request.Headers.AcceptLanguage;
            var userAgent = c.Request.Headers.UserAgent;
            var referrer = c.Request.Headers.Referer;

            app.Logger.LogInformation(
                "Request received from {IPAddress} with fingerprint {Fingerprint} at {Timestamp}",
                ipAddress,
                fingerprint,
                timestamp);

            await bus.Publish<EventReceived>(new(
                TimestampUtc: timestamp,
                Fingerprint: fingerprint,
                Timezone: timezone,
                IPAddress: ipAddress
            )).ConfigureAwait(false);

            return Results.Accepted();
        });
    }

    private record EventRequest(string Fingerprint, string Timezone);
}
