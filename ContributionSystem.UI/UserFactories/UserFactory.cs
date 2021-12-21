using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ContributionSystem.UI.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;

namespace ContributionSystem.UI.UserFactories
{
    /// <summary>
    /// Provides methods for setting up a user.
    /// </summary>
    public class UserFactory
    : AccountClaimsPrincipalFactory<UserAccountModel>
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Creates a new instance of <see cref="UserFactory" />.
        /// </summary>
        /// <param name="accessor"><see cref="IAccessTokenProviderAccessor" /> instance.</param>
        /// <param name="serviceProvider"><see cref="IServiceProvider" /> instance.</param>
        public UserFactory(IAccessTokenProviderAccessor accessor,
            IServiceProvider serviceProvider)
            : base(accessor)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Сreates user.
        /// </summary>
        /// <param name="account"><see cref="CustomUserAccount" /> instance.</param>
        /// <param name="options"><see cref="RemoteAuthenticationUserOptions" /> instance.</param>
        /// <returns>New user with claims.</returns>
        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(
            UserAccountModel account,
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

            return initialUser;
        }
    }
}