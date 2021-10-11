using Bunit;
using ContributionSystem.UI.Components;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using Xunit;

namespace ContributionSystem.UI.UnitTests
{
    public class ContributionCalculatorTableTests
    {
        private TestContext testContext;

        public ContributionCalculatorTableTests()
        {
            testContext = new TestContext();
        }

        [Fact]
        public void ContributionCalculatorTableTest()
        {
            // Arrange
            var model = new ResponseCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,

                Items = new ResponseCalculateContributionViewModelItem[1]
                {
                    new ResponseCalculateContributionViewModelItem
                    {
                        MonthNumber = 1,
                        Income = 0.08M,
                        Sum = 1.08M
                    }
                }
            };
            var page = testContext.RenderComponent<ContributionCalculatorTable>((nameof(ContributionCalculatorTable.ResponseCalculateContributionViewModel), model));

            // Act
            var expected = @"<tbody><tr><td scope = ""row"">1</td><td>0.08</td><td>1.08</td></tr></ tbody>";

            // Assert
            page.Find("tbody").MarkupMatches(expected);
        }
    }
}
