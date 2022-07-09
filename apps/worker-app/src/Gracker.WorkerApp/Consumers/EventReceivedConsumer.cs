using Gracker.MessageContracts;
using Gracker.WorkerApp.Infrastructure.Data;
using Gracker.WorkerApp.Infrastructure.Data.Models;
using MassTransit;

namespace Gracker.WorkerApp.Consumers;

public class EventReceivedConsumer : IConsumer<EventReceived>
{
    private readonly GrackerDbContext db;

    public EventReceivedConsumer(GrackerDbContext db)
    {
        this.db = db;
    }

    public async Task Consume(ConsumeContext<EventReceived> context)
    {
        var dao = new RawEventDAO
        {
            Fingerprint = context.Message.Fingerprint,
            IPAddress = context.Message.IPAddress,
            TimestampUtc = context.Message.TimestampUtc
        };
        db.RawEvents.Add(dao);
        await db.SaveChangesAsync().ConfigureAwait(false);
    }
}
