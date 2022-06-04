using Gracker.Api;
using Gracker.ServiceShell;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddDefaultPolicy(p => {
    p.AllowAnyOrigin();
    p.WithHeaders("Content-Type");
}));
builder.Services.AddMassTransit(builder.Configuration);

var app = builder.Build();

app.UseSwagger(c => {
    c.RouteTemplate = "docs/{documentName}/openapi.{json|yaml}";
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("docs/v1/openapi.json", "v1");
    c.DocumentTitle = "Gracker API";
    c.RoutePrefix = string.Empty;
});

app.UseCors();
app.MapEndpoints();

app.Run();
