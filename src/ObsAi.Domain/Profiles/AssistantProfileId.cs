namespace ObsAi.Domain.Profiles;

/// <summary>
/// Identifies an assistant profile independently from its persisted representation.
/// </summary>
public sealed record AssistantProfileId
{
    private AssistantProfileId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static AssistantProfileId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Assistant profile identifier cannot be empty.", nameof(value));
        }

        return new AssistantProfileId(value);
    }

    public override string ToString() => Value.ToString("D");
}
