using Microsoft.AspNetCore.Mvc.Testing;

namespace Gracker.Api.Tests;

public class ApiTestBed : IDisposable
{

    public WebApplicationFactory<Program> Api { get; }

    public ApiTestBed()
    {
        Api = new WebApplicationFactory<Program>();
        Api.WithWebHostBuilder(builder =>
        {
            // ... Configure test services
        });
    }







    private bool disposedValue;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
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
