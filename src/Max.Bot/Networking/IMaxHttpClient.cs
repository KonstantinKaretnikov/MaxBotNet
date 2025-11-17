// рџ“Ѓ [IMaxHttpClient] - РРЅС‚РµСЂС„РµР№СЃ HTTP РєР»РёРµРЅС‚Р°
// рџЋЇ Core function: РћРїСЂРµРґРµР»СЏРµС‚ РєРѕРЅС‚СЂР°РєС‚ РґР»СЏ HTTP РєР»РёРµРЅС‚Р°, РѕР±РµСЃРїРµС‡РёРІР°СЏ С‚РµСЃС‚РёСЂСѓРµРјРѕСЃС‚СЊ С‡РµСЂРµР· РјРѕРєРё
// рџ”— Key dependencies: Max.Bot.Networking
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ РґР»СЏ dependency injection Рё СЃРѕР·РґР°РЅРёСЏ РјРѕРєРѕРІ РІ С‚РµСЃС‚Р°С…

namespace Max.Bot.Networking;

/// <summary>
/// Interface for HTTP client that handles communication with the Max Bot API.
/// </summary>
public interface IMaxHttpClient
{
    /// <summary>
    /// Sends an HTTP request and deserializes the response.
    /// </summary>
    /// <typeparam name="TResponse">The type to deserialize the response to.</typeparam>
    /// <param name="request">The API request to send.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the deserialized response.</returns>
    /// <exception cref="Max.Bot.Exceptions.MaxApiException">Thrown when the API returns an error response.</exception>
    /// <exception cref="Max.Bot.Exceptions.MaxNetworkException">Thrown when a network error occurs.</exception>
    /// <exception cref="Max.Bot.Exceptions.MaxRateLimitException">Thrown when rate limit is exceeded.</exception>
    /// <exception cref="Max.Bot.Exceptions.MaxUnauthorizedException">Thrown when authentication or authorization fails.</exception>
    Task<TResponse> SendAsync<TResponse>(MaxApiRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an HTTP request without expecting a response body.
    /// </summary>
    /// <param name="request">The API request to send.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="Max.Bot.Exceptions.MaxApiException">Thrown when the API returns an error response.</exception>
    /// <exception cref="Max.Bot.Exceptions.MaxNetworkException">Thrown when a network error occurs.</exception>
    /// <exception cref="Max.Bot.Exceptions.MaxRateLimitException">Thrown when rate limit is exceeded.</exception>
    /// <exception cref="Max.Bot.Exceptions.MaxUnauthorizedException">Thrown when authentication or authorization fails.</exception>
    Task SendAsync(MaxApiRequest request, CancellationToken cancellationToken = default);
}

