using Microsoft.Graph;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Providers
{
    /// <summary>
    ///  HTTP provider to send requests.
    /// </summary>
    public class CustomHttpProvider : IHttpProvider
    {
        private readonly HttpClient _http;

        /// <summary>
        /// Creates a new instance of <see cref="CustomHttpProvider" />.
        /// </summary>
        /// <param name="http"><see cref="HttpClient" /> instance.</param>
        public CustomHttpProvider(HttpClient http)
        {
            this._http = http;
        }

        /// <inheritdoc/>
        public ISerializer Serializer { get; } = new Serializer();

        /// <inheritdoc/>
        public TimeSpan OverallTimeout { get; set; } = TimeSpan.FromSeconds(300);

        /// <inheritdoc/>
        public void Dispose()
        {
        }

        /// <inheritdoc/>
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return _http.SendAsync(request);
        }

        /// <inheritdoc/>
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            HttpCompletionOption completionOption,
            CancellationToken cancellationToken)
        {
            return _http.SendAsync(request, completionOption, cancellationToken);
        }
    }
}
