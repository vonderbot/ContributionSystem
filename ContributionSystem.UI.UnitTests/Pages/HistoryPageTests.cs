using Bunit;
using ContributionSystem.UI.Pages;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ContributionSystem.UI.UnitTests.Pages
{
    public class HistoryPageTests : PageTestsBaseComponent
    {
        private const int Take = 8;
        private const int Skip = 0;

        [Fact]
        public void WhenButtonSeeDetailsClicked_DataOnlyForOneLoad_Redirect()
        {
            var itemList = new List<ResponseGetHistoryContributionViewModelItem>();
            for (int i = 0; i < Take; i++)
            {
                itemList.Add(new ResponseGetHistoryContributionViewModelItem());
            }
            var response = new ResponseGetHistoryContributionViewModel
            {
                Items = itemList,
                Skip = Skip,
                Take = Take,
                TotalNumberOfRecords = Take
            };
            var nav = _baseComponent._testContext.Services.GetRequiredService<NavigationManager>();
            _baseComponent._contributionServiceMock.Setup(x => x.GetHistory(Take, Skip)).ReturnsAsync(response);
            var page = _baseComponent._testContext.RenderComponent<History>();
            page.Find("#SeeDetails").Click();
            Assert.Equal("http://localhost/Details/0", nav.Uri);
        }

        [Fact]
        public void WhenButtonLoadMoreClicked_DataForTwoLoads_ExpectedMarkupRendered()
        {
            var itemList = new List<ResponseGetHistoryContributionViewModelItem>();
            for (int i = 0; i < Take; i++)
            {
                itemList.Add(new ResponseGetHistoryContributionViewModelItem());
            }
            var response = new ResponseGetHistoryContributionViewModel
            {
                Items = itemList,
                Skip = Skip,
                Take = Take,
                TotalNumberOfRecords = Take * 2
            };
            _baseComponent._contributionServiceMock.Setup(x => x.GetHistory(Take, Skip)).ReturnsAsync(response);
            var page = _baseComponent._testContext.RenderComponent<History>();
            response.Skip = Take + Skip;
            _baseComponent._contributionServiceMock.Setup(x => x.GetHistory(Take, Take + Skip)).ReturnsAsync(response);
            page.Find("#LoadMore").Click();
            page.FindAll("#DataContainer").Should().HaveCount(Take* 2);
            page.FindAll("#LoadMore").Should().BeEmpty();
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("End of history");
        }

        [Fact]
        public void WhenPageRendered_DataForTwoLoads_ExpectedMarkupRendered()
        {
            var itemList = new List<ResponseGetHistoryContributionViewModelItem>();
            for (int i = 0; i < Take; i++)
            {
                itemList.Add(new ResponseGetHistoryContributionViewModelItem());
            }
            var response = new ResponseGetHistoryContributionViewModel
            {
                Items = itemList,
                Skip = Skip,
                Take = Take,
                TotalNumberOfRecords = Take * 2
            };
            _baseComponent._contributionServiceMock.Setup(x => x.GetHistory(Take, Skip)).ReturnsAsync(response);
            var page = _baseComponent._testContext.RenderComponent<History>();
            page.FindAll("#DataContainer").Should().HaveCount(Take);
            page.FindAll("#LoadMore").Should().NotBeEmpty();
            page.FindAll("div h1").Should().BeEmpty();
        }

        [Fact]
        public void WhenPageRendered_DataOnlyForOneLoad_ExpectedMarkupRendered()
        {
            var itemList = new List<ResponseGetHistoryContributionViewModelItem>();
            for (int i = 0; i < Take; i++)
            {
                itemList.Add(new ResponseGetHistoryContributionViewModelItem());
            }
            var response = new ResponseGetHistoryContributionViewModel
            {
                Items = itemList,
                Skip = Skip,
                Take = Take,
                TotalNumberOfRecords = Take
            };
            _baseComponent._contributionServiceMock.Setup(x => x.GetHistory(Take, Skip)).ReturnsAsync(response);
            var page = _baseComponent._testContext.RenderComponent<History>();
            page.FindAll("#DataContainer").Should().HaveCount(response.TotalNumberOfRecords);
            page.FindAll("#LoadMore").Should().BeEmpty();
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("End of history");
        }

        [Fact]
        public void WhenPageRendered_NoData_ExpectedMarkupRendered()
        {
            var response = new ResponseGetHistoryContributionViewModel
            {
                Items = new List<ResponseGetHistoryContributionViewModelItem>(),
                Skip = Skip,
                Take = Take,
                TotalNumberOfRecords = 0
            };
            _baseComponent._contributionServiceMock.Setup(x => x.GetHistory(Take, Skip)).ReturnsAsync(response);
            var page = _baseComponent._testContext.RenderComponent<History>();
            page.FindAll("#DataContainer").Should().BeEmpty();
            page.FindAll("#LoadMore").Should().BeEmpty();
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("History is empty");
        }
    }
}
