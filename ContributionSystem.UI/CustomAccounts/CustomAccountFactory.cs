using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;

namespace ContributionSystem.UI.CustomAccounts
{
    /// <summary>
    /// Provides methods for setting up a user.
    /// </summary>
    public class CustomAccountFactory
    : AccountClaimsPrincipalFactory<CustomUserAccount>
    {
        private readonly ILogger<CustomAccountFactory> _logger;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Creates a new instance of <see cref="CustomAccountFactory" />.
        /// </summary>
        /// <param name="accessor"><see cref="IAccessTokenProviderAccessor" /> instance.</param>
        /// <param name="serviceProvider"><see cref="IServiceProvider" /> instance.</param>
        /// <param name="logger"><see cref="ILogger" /> instance.</param>
        public CustomAccountFactory(IAccessTokenProviderAccessor accessor,
            IServiceProvider serviceProvider,
            ILogger<CustomAccountFactory> logger)
            : base(accessor)
        {
            this._serviceProvider = serviceProvider;
            this._logger = logger;
        }

        /// <summary>
        /// Сreates user.
        /// </summary>
        /// <param name="account"><see cref="CustomUserAccount" /> instance.</param>
        /// <param name="options"><see cref="RemoteAuthenticationUserOptions" /> instance.</param>
        /// <returns>New user with claims.</returns>
        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(
            CustomUserAccount account,
            RemoteAuthenticationUserOptions options)
        {
            var initialUser = await base.CreateUserAsync(account, options);

            if (initialUser.Identity.IsAuthenticated)
            {
                var userIdentity = (ClaimsIdentity)initialUser.Identity;

                foreach (var role in account.Roles)
                {
                    userIdentity.AddClaim(new Claim("roles", role));
                }

                foreach (var wid in account.Wids)
                {
                    userIdentity.AddClaim(new Claim("directoryRole", wid));
                }

                try
                {
                    var graphClient = ActivatorUtilities
                        .CreateInstance<GraphServiceClient>(_serviceProvider);

                    var requestMemberOf = graphClient.Users[account.Oid].MemberOf;
                    var memberships = await requestMemberOf.Request().GetAsync();

                    if (memberships != null)
                    {
                        foreach (var entry in memberships)
                        {
                            if (entry.ODataType == "#microsoft.graph.group")
                            {
                                userIdentity.AddClaim(
                                    new Claim("directoryGroup", entry.Id));
                            }
                        }
                    }
                }
                catch (ServiceException exception)
                {
                    _logger.LogError("Graph API service failure: {Message}",
                        exception.Message);
                }
            }

            return initialUser;
        }
    }
}