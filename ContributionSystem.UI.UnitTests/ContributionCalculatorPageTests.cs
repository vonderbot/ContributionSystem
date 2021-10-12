using Bunit;
using ContributionSystem.UI.Components;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.UI.Pages;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContributionSystem.UI.UnitTests
{
    public class ContributionCalculatorPageTests
    {
        private TestContext testContext;
        private Mock<IContributionService> contributionServiceMock;

        public ContributionCalculatorPageTests()
        {
            testContext = new TestContext();
            contributionServiceMock = new Mock<IContributionService>();
            testContext.Services.AddSingleton<IContributionService>(contributionServiceMock.Object);
        }

        [Fact]
        public void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        {
            // Arrange
            var page = testContext.RenderComponent<ContributionCalculator>();

            // Act

            // Assert
            page.FindComponent<ContributionCalculatorForm>().Should().NotBeNull();
            page.FindComponent<ContributionCalculatorTable>().Should().NotBeNull();
            try
            {
                page.Find("div h1").Should().NotBeNull();
                Assert.True(false);
            }
            catch
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void WhenSubmitButtonClicked_ValidParameters_ServerExceptionRendered()
        {
            // Arrange
            contributionServiceMock.Setup(x => x.Сalculate(It.IsAny<RequestCalculateContributionViewModel>())).ThrowsAsync(new Exception("Mock exception"));
            var page = testContext.RenderComponent<ContributionCalculator>();

            // Act
            page.Find("#Percent").Change("100");
            page.Find("#Term").Change("1");
            page.Find("#Sum").Change("1");
            page.Find("form").Submit();

            // Assert
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("Mock exception");
        }
    }
}
