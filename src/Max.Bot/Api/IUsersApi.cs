// рџ“Ѓ [IUsersApi] - РРЅС‚РµСЂС„РµР№СЃ РґР»СЏ РјРµС‚РѕРґРѕРІ СЂР°Р±РѕС‚С‹ СЃ РїРѕР»СЊР·РѕРІР°С‚РµР»СЏРјРё
// рџЋЇ Core function: РћРїСЂРµРґРµР»СЏРµС‚ РєРѕРЅС‚СЂР°РєС‚ РґР»СЏ API РјРµС‚РѕРґРѕРІ СЂР°Р±РѕС‚С‹ СЃ РїРѕР»СЊР·РѕРІР°С‚РµР»СЏРјРё
// рџ”— Key dependencies: Max.Bot.Types
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ РґР»СЏ dependency injection Рё СЃРѕР·РґР°РЅРёСЏ РјРѕРєРѕРІ РІ С‚РµСЃС‚Р°С…

using Max.Bot.Types;

namespace Max.Bot.Api;

/// <summary>
/// Interface for user-related API methods.
/// </summary>
public interface IUsersApi
{
    /// <summary>
    /// Gets information about a user by their identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user information.</returns>
    /// <exception cref="ArgumentException">Thrown when userId is less than or equal to zero.</exception>
    /// <exception cref="Max.Bot.Exceptions.MaxApiException">Thrown when the API returns an error response.</exception>
    /// <exception cref="Max.Bot.Exceptions.MaxNetworkException">Thrown when a network error occurs.</exception>
    /// <exception cref="Max.Bot.Exceptions.MaxUnauthorizedException">Thrown when authentication fails.</exception>
    Task<User> GetUserAsync(long userId, CancellationToken cancellationToken = default);
}

