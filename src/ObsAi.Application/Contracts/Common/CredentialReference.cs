namespace ObsAi.Application.Contracts.Common;

/// <summary>
/// Identifies credential material held by a secure store without carrying the secret value.
/// </summary>
public sealed record CredentialReference
{
    private const int MaximumLength = 256;

    private CredentialReference(string id)
    {
        Id = id;
    }

    public string Id { get; }

    public static CredentialReference Create(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var normalizedId = id.Trim();
        if (normalizedId.Length > MaximumLength || normalizedId.Any(char.IsControl))
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Credential reference must be a short, printable identifier.");
        }

        return new CredentialReference(normalizedId);
    }

    public override string ToString() => "[credential-reference]";
}
