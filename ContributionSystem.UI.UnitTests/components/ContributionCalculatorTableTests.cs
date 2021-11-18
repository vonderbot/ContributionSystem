using Bunit;
using ContributionSystem.UI.Components;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using System.Linq;
using ContributionSystem.UI.UnitTests.Common;
using Xunit;

namespace ContributionSystem.UI.UnitTests.Components
{
    public class ContributionCalculatorTableTests : PageTestsBaseComponent
    {
        [Fact]
        public void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        {
            var page =TestContext.RenderComponent<ContributionCalculatorTable<ResponseCalculateContributionViewModel, MonthsInfoContributionViewModelItem>>();

            page.Find("thead").Should().NotBeNull();
            page.FindAll("tbody").Should().BeEmpty();
        }

        [Fact]
        public void WhenPageRendered_NullResponse_ExpectedMarkupRendered()
        {
            var page = TestContext.RenderComponent<ContributionCalculatorTable<ResponseCalculateContributionViewModel, MonthsInfoContributionViewModelItem>>(parameters => parameters
                .Add(p => p.ResponseCalculateContributionViewModel, null));

            page.Find("thead").Should().NotBeNull();
            page.FindAll("tbody").Should().BeEmpty();
        }

        [Fact]
        public void WhenPageRendered_ValidResponse_ExpectedMarkupRendered()
        {
            var page = TestContext.RenderComponent<ContributionCalculatorTable<ResponseCalculateContributionViewModel, MonthsInfoContributionViewModelItem>>(parameters => parameters
                .Add(p => p.ResponseCalculateContributionViewModel, GetCalculationResponse()));
            var rows = page.FindAll("tbody tr");
            var items = GetCalculationResponse().Items.ToList();
            var cells = page.FindAll("tbody tr td");
            var rowsCounter = 0;

            page.Find("thead").Should().NotBeNull();
            page.Find("tbody").Should().NotBeNull();
            rows.Count.Should().Be(items.Count);

            for (var i = 0; i < cells.Count; i++)
            {
                switch (i % 3)
                {
                    case 0:
                        cells[i].TextContent.Should().BeEquivalentTo(items[rowsCounter].MonthNumber.ToString());
                        break;
                    case 1:
                        cells[i].TextContent.Should().BeEquivalentTo(items[rowsCounter].Income.ToString());
                        break;
                    case 2:
                        cells[i].TextContent.Should().BeEquivalentTo(items[rowsCounter].Sum.ToString());
                        rowsCounter++;
                        break;
                }
            }
        }
    }
}
