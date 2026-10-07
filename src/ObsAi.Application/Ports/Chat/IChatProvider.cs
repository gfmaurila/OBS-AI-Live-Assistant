using ObsAi.Application.Contracts.Chat;
using ObsAi.Application.Contracts.Common;

namespace ObsAi.Application.Ports.Chat;

public interface IChatProvider
{
    ProviderId Id { get; }

    IAsyncEnumerable<ProviderResult<ChatMessage>> ReceiveAsync(
        ChatSubscription subscription,
        CancellationToken cancellationToken);
}
