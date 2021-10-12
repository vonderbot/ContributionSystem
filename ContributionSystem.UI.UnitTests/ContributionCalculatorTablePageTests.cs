using Bunit;
using ContributionSystem.UI.Components;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ContributionSystem.UI.UnitTests
{
    public class ContributionCalculatorTablePageTests
    {
        private readonly TestContext _testContext;
        private readonly Mock<IContributionService> _contributionServiceMock;

        public ContributionCalculatorTablePageTests()
        {
            _testContext = new TestContext();
            _contributionServiceMock = new Mock<IContributionService>();
            _testContext.Services.AddSingleton<IContributionService>(_contributionServiceMock.Object);
        }

        [Fact]
        public void WhenPageRendered_NullResponse_ExpectedMarkupRendered()
        {
            var page = _testContext.RenderComponent<ContributionCalculatorTable>(parameters => parameters
                .Add(p => p.ResponseCalculateContributionViewModel, null));
            page.Find("thead").Should().NotBeNull();
            page.FindAll("tbody").Should().BeEmpty();
        }

        [Fact]
        public void WhenPageRendered_ValidResponse_ExpectedMarkupRendered()
        {
            var page = _testContext.RenderComponent<ContributionCalculatorTable>(parameters => parameters
                .Add(p => p.ResponseCalculateContributionViewModel, GetCalculationResponse()));
            page.Find("thead").Should().NotBeNull();
            page.Find("tbody").Should().NotBeNull();
            var rows = page.FindAll("tbody tr");
            rows.Count.Should().Be(GetCalculationResponse().Items.Count);
            var cells = page.FindAll("tbody tr td");
            var rowsCounter = 0;

            for(var i = 0; i < cells.Count; i++)
            {
                switch (i % 3)
                {
                    case 0:
                        cells[i].TextContent.Should().BeEquivalentTo(GetCalculationResponse().Items[rowsCounter].MonthNumber.ToString());
                        break;
                    case 1:
                        cells[i].TextContent.Should().BeEquivalentTo(GetCalculationResponse().Items[rowsCounter].Income.ToString());
                        break;
                    case 2:
                        cells[i].TextContent.Should().BeEquivalentTo(GetCalculationResponse().Items[rowsCounter].Sum.ToString());
                        rowsCounter++;
                        break;
                }
            }
        }

        [Fact]
        public void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        {
            var page = _testContext.RenderComponent<ContributionCalculatorTable>();
            page.Find("thead").Should().NotBeNull();
            page.FindAll("tbody").Should().BeEmpty();
        }

        private static ResponseCalculateContributionViewModel GetCalculationResponse()
        {
            return new ResponseCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,
                Items = new List<ResponseCalculateContributionViewModelItem>
                {
                    new ResponseCalculateContributionViewModelItem
                    {
                        MonthNumber = 1,
                        Income = 0.08M,
                        Sum = 1.08M
                    },
                    new ResponseCalculateContributionViewModelItem
                    {
                        MonthNumber = 2,
                        Income = 0.08M,
                        Sum = 1.16M
                    }
                }
            };
        }
    }
}
