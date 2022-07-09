using Gracker.WorkerApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Gracker.WorkerApp.Infrastructure.Data;

// TODO: make this internal
public class GrackerDbContext : DbContext
{
    

    public DbSet<RawEventDAO> RawEvents => Set<RawEventDAO>();


    private readonly DbConfig _config;
    public GrackerDbContext(IOptions<DbConfig> options)
    {
        _config = options.Value;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql($"Host={_config.Host};Database=my_db;Username=my_user;Password=my_pw");
    }
}
