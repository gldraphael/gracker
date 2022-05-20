namespace Gracker.Api.Endpoints;

public static class PostEvent
{
    public static void Map(WebApplication app)
    {
        app.MapPost("v1/events", (EventRequest body, HttpContext c) =>
        {
            var fingerprint = body.Fingerprint;
            var timezone = body.Timezone;

            var timestamp = DateTimeOffset.UtcNow;

            var ipAddress = c.Connection.RemoteIpAddress;
            var acceptLanguage = c.Request.Headers.AcceptLanguage;
            var userAgent = c.Request.Headers.UserAgent;
            var referrer = c.Request.Headers.Referer;

            app.Logger.LogInformation(
                "Request received from {IPAddress} with fingerprint {Fingerprint} at {Timestamp}",
                ipAddress,
                fingerprint,
                timestamp);
            
            return Results.Accepted();
        });
    }

    private record EventRequest(string Fingerprint, string Timezone);
}
