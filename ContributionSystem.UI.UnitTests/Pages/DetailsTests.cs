using Bunit;
using ContributionSystem.UI.Components;
using ContributionSystem.UI.Pages;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Moq;
using System;
using ContributionSystem.UI.UnitTests.Common;
using Xunit;
using System.Collections.Generic;

namespace ContributionSystem.UI.UnitTests.Pages
{
    public class DetailsTests : PageTestsBaseComponent
    {
        private const int Id = 1;

        [Fact]
        public void WhenPageRendered_ServiceException_ExpectedMarkupRendered()
        {
            ContributionServiceMock.Setup(x => x.GetDetailsById(Id)).ThrowsAsync(new Exception("Service exception"));
            var page = TestContext.RenderComponent<Details>(parameter => parameter.Add(p => p.Id, Id));

            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("Service exception");
        }

        [Fact]
        public void WhenCloseButtonClicked_ValidParameters_Redirect()
        {
            ContributionServiceMock.Setup(x => x.GetDetailsById(Id)).ReturnsAsync(GetDetailsByIdResponse(Id));
            var page = TestContext.RenderComponent<Details>(parameter => parameter.Add(p => p.Id, Id));
            page.Find("#CloseButton").Click();

            Assert.Equal(URLs.History, NavigationManager.Uri);
        }

        [Fact]
        public void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        {
            ContributionServiceMock.Setup(x => x.GetDetailsById(Id)).ReturnsAsync(GetDetailsByIdResponse(Id));
            var page = TestContext.RenderComponent<Details>();

            page.FindComponent<ContributionCalculatorTable<ResponseGetDetailsByIdContributionViewModel, ResponseGetDetailsByIdContributionViewModelItem>>().Should().NotBeNull();
            page.Find("#CloseButton").Should().NotBeNull();
            page.FindAll("div h1").Should().BeEmpty();
        }

        protected ResponseGetDetailsByIdContributionViewModel GetDetailsByIdResponse(int Id)
        {
            return new ResponseGetDetailsByIdContributionViewModel
            {
                ContributionId = Id,
                Items = new List<ResponseGetDetailsByIdContributionViewModelItem>{
                    new ResponseGetDetailsByIdContributionViewModelItem
                    {
                        Id = 1,
                        MonthNumber = 1,
                        Income = 0.08M,
                        Sum = 1.08M
                    }
                }
            };
        }
    }
}
