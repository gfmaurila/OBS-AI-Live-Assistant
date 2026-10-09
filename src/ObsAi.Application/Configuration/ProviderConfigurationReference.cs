namespace ObsAi.Application.Configuration;

/// <summary>
/// Opaque reference to provider-specific non-secret configuration. It never carries option values
/// or credential material across the common configuration boundary.
/// </summary>
public sealed record ProviderConfigurationReference
{
    private const int MaximumLength = 128;

    private ProviderConfigurationReference(string id)
    {
        Id = id;
    }

    public string Id { get; }

    public static ProviderConfigurationReference Create(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var normalized = id.Trim();
        if (normalized.Length > MaximumLength || !normalized.All(IsReferenceCharacter))
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Provider configuration reference has an invalid format.");
        }

        return new ProviderConfigurationReference(normalized);
    }

    public override string ToString() => "[provider-configuration-reference]";

    private static bool IsReferenceCharacter(char value) =>
        char.IsAsciiLetterOrDigit(value) || value is '-' or '_' or '.' or ':' or '/';
}
