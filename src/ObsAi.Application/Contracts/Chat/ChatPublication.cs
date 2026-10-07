using ObsAi.Application.Contracts.Common;

namespace ObsAi.Application.Contracts.Chat;

public sealed record ChatPublication(
    string ChannelReference,
    string ApprovedText,
    CredentialReference CredentialReference,
    RequestContext Context);
