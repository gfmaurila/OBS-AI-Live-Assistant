using ObsAi.Application.Contracts.Common;

namespace ObsAi.Application.Contracts.Chat;

/// <summary>
/// Vendor-neutral, untrusted chat data. Validation remains mandatory before application use.
/// </summary>
public sealed record ChatMessage(
    ProviderId ProviderId,
    string ChannelReference,
    string MessageReference,
    string SenderReference,
    string Text,
    DateTimeOffset ReceivedAtUtc);
