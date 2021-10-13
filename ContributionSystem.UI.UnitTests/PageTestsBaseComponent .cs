using Bunit;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContributionSystem.UI.UnitTests
{
    public abstract class PageTestsBaseComponent
    {
        protected readonly TestContext _testContext;
        protected readonly Mock<IContributionService> _contributionServiceMock;

        public PageTestsBaseComponent()
        {
            _testContext = new TestContext();
            _contributionServiceMock = new Mock<IContributionService>();
            _testContext.Services.AddSingleton<IContributionService>(_contributionServiceMock.Object);
        }

        protected static ResponseCalculateContributionViewModel GetCalculationResponse()
        {
            return new ResponseCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,
                Items = new List<ResponseCalculateContributionViewModelItem>{
                    new ResponseCalculateContributionViewModelItem
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
