using System.Net;

namespace Gracker.MessageContracts;

public sealed record EventReceived(
    DateTime TimestampUtc, // TODO: reconsider this
    string Fingerprint,
    string Timezone,
    IPAddress? IPAddress
);
