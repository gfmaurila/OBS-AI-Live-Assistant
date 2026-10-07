using ObsAi.Application.Contracts.Common;

namespace ObsAi.Application.Contracts.Chat;

public sealed record ChatSubscription(
    string ChannelReference,
    CredentialReference CredentialReference,
    RequestContext Context);
