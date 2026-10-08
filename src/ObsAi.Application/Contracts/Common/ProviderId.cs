namespace ObsAi.Application.Contracts.Common;

/// <summary>
/// Identifies a provider implementation without coupling the application to a vendor SDK.
/// </summary>
public sealed record ProviderId
{
    private const int MaximumLength = 128;

    private ProviderId(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static ProviderId Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        var normalizedValue = value.Trim();
        if (normalizedValue.Length > MaximumLength || normalizedValue.Any(char.IsControl))
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Provider identifier must be a short, printable value.");
        }

        return new ProviderId(normalizedValue);
    }

    public override string ToString() => Value;
}
