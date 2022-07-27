using Gracker.MessageContracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            // var userAgent = c.Request.Headers.UserAgent;
            var referrer = c.Request.Headers.Referer;

            app.Logger.LogInformation(
                "Request received from {IPAddress} with fingerprint {Fingerprint} at {Timestamp}",
                ipAddress,
                fingerprint,
                timestamp);

            await PublishMessage(bus, timestamp, fingerprint, timezone, ipAddress).ConfigureAwait(false);
            return Results.Accepted();
        });
    }

    internal static async Task PublishMessage(IBus bus, DateTime timestamp, string fingerprint, string timezone, IPAddress? ipAddress)
    {
        await bus.Publish<EventReceived>(new(
                TimestampUtc: timestamp,
                Fingerprint: fingerprint,
                Timezone: timezone,
                IPAddress: ipAddress
            )).ConfigureAwait(false);
    }

    internal record EventRequest(string Fingerprint, string Timezone);
}
