using Gracker.Api.Endpoints;

namespace Gracker.Api;

public static class MapEndpointsExtension
{
    public static void MapEndpoints(this WebApplication app)
    {
        PostEvent.Map(app);
    }
}
