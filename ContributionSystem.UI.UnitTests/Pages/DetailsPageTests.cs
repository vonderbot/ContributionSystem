using Bunit;
using ContributionSystem.UI.Components;
using ContributionSystem.UI.Pages;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ContributionSystem.UI.UnitTests.Pages
{
    public class DetailsPageTests : PageTestsBaseComponent
    {
        private const int Id = 1;

        [Fact]
        public void WhenCloseButtonClicked_InvalidData_ExceptionMessageRendered()
        {
            var nav = _baseComponent._testContext.Services.GetRequiredService<NavigationManager>();
            _baseComponent._contributionServiceMock.Setup(x => x.GetDetailsById(Id)).ThrowsAsync(new Exception("Service exception"));
            var page = _baseComponent._testContext.RenderComponent<Details>(parameter => parameter.Add(p => p.Id, Id));
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("Service exception");
        }

        [Fact]
        public void WhenCloseButtonClicked_Nodata_Redirect()
        {
            var response = new ResponseGetDetailsByIdContributionViewModel()
            {
                ContributionId = Id,
                Items = new List<ResponseGetDetailsByIdContributionViewModelItem>()
            };
            var nav = _baseComponent._testContext.Services.GetRequiredService<NavigationManager>();
            _baseComponent._contributionServiceMock.Setup(x => x.GetDetailsById(Id)).ReturnsAsync(response);
            var page = _baseComponent._testContext.RenderComponent<Details>(parameter => parameter.Add(p => p.Id, Id));
            page.Find("#CloseButton").Click();
            Assert.Equal("http://localhost/History", nav.Uri);
        }

        [Fact]
        public void WhenPageRendered_NoData_ExpectedMarkupRendered()
        {
            var response = new ResponseGetDetailsByIdContributionViewModel()
            {
                ContributionId = Id,
                Items = null
            };
            _baseComponent._contributionServiceMock.Setup(x => x.GetDetailsById(Id)).ReturnsAsync(response);
            var page = _baseComponent._testContext.RenderComponent<Details>();
            page.FindComponent<ContributionCalculatorTable<ResponseGetDetailsByIdContributionViewModel, ResponseGetDetailsByIdContributionViewModelItem>>().Should().NotBeNull();
            page.Find("#CloseButton").Should().NotBeNull();
            page.FindAll("div h1").Should().BeEmpty();
        }
    }
}
