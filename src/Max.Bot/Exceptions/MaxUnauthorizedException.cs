// рџ“Ѓ [MaxUnauthorizedException] - РСЃРєР»СЋС‡РµРЅРёРµ РґР»СЏ РѕС€РёР±РѕРє Р°РІС‚РѕСЂРёР·Р°С†РёРё
// рџЋЇ Core function: РџСЂРµРґСЃС‚Р°РІР»СЏРµС‚ РѕС€РёР±РєРё Р°РІС‚РѕСЂРёР·Р°С†РёРё (HTTP 401/403)
// рџ”— Key dependencies: Max.Bot.Exceptions
// рџ’Ў Usage: Р’С‹Р±СЂР°СЃС‹РІР°РµС‚СЃСЏ РїСЂРё РЅРµР°РІС‚РѕСЂРёР·РѕРІР°РЅРЅРѕРј РґРѕСЃС‚СѓРїРµ Рє API

using System.Net;

namespace Max.Bot.Exceptions;

/// <summary>
/// Exception thrown when authentication or authorization fails (HTTP 401 or 403).
/// </summary>
public class MaxUnauthorizedException : MaxApiException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MaxUnauthorizedException"/> class.
    /// </summary>
    public MaxUnauthorizedException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MaxUnauthorizedException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public MaxUnauthorizedException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MaxUnauthorizedException"/> class with a specified error message and inner exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public MaxUnauthorizedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MaxUnauthorizedException"/> class with a specified error message, error code, and HTTP status code.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="errorCode">The error code from the API response.</param>
    /// <param name="httpStatusCode">The HTTP status code from the API response.</param>
    public MaxUnauthorizedException(string message, string? errorCode, HttpStatusCode? httpStatusCode)
        : base(message, errorCode, httpStatusCode)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MaxUnauthorizedException"/> class with a specified error message, error code, HTTP status code, and inner exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="errorCode">The error code from the API response.</param>
    /// <param name="httpStatusCode">The HTTP status code from the API response.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public MaxUnauthorizedException(string message, string? errorCode, HttpStatusCode? httpStatusCode, Exception innerException)
        : base(message, errorCode, httpStatusCode, innerException)
    {
    }
}

