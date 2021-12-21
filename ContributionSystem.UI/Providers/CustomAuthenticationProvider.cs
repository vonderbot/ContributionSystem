using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Providers 
{
    /// <summary>
    /// Provides methods for authenticating requests.
    /// </summary>
    public class CustomAuthenticationProvider : IAuthenticationProvider
    {
        private const string ScopesSectionName = "Scopes";
        private const string HeaderScheme = "Bearer";

        private readonly IConfiguration _configuration;
        private readonly IAccessTokenProvider _tokenProvider;

        /// <summary>
        /// Creates a new instance of <see cref="CustomAuthenticationProvider" />.
        /// </summary>
        /// <param name="tokenProvider"><see cref="IAccessTokenProvider" /> instance.</param>
        /// <param name="configuration"><see cref="IConfiguration" /> instance.</param>
        public CustomAuthenticationProvider(IAccessTokenProvider tokenProvider, IConfiguration configuration)
        {
            _tokenProvider = tokenProvider;
            _configuration = configuration;
        }

        /// <inheritdoc/>
        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            var result = await _tokenProvider.RequestAccessToken(
                new AccessTokenRequestOptions()
                {
                    Scopes = _configuration.GetSection(ScopesSectionName).Get<string[]>()
                });

            if (result.TryGetToken(out var token))
            {
                request.Headers.Authorization ??= new AuthenticationHeaderValue(
                    HeaderScheme, token.Value);
            }
        }
    }
}
