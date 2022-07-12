using Gracker.ServiceShell;
using Gracker.WorkerApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
builder.SetupGrackerService();

builder.Services.AddOptions<DbConfig>()
    .Bind(builder.Configuration.GetSection("Db"));

builder.Services.AddDbContext<GrackerDbContext>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
