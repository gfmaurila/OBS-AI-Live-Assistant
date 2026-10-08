using ObsAi.Application.Contracts.Common;

namespace ObsAi.Application.Validation;

/// <summary>
/// The normalized, common functional representation of one accepted chat message. It preserves only
/// the operational identity and origin required for later abuse controls and deduplication, plus the
/// normalized text; no provider-specific or extended metadata is carried forward (RF-007, SEC-024).
/// </summary>
public sealed record NormalizedChatMessage(
    ProviderId ProviderId,
    string ChannelReference,
    string SenderReference,
    string MessageReference,
    string Text,
    DateTimeOffset ReceivedAtUtc);
