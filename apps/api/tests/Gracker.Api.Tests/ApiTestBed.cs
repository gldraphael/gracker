using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Gracker.Api.Tests;

public class ApiTestBed : IDisposable
{

    private  WebApplicationFactory<Program> Api { get; }

    public HttpClient HttpClient { get; }
    public ITestHarness Harness { get; }

    public ApiTestBed()
    {
#pragma warning disable CA2000 // Dispose objects before losing scope -- being done in Dispose(bool)
        Api = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddMassTransitTestHarness();
            });
        });
#pragma warning restore CA2000 // Dispose objects before losing scope


        HttpClient = Api.CreateClient();
        Harness = Api.Services.GetTestHarness();
    }







    private bool disposedValue;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                HttpClient.Dispose();
                Api.Dispose();
            }
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
