namespace ObsAi.Application.Authorization;

public enum AuthorityVerificationStatus
{
    Authorized = 1,
    IdentityMissing = 2,
    PermissionDenied = 3,
}

/// <summary>Returns a constrained result from the trusted authority boundary.</summary>
public sealed class AuthorityVerification
{
    private AuthorityVerification(AuthorityVerificationStatus status, AuthorizationGrant? grant)
    {
        Status = status;
        Grant = grant;
    }

    public AuthorityVerificationStatus Status { get; }

    public AuthorizationGrant? Grant { get; }

    public static AuthorityVerification Authorized(AuthorizationGrant grant)
    {
        ArgumentNullException.ThrowIfNull(grant);
        return new AuthorityVerification(AuthorityVerificationStatus.Authorized, grant);
    }

    public static AuthorityVerification Denied(AuthorityVerificationStatus status)
    {
        if (status is not AuthorityVerificationStatus.IdentityMissing and not AuthorityVerificationStatus.PermissionDenied)
        {
            throw new ArgumentOutOfRangeException(nameof(status), "A denial must use a defined denial status.");
        }

        return new AuthorityVerification(status, null);
    }
}
