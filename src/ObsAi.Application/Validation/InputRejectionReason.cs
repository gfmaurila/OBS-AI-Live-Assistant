namespace ObsAi.Application.Validation;

/// <summary>
/// Classifies why an untrusted input was rejected before it can reach any provider, moderation,
/// trigger detection or queue sink. Rejection reasons never carry the offending payload and are
/// safe to surface in logs and diagnostics (RNF-004, SEC-002, SEC-024).
/// </summary>
public enum InputRejectionReason
{
    /// <summary>A required field is missing, null or empty.</summary>
    MissingRequiredField = 1,

    /// <summary>An operational identity reference contains characters outside the allowed schema.</summary>
    MalformedReference = 2,

    /// <summary>The message text contains characters outside the allowed printable schema.</summary>
    MalformedText = 3,

    /// <summary>An input field is not well-formed Unicode (for example, an unpaired surrogate).</summary>
    MalformedEncoding = 4,

    /// <summary>A normalized reference exceeds the configured size limit.</summary>
    OversizeReference = 5,

    /// <summary>The normalized text exceeds the configured size limit.</summary>
    OversizeText = 6,

    /// <summary>The received-at timestamp is missing or invalid.</summary>
    MalformedTimestamp = 7,
}
