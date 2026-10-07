using ObsAi.Application.Contracts.Chat;
using ObsAi.Application.Contracts.Common;

namespace ObsAi.Application.Ports.Chat;

/// <summary>
/// Optional capability for chat providers that support publishing messages.
/// </summary>
public interface IChatPublisher
{
    ValueTask<ProviderResult<string>> PublishAsync(
        ChatPublication publication,
        CancellationToken cancellationToken);
}
