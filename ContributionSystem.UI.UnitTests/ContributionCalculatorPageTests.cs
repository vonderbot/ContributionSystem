using Bunit;
using ContributionSystem.UI.Pages;
using System;
using Xunit;

namespace ContributionSystem.UI.UnitTests
{
    public class ContributionCalculatorPageTests
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<ContributionCalculator>();

            // Act
            cut.Find("button").Click();

            // Assert
            cut.Find("p").MarkupMatches("<p>Current count: 1</p>");
        }
    }
}
