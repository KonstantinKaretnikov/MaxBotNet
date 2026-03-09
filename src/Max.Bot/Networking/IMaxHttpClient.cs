using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Max.Bot.Networking;

/// <summary>
/// Interface for HTTP client that handles communication with the Max Bot API.
/// </summary>
public interface IMaxHttpClient : IDisposable
{
    /// <summary>
    /// Sends an HTTP request and deserializes the response.
    /// </summary>
    Task<TResponse> SendAsync<TResponse>(MaxApiRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an HTTP request without expecting a response body.
    /// </summary>
    Task SendAsync(MaxApiRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an HTTP request and returns the raw response body as a string.
    /// </summary>
    Task<string> SendAsyncRaw(MaxApiRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an HTTP request with arbitrary content to an absolute URL.
    /// Used for file uploading where the upload URL is often external.
    /// </summary>
    /// <param name="absoluteUrl">The absolute URL for the request.</param>
    /// <param name="contentFactory">A factory that creates the HTTP content to send. Needed for retries.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <param name="method">The HTTP method for the request (default is POST).</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the raw response body.</returns>
    Task<string> SendAsyncRaw(string absoluteUrl, Func<HttpContent?>? contentFactory = null, CancellationToken cancellationToken = default, HttpMethod? method = null);
}
