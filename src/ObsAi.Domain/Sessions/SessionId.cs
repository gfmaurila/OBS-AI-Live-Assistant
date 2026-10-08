namespace ObsAi.Domain.Sessions;

/// <summary>
/// Identifies one live session without exposing infrastructure-specific identity details.
/// </summary>
public sealed record SessionId
{
    private SessionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static SessionId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Session identifier cannot be empty.", nameof(value));
        }

        return new SessionId(value);
    }

    public override string ToString() => Value.ToString("D");
}
