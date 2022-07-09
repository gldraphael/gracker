using System.Net;

namespace Gracker.WorkerApp.Infrastructure.Data.Models;

public class RawEventDAO
{
    public int Id { get; set; }
    public DateTime TimestampUtc { get; set; }
    public string Fingerprint { get; set; } = null!;
    public IPAddress? IPAddress { get; set; }
}
