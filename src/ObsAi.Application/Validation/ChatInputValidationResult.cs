namespace ObsAi.Application.Validation;

/// <summary>
/// Fail-closed outcome of validating and normalizing one untrusted chat input: either an accepted
/// <see cref="NormalizedChatMessage"/> or a safe <see cref="InputRejection"/>, never both and never
/// neither. Inputs are rejected before any provider or queue sink (RF-007, RNF-004, SEC-001).
/// </summary>
public sealed record ChatInputValidationResult
{
    private ChatInputValidationResult(NormalizedChatMessage? message, InputRejection? rejection)
    {
        Message = message;
        Rejection = rejection;
    }

    /// <summary>Gets the normalized message when the input was accepted; otherwise <c>null</c>.</summary>
    public NormalizedChatMessage? Message { get; }

    /// <summary>Gets the safe rejection diagnostic when the input was rejected; otherwise <c>null</c>.</summary>
    public InputRejection? Rejection { get; }

    /// <summary>Gets a value indicating whether the input passed validation and normalization.</summary>
    public bool IsAccepted => Message is not null && Rejection is null;

    /// <summary>Gets a value indicating whether the input was rejected without reaching any sink.</summary>
    public bool IsRejected => Rejection is not null && Message is null;

    /// <summary>Creates an accepted outcome carrying the normalized message.</summary>
    public static ChatInputValidationResult Accepted(NormalizedChatMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);
        return new ChatInputValidationResult(message, null);
    }

    /// <summary>Creates a rejected outcome carrying the safe rejection diagnostic.</summary>
    public static ChatInputValidationResult Rejected(InputRejection rejection)
    {
        ArgumentNullException.ThrowIfNull(rejection);
        return new ChatInputValidationResult(null, rejection);
    }
}
