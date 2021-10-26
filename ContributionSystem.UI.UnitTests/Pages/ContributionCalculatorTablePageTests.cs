using Bunit;
using ContributionSystem.UI.Components;
using FluentAssertions;
using Xunit;

namespace ContributionSystem.UI.UnitTests.Pages
{
    public class ContributionCalculatorTablePageTests : PageTestsBaseComponent
    {

        [Fact]
        public void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        {
            var page = _baseComponent._testContext.RenderComponent<ContributionCalculatorTable>();
            page.Find("thead").Should().NotBeNull();
            page.FindAll("tbody").Should().BeEmpty();
        }

        [Fact]
        public void WhenPageRendered_NullResponse_ExpectedMarkupRendered()
        {
            var page = _baseComponent._testContext.RenderComponent<ContributionCalculatorTable>(parameters => parameters
                .Add(p => p.ResponseCalculateContributionViewModel, null));
            page.Find("thead").Should().NotBeNull();
            page.FindAll("tbody").Should().BeEmpty();
        }

        //[Fact]
        //public void WhenPageRendered_ValidResponse_ExpectedMarkupRendered()
        //{
        //    var page = _baseComponent._testContext.RenderComponent<ContributionCalculatorTable>(parameters => parameters
        //        .Add(p => p.ResponseCalculateContributionViewModel, _baseComponent.GetCalculationResponse()));
        //    page.Find("thead").Should().NotBeNull();
        //    page.Find("tbody").Should().NotBeNull();
        //    var rows = page.FindAll("tbody tr");
        //    rows.Count.Should().Be(_baseComponent.GetCalculationResponse().Items.Count);
        //    var cells = page.FindAll("tbody tr td");
        //    var rowsCounter = 0;

        //    for (var i = 0; i < cells.Count; i++)
        //    {
        //        switch (i % 3)
        //        {
        //            case 0:
        //                cells[i].TextContent.Should().BeEquivalentTo(_baseComponent.GetCalculationResponse().Items[rowsCounter].MonthNumber.ToString());
        //                break;
        //            case 1:
        //                cells[i].TextContent.Should().BeEquivalentTo(_baseComponent.GetCalculationResponse().Items[rowsCounter].Income.ToString());
        //                break;
        //            case 2:
        //                cells[i].TextContent.Should().BeEquivalentTo(_baseComponent.GetCalculationResponse().Items[rowsCounter].Sum.ToString());
        //                rowsCounter++;
        //                break;
        //        }
        //    }
        //}
    }
}
