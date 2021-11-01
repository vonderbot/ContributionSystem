using Bunit;
using ContributionSystem.UI.Components;
using ContributionSystem.UI.Pages;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace ContributionSystem.UI.UnitTests.Pages
{
    public class DetailsPageTests : PageTestsBaseComponent
    {
        private const int Id = 1;

        public DetailsPageTests()
        {
            ResponseGetDetailsByIdContributionViewModel response = null;
            _baseComponent._contributionServiceMock.Setup(x => x.GetDetailsById(Id)).ReturnsAsync(response);
        }

        [Fact]
        public void WhenPageRendered_ValidParameters_ServiceExceptionMessageRendered()
        {
            _baseComponent._contributionServiceMock.Setup(x => x.GetDetailsById(Id)).ThrowsAsync(new Exception("Service exception"));
            var page = _baseComponent._testContext.RenderComponent<Details>(parameter => parameter.Add(p => p.Id, Id));
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("Service exception");
        }

        [Fact]
        public void WhenCloseButtonClicked_ValidParameters_Redirect()
        {
            var page = _baseComponent._testContext.RenderComponent<Details>(parameter => parameter.Add(p => p.Id, Id));
            page.Find("#CloseButton").Click();
            Assert.Equal("http://localhost/History", _baseComponent.navigationManager.Uri);
        }

        [Fact]
        public void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        {
            var page = _baseComponent._testContext.RenderComponent<Details>();
            page.FindComponent<ContributionCalculatorTable<ResponseGetDetailsByIdContributionViewModel, ResponseGetDetailsByIdContributionViewModelItem>>().Should().NotBeNull();
            page.Find("#CloseButton").Should().NotBeNull();
            page.FindAll("div h1").Should().BeEmpty();
        }
    }
}
