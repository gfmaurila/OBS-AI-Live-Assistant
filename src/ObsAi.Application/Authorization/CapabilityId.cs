namespace ObsAi.Application.Authorization;

/// <summary>Identifies one protected capability without approving any concrete implementation.</summary>
public sealed record CapabilityId
{
    private const int MaximumLength = 128;

    private CapabilityId(string value)
    {
        Value = value;
    }

    /// <summary>Gets the canonical, non-sensitive capability name.</summary>
    public string Value { get; }

    /// <summary>Creates a canonical capability identifier suitable for exact allowlist matching.</summary>
    public static CapabilityId Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (value.Length > MaximumLength)
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"Capability identifiers cannot exceed {MaximumLength} characters.");
        }

        string canonicalValue = value.Trim().ToLowerInvariant();
        if (!IsAlphaNumeric(canonicalValue[0])
            || !IsAlphaNumeric(canonicalValue[^1])
            || canonicalValue.Any(character => !IsAlphaNumeric(character) && character is not '.' and not '-'))
        {
            throw new ArgumentException("Capability identifiers must use lowercase letters, digits, dots or hyphens and start and end with a letter or digit.", nameof(value));
        }

        return new CapabilityId(canonicalValue);
    }

    public override string ToString() => Value;

    private static bool IsAlphaNumeric(char character) =>
        character is >= 'a' and <= 'z' or >= '0' and <= '9';
}
