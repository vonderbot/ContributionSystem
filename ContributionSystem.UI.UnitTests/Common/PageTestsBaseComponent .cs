using Bunit;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Bunit.TestDoubles;
using System.Security.Claims;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace ContributionSystem.UI.UnitTests.Common
{
    public abstract class PageTestsBaseComponent
    {
        protected const string UserId = "23";

        protected readonly TestContext TestContext;
        protected readonly Mock<IContributionService> ContributionServiceMock;
        protected readonly Mock<IUserService> UserServiceMock;
        protected readonly NavigationManager NavigationManager;
        protected TestAuthorizationContext TestAuthorizationContext;

        protected PageTestsBaseComponent()
        {
            TestContext = new TestContext();
            ContributionServiceMock = new Mock<IContributionService>();
            TestContext.Services.AddSingleton(ContributionServiceMock.Object);
            UserServiceMock = new Mock<IUserService>();
            TestContext.Services.AddSingleton(UserServiceMock.Object);
            TestAuthorizationContext = TestContext.AddTestAuthorization();
            TestAuthorizationContext.SetAuthorized("TEST USER");
            TestAuthorizationContext.SetClaims(new Claim("oid", UserId));
            TestAuthorizationContext.SetRoles(new string[] { "UserAdmin" });
            TestContext.Services.AddSingleton(TestAuthorizationContext);
            TestContext.Services.AddSingleton<SignOutSessionStateManager>();
            TestContext.JSInterop.SetupVoid(
                "sessionStorage.setItem",
                inv => string.Equals(inv.Arguments.FirstOrDefault(), "Microsoft.AspNetCore.Components.WebAssembly.Authentication.SignOutState")
                ).SetVoidResult();
            var inMemorySettings = new Dictionary<string, string> {
                {"Take", "8"},
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            TestContext.Services.AddSingleton(configuration);
            NavigationManager = TestContext.Services.GetRequiredService<NavigationManager>();
        }

        protected ResponseCalculateContributionViewModel GetCalculationResponse()
        {
            return new ResponseCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,
                Items = new List<MonthsInfoContributionViewModelItem>{
                    new MonthsInfoContributionViewModelItem
                    {
                        MonthNumber = 1,
                        Income = 0.08M,
                        Sum = 1.08M
                    }
                }
            };
        }
    }
}