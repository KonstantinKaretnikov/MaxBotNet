// рџ“Ѓ [UsersApi] - Р РµР°Р»РёР·Р°С†РёСЏ РјРµС‚РѕРґРѕРІ СЂР°Р±РѕС‚С‹ СЃ РїРѕР»СЊР·РѕРІР°С‚РµР»СЏРјРё
// рџЋЇ Core function: Р РµР°Р»РёР·Р°С†РёСЏ РјРµС‚РѕРґРѕРІ IUsersApi (GetUserAsync)
// рџ”— Key dependencies: Max.Bot.Api, Max.Bot.Configuration, Max.Bot.Networking, Max.Bot.Types
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ РІ MaxClient РґР»СЏ РїСЂРµРґРѕСЃС‚Р°РІР»РµРЅРёСЏ РјРµС‚РѕРґРѕРІ СЂР°Р±РѕС‚С‹ СЃ РїРѕР»СЊР·РѕРІР°С‚РµР»СЏРјРё

using System.Net.Http;
using Max.Bot.Configuration;
using Max.Bot.Networking;
using Max.Bot.Types;

namespace Max.Bot.Api;

/// <summary>
/// Implementation of user-related API methods.
/// </summary>
internal class UsersApi : BaseApi, IUsersApi
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UsersApi"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client to use for requests.</param>
    /// <param name="options">The bot options containing token and base URL.</param>
    /// <exception cref="ArgumentNullException">Thrown when httpClient or options is null.</exception>
    public UsersApi(IMaxHttpClient httpClient, MaxBotOptions options)
        : base(httpClient, options)
    {
    }

    /// <inheritdoc />
    public async Task<User> GetUserAsync(long userId, CancellationToken cancellationToken = default)
    {
        ValidateUserId(userId);

        var request = CreateRequest(HttpMethod.Get, $"/users/{userId}");
        return await ExecuteRequestAsync<User>(request, cancellationToken).ConfigureAwait(false);
    }
}

