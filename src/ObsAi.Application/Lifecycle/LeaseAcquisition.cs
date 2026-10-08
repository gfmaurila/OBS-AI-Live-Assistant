namespace ObsAi.Application.Lifecycle;

/// <summary>
/// Outcome of an operation lease request: an accepted lease or an explicit rejection reason.
/// </summary>
public readonly record struct LeaseAcquisition(OperationLease? Lease, LeaseRejectionReason Reason)
{
    /// <summary>Gets a value indicating whether a lease was granted.</summary>
    public bool IsAccepted => Lease is not null;

    /// <summary>Creates an accepted acquisition.</summary>
    public static LeaseAcquisition Accepted(OperationLease lease)
    {
        ArgumentNullException.ThrowIfNull(lease);
        return new LeaseAcquisition(lease, LeaseRejectionReason.None);
    }

    /// <summary>Creates a rejected acquisition with the reason that blocked it.</summary>
    public static LeaseAcquisition Rejected(LeaseRejectionReason reason)
    {
        if (reason == LeaseRejectionReason.None)
        {
            throw new ArgumentOutOfRangeException(nameof(reason), "A rejected acquisition must carry an explicit rejection reason.");
        }

        return new LeaseAcquisition(null, reason);
    }
}
