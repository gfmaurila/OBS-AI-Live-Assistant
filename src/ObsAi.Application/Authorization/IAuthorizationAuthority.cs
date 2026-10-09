namespace ObsAi.Application.Authorization;

/// <summary>
/// Port implemented by a trusted control boundary that verifies streamer authority. Authentication
/// and credential storage remain outside this contract and outside TASK-015.
/// </summary>
public interface IAuthorizationAuthority
{
    AuthorityVerification Verify(AuthorizationChallenge challenge);
}
