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
using Microsoft.Graph;
using Microsoft.AspNetCore.Components.Authorization;

namespace ContributionSystem.UI.UnitTests.Common
{
    public abstract class PageTestsBaseComponent
    {
        protected const string UserId = "23";

        protected readonly TestContext TestContext;
        protected readonly Mock<IContributionService> ContributionServiceMock;
        protected readonly NavigationManager NavigationManager;
        protected readonly TestAuthorizationContext testAuthorizationContext;
        protected readonly GraphServiceClient graphServiceClient;

        protected PageTestsBaseComponent()
        {
            TestContext = new TestContext();
            ContributionServiceMock = new Mock<IContributionService>();
            TestContext.Services.AddSingleton(ContributionServiceMock.Object);
            testAuthorizationContext = TestContext.AddTestAuthorization();
            testAuthorizationContext.SetAuthorized("TEST USER");
            testAuthorizationContext.SetClaims(new Claim("oid", UserId));
            TestContext.Services.AddSingleton(testAuthorizationContext);
            TestContext.Services.AddSingleton<SignOutSessionStateManager>();
            TestContext.Services.AddSingleton<GraphServiceClient>();
            TestContext.JSInterop.SetupVoid(
                "sessionStorage.setItem",
                inv => string.Equals(inv.Arguments.FirstOrDefault(), "Microsoft.AspNetCore.Components.WebAssembly.Authentication.SignOutState")
                ).SetVoidResult();
            var d = new Mock<AuthenticationStateProvider>();
            TestContext.Services.AddSingleton(d.Object);
            NavigationManager = TestContext.Services.GetRequiredService<NavigationManager>();
            graphServiceClient = TestContext.Services.GetRequiredService<GraphServiceClient>();
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