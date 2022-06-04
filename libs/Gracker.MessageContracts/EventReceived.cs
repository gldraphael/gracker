using System.Net;

namespace Gracker.MessageContracts;

public record EventReceived(
    DateTime TimestampUtc, // TODO: reconsider this
    string Fingerprint,
    string Timezone,
    IPAddress? IPAddress
);
