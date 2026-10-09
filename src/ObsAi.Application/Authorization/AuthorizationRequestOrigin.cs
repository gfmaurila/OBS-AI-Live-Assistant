namespace ObsAi.Application.Authorization;

/// <summary>Identifies the trust boundary from which an authorization request arrived.</summary>
public enum AuthorizationRequestOrigin
{
    Unknown = 0,
    TrustedControl = 1,
    ViewerMessage = 2,
    AiOutput = 3,
}
