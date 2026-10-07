using ObsAi.Application.Contracts.Common;
using Xunit;

namespace ObsAi.Unit.Tests;

public sealed class CommonContractTests
{
    [Fact]
    public void CredentialReference_NormalizesIdentifierAndRedactsStringRepresentation()
    {
        const string identifier = "credential-reference-42";

        var reference = CredentialReference.Create($"  {identifier}  ");

        Assert.Equal(identifier, reference.Id);
        Assert.DoesNotContain(identifier, reference.ToString(), StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("invalid\nreference")]
    public void CredentialReference_RejectsInvalidIdentifiers(string identifier)
    {
        Assert.ThrowsAny<ArgumentException>(() => CredentialReference.Create(identifier));
    }

    [Fact]
    public void ProviderId_NormalizesPrintableIdentifier()
    {
        var providerId = ProviderId.Create("  provider-a  ");

        Assert.Equal("provider-a", providerId.Value);
        Assert.Equal("provider-a", providerId.ToString());
    }

    [Fact]
    public void ProviderResult_SuccessContainsOnlyValue()
    {
        var result = ProviderResult.Success("response");

        Assert.True(result.IsSuccess);
        Assert.Equal("response", result.Value);
        Assert.Null(result.Failure);
    }

    [Fact]
    public void ProviderResult_FailureContainsOnlyNormalizedFailure()
    {
        var failure = new ProviderFailure(ProviderFailureCode.Unavailable, "Provider unavailable.", true);

        var result = ProviderResult.Failed<string>(failure);

        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.Same(failure, result.Failure);
    }

    [Fact]
    public void ProviderResult_RejectsNullAlternatives()
    {
        Assert.Throws<ArgumentNullException>(() => ProviderResult.Success<string>(null!));
        Assert.Throws<ArgumentNullException>(() => ProviderResult.Failed<string>(null!));
    }
}
