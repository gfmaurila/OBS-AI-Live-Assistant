using System.Text;
using ObsAi.Application.Contracts.Chat;

namespace ObsAi.Application.Validation;

/// <summary>
/// Validates untrusted chat input and normalizes it to the common functional representation. The
/// validator is fail-closed: absent, malformed, non-printable, badly encoded or oversize input is
/// rejected with a safe diagnostic before it can reach a provider, trigger, moderation or queue sink
/// (RF-006, RF-007, RNF-004, SEC-001, SEC-002, SEC-019, SEC-024). No product numbers are fixed:
/// bounds come from <see cref="InputValidationLimits"/>.
/// </summary>
public static class ChatInputValidator
{
    /// <summary>
    /// Validates and normalizes a single untrusted chat message. Never throws for untrusted data;
    /// only null arguments or misuse of the limits throw, as programmer contract errors.
    /// </summary>
    public static ChatInputValidationResult Validate(ChatMessage input, InputValidationLimits limits)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(limits);

        if (!TryNormalizeReference(input.ChannelReference, limits.MaximumReferenceLength, out var channel, out var reason))
        {
            return ChatInputValidationResult.Rejected(InputRejection.Create(reason!.Value));
        }

        if (!TryNormalizeReference(input.MessageReference, limits.MaximumReferenceLength, out var messageReference, out reason))
        {
            return ChatInputValidationResult.Rejected(InputRejection.Create(reason!.Value));
        }

        if (!TryNormalizeReference(input.SenderReference, limits.MaximumReferenceLength, out var sender, out reason))
        {
            return ChatInputValidationResult.Rejected(InputRejection.Create(reason!.Value));
        }

        if (!TryNormalizeText(input.Text, limits.MaximumTextLength, out var text, out reason))
        {
            return ChatInputValidationResult.Rejected(InputRejection.Create(reason!.Value));
        }

        if (input.ReceivedAtUtc == default)
        {
            return ChatInputValidationResult.Rejected(InputRejection.Create(InputRejectionReason.MalformedTimestamp));
        }

        return ChatInputValidationResult.Accepted(
            new NormalizedChatMessage(
                input.ProviderId,
                channel,
                sender,
                messageReference,
                text,
                input.ReceivedAtUtc));
    }

    private static bool TryNormalizeReference(
        string reference,
        int maximumLength,
        out string normalized,
        out InputRejectionReason? reason)
    {
        if (string.IsNullOrWhiteSpace(reference))
        {
            normalized = null!;
            reason = InputRejectionReason.MissingRequiredField;
            return false;
        }

        normalized = reference.Trim();
        if (normalized.Any(char.IsControl))
        {
            normalized = null!;
            reason = InputRejectionReason.MalformedReference;
            return false;
        }

        if (normalized.Length > maximumLength)
        {
            normalized = null!;
            reason = InputRejectionReason.OversizeReference;
            return false;
        }

        reason = null;
        return true;
    }

    private static bool TryNormalizeText(
        string text,
        int maximumLength,
        out string normalized,
        out InputRejectionReason? reason)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            normalized = null!;
            reason = InputRejectionReason.MissingRequiredField;
            return false;
        }

        if (text.Length > maximumLength)
        {
            normalized = null!;
            reason = InputRejectionReason.OversizeText;
            return false;
        }

        if (HasUnpairedSurrogate(text))
        {
            normalized = null!;
            reason = InputRejectionReason.MalformedEncoding;
            return false;
        }

        normalized = text.Normalize(NormalizationForm.FormC);
        if (normalized.Any(char.IsControl))
        {
            normalized = null!;
            reason = InputRejectionReason.MalformedText;
            return false;
        }

        normalized = string.Join(' ', normalized.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));
        if (normalized.Length == 0)
        {
            normalized = null!;
            reason = InputRejectionReason.MissingRequiredField;
            return false;
        }

        if (normalized.Length > maximumLength)
        {
            normalized = null!;
            reason = InputRejectionReason.OversizeText;
            return false;
        }

        reason = null;
        return true;
    }

    private static bool HasUnpairedSurrogate(string value)
    {
        for (var index = 0; index < value.Length; index++)
        {
            var current = value[index];
            if (char.IsHighSurrogate(current))
            {
                if (index + 1 >= value.Length || !char.IsLowSurrogate(value[index + 1]))
                {
                    return true;
                }

                index++;
            }
            else if (char.IsLowSurrogate(current))
            {
                return true;
            }
        }

        return false;
    }
}
