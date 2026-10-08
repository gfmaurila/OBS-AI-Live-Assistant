using System.Collections.ObjectModel;
using ObsAi.Domain.Sessions;

namespace ObsAi.Domain.Context;

/// <summary>
/// Holds bounded, allowlisted and ephemeral context for exactly one live session.
/// </summary>
public sealed class LiveContext
{
    private readonly Dictionary<LiveContextField, string> entries;
    private readonly ReadOnlyDictionary<LiveContextField, string> readOnlyEntries;

    private LiveContext(SessionId sessionId, Dictionary<LiveContextField, string> entries)
    {
        SessionId = sessionId;
        this.entries = entries;
        readOnlyEntries = new ReadOnlyDictionary<LiveContextField, string>(entries);
    }

    public SessionId SessionId { get; }

    public bool IsCleared { get; private set; }

    public IReadOnlyDictionary<LiveContextField, string> Entries
    {
        get
        {
            EnsureAvailable();
            return readOnlyEntries;
        }
    }

    public static LiveContext Create(
        SessionId sessionId,
        IReadOnlyDictionary<LiveContextField, string> entries,
        LiveContextLimits limits)
    {
        ArgumentNullException.ThrowIfNull(sessionId);
        ArgumentNullException.ThrowIfNull(entries);
        ArgumentNullException.ThrowIfNull(limits);

        if (entries.Count > limits.MaximumFields)
        {
            throw new ArgumentOutOfRangeException(nameof(entries), "Live context exceeds the configured field limit.");
        }

        var normalizedEntries = new Dictionary<LiveContextField, string>(entries.Count);
        var totalLength = 0;
        foreach (var (field, value) in entries)
        {
            if (!Enum.IsDefined(field))
            {
                throw new ArgumentOutOfRangeException(nameof(entries), "Live context contains a field outside the allowlist.");
            }

            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(entries));
            var normalizedValue = value.Trim();
            if (normalizedValue.Length > limits.MaximumValueLength || normalizedValue.Any(char.IsControl))
            {
                throw new ArgumentOutOfRangeException(nameof(entries), "Live context values must be bounded and printable.");
            }

            totalLength = checked(totalLength + normalizedValue.Length);
            if (totalLength > limits.MaximumTotalLength)
            {
                throw new ArgumentOutOfRangeException(nameof(entries), "Live context exceeds the configured total length.");
            }

            normalizedEntries.Add(field, normalizedValue);
        }

        return new LiveContext(sessionId, normalizedEntries);
    }

    public bool TryGetValue(LiveContextField field, out string? value)
    {
        EnsureAvailable();
        return entries.TryGetValue(field, out value);
    }

    internal void Clear()
    {
        entries.Clear();
        IsCleared = true;
    }

    private void EnsureAvailable()
    {
        if (IsCleared)
        {
            throw new InvalidOperationException("Live context is no longer available after its session has ended.");
        }
    }
}
