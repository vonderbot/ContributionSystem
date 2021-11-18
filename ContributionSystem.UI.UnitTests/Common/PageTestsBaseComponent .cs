using Bunit;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace ContributionSystem.UI.UnitTests.Common
{
    public abstract class PageTestsBaseComponent
    {
        protected readonly TestContext TestContext;
        protected readonly Mock<IContributionService> ContributionServiceMock;
        protected readonly NavigationManager NavigationManager;

        protected PageTestsBaseComponent()
        {
            TestContext = new TestContext();
            ContributionServiceMock = new Mock<IContributionService>();
            TestContext.Services.AddSingleton(ContributionServiceMock.Object);
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
