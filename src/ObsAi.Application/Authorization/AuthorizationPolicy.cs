namespace ObsAi.Application.Authorization;

/// <summary>An immutable exact-match allowlist for protected capabilities.</summary>
public sealed class AuthorizationPolicy
{
    private readonly HashSet<CapabilityId> allowlistedCapabilities;

    private AuthorizationPolicy(HashSet<CapabilityId> allowlistedCapabilities)
    {
        this.allowlistedCapabilities = allowlistedCapabilities;
    }

    /// <summary>Gets the number of explicitly allowlisted capabilities.</summary>
    public int AllowlistedCapabilityCount => allowlistedCapabilities.Count;

    /// <summary>Copies the supplied capabilities into an immutable policy snapshot.</summary>
    public static AuthorizationPolicy Create(IEnumerable<CapabilityId> allowlistedCapabilities)
    {
        ArgumentNullException.ThrowIfNull(allowlistedCapabilities);

        var snapshot = new HashSet<CapabilityId>();
        foreach (CapabilityId capability in allowlistedCapabilities)
        {
            ArgumentNullException.ThrowIfNull(capability);
            snapshot.Add(capability);
        }

        return new AuthorizationPolicy(snapshot);
    }

    /// <summary>Returns true only for a capability explicitly present in this policy snapshot.</summary>
    public bool IsAllowlisted(CapabilityId capability)
    {
        ArgumentNullException.ThrowIfNull(capability);
        return allowlistedCapabilities.Contains(capability);
    }
}
