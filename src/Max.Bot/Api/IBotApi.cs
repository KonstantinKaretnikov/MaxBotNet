// рџ“Ѓ [IBotApi] - РРЅС‚РµСЂС„РµР№СЃ РґР»СЏ РјРµС‚РѕРґРѕРІ СЂР°Р±РѕС‚С‹ СЃ Р±РѕС‚РѕРј
// рџЋЇ Core function: РћРїСЂРµРґРµР»СЏРµС‚ РєРѕРЅС‚СЂР°РєС‚ РґР»СЏ API РјРµС‚РѕРґРѕРІ СЂР°Р±РѕС‚С‹ СЃ Р±РѕС‚РѕРј
// рџ”— Key dependencies: Max.Bot.Types
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ РґР»СЏ dependency injection Рё СЃРѕР·РґР°РЅРёСЏ РјРѕРєРѕРІ РІ С‚РµСЃС‚Р°С…

using Max.Bot.Types;

namespace Max.Bot.Api;

/// <summary>
/// Interface for bot-related API methods.
/// </summary>
public interface IBotApi
{
    /// <summary>
    /// Gets information about the current bot.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the bot user information.</returns>
    /// <exception cref="Max.Bot.Exceptions.MaxApiException">Thrown when the API returns an error response.</exception>
    /// <exception cref="Max.Bot.Exceptions.MaxNetworkException">Thrown when a network error occurs.</exception>
    /// <exception cref="Max.Bot.Exceptions.MaxUnauthorizedException">Thrown when authentication fails.</exception>
    Task<User> GetMeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets detailed information about the bot.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the detailed bot information.</returns>
    /// <exception cref="Max.Bot.Exceptions.MaxApiException">Thrown when the API returns an error response.</exception>
    /// <exception cref="Max.Bot.Exceptions.MaxNetworkException">Thrown when a network error occurs.</exception>
    /// <exception cref="Max.Bot.Exceptions.MaxUnauthorizedException">Thrown when authentication fails.</exception>
    Task<User> GetBotInfoAsync(CancellationToken cancellationToken = default);
}

