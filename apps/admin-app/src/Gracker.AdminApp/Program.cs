var builder = WebApplication.CreateBuilder(args);

if(builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(o => o.AddDefaultPolicy(p => {
        p.AllowAnyOrigin();
        p.AllowAnyHeader();
        p.AllowAnyMethod();
    }));
}

var app = builder.Build();
if (builder.Environment.IsDevelopment()) app.UseCors();
app.UseFileServer();

#pragma warning disable CA5394 // Do not use insecure randomness
app.MapGet("/api/active-users", () => new { Count = Random.Shared.Next(0, 10) });
#pragma warning restore CA5394 // Do not use insecure randomness

if (builder.Environment.IsDevelopment()) app.MapGet("/", () => "To run the admin-app's SPA, cd into the react project folder, and run npm start");
app.Run();
