namespace ObsAi.Application.Configuration;

/// <summary>
/// Defines a trusted product range and its explicit default without hardcoding product values in
/// the configuration model.
/// </summary>
public sealed record BoundedInt32Setting
{
    private BoundedInt32Setting(int minimum, int maximum, int defaultValue)
    {
        Minimum = minimum;
        Maximum = maximum;
        DefaultValue = defaultValue;
    }

    public int Minimum { get; }

    public int Maximum { get; }

    public int DefaultValue { get; }

    public static BoundedInt32Setting Create(int minimum, int maximum, int defaultValue)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(minimum);
        ArgumentOutOfRangeException.ThrowIfLessThan(maximum, minimum);

        if (defaultValue < minimum || defaultValue > maximum)
        {
            throw new ArgumentOutOfRangeException(nameof(defaultValue), "Default value must be inside the approved range.");
        }

        return new BoundedInt32Setting(minimum, maximum, defaultValue);
    }

    internal bool Contains(int value) => value >= Minimum && value <= Maximum;
}
