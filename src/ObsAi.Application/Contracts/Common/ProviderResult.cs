namespace ObsAi.Application.Contracts.Common;

/// <summary>
/// Represents either a successful provider value or one normalized failure.
/// </summary>
public sealed class ProviderResult<T>
{
    internal ProviderResult(T value)
    {
        Value = value;
        IsSuccess = true;
    }

    internal ProviderResult(ProviderFailure failure)
    {
        Failure = failure;
    }

    public bool IsSuccess { get; }

    public T? Value { get; }

    public ProviderFailure? Failure { get; }

}

public static class ProviderResult
{
    public static ProviderResult<T> Success<T>(T value) => new(value ?? throw new ArgumentNullException(nameof(value)));

    public static ProviderResult<T> Failed<T>(ProviderFailure failure) => new(failure ?? throw new ArgumentNullException(nameof(failure)));
}
