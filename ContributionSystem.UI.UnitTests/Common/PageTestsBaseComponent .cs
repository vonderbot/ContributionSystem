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
    public class PageTestsBaseComponent
    {
        protected readonly BaseComponent _baseComponent;

        protected PageTestsBaseComponent()
        {
            _baseComponent = new BaseComponent();
        }

        protected class BaseComponent
        {
            public readonly TestContext _testContext;
            public readonly Mock<IContributionService> _contributionServiceMock;
            public readonly NavigationManager navigationManager;

            public BaseComponent()
            {
                _testContext = new TestContext();
                _contributionServiceMock = new Mock<IContributionService>();
                _testContext.Services.AddSingleton(_contributionServiceMock.Object);
                navigationManager = _testContext.Services.GetRequiredService<NavigationManager>();
            }

            public ResponseCalculateContributionViewModel GetCalculationResponse()
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
}