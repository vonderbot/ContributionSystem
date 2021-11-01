using Bunit;
using ContributionSystem.UI.Pages;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
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
        public void WhenButtonSeeDetailsClicked_DataForOneLoad_Redirect()
        {
            BaseComponentSetup(GetEmptyItemList(Take), Take, Skip, Take);
            var page = _baseComponent._testContext.RenderComponent<History>();
            page.Find("#SeeDetails").Click();
            Assert.Equal("http://localhost/Details/0", _baseComponent.navigationManager.Uri);
        }

        [Fact]
        public void WhenButtonLoadMoreClicked_DataForTwoLoads_ExpectedMarkupRendered()
        {
            BaseComponentSetup(GetEmptyItemList(Take), Take, Skip, Take * 2);
            BaseComponentSetup(GetEmptyItemList(Take), Take, Take + Skip, Take * 2);
            var page = _baseComponent._testContext.RenderComponent<History>();
            page.Find("#LoadMore").Click();
            page.FindAll("#DataContainer").Should().HaveCount(Take * 2);
            page.FindAll("#LoadMore").Should().BeEmpty();
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("End of history");
        }

        [Fact]
        public void WhenPageRendered_DataForTwoLoads_ExpectedMarkupRendered()
        {
            BaseComponentSetup(GetEmptyItemList(Take), Take, Skip, Take * 2);
            var page = _baseComponent._testContext.RenderComponent<History>();
            page.FindAll("#DataContainer").Should().HaveCount(Take);
            page.FindAll("#LoadMore").Should().NotBeEmpty();
            page.FindAll("div h1").Should().BeEmpty();
        }

        [Fact]
        public void WhenPageRendered_DataForOneLoad_ExpectedMarkupRendered()
        {
            BaseComponentSetup(GetEmptyItemList(Take), Take, Skip, Take);
            var page = _baseComponent._testContext.RenderComponent<History>();
            page.FindAll("#DataContainer").Should().HaveCount(Take);
            page.FindAll("#LoadMore").Should().BeEmpty();
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("End of history");
        }

        [Fact]
        public void WhenPageRendered_EmptyDataBase_ExpectedMarkupRendered()
        {
            BaseComponentSetup(new List<ResponseGetHistoryContributionViewModelItem>(), Take, Skip, 0);
            var page = _baseComponent._testContext.RenderComponent<History>();
            page.FindAll("#DataContainer").Should().BeEmpty();
            page.FindAll("#LoadMore").Should().BeEmpty();
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("History is empty");
        }

        private void BaseComponentSetup(List<ResponseGetHistoryContributionViewModelItem> items, int take, int skip, int totalNumberOfRecords)
        {
            var response = new ResponseGetHistoryContributionViewModel
            {
                Items = items,
                Take = take,
                Skip = skip,
                TotalNumberOfRecords = totalNumberOfRecords
            };
            _baseComponent._contributionServiceMock.Setup(x => x.GetHistory(take, skip)).ReturnsAsync(response);
        }

        private List<ResponseGetHistoryContributionViewModelItem> GetEmptyItemList(int numberOfItems)
        {
            var itemList = new List<ResponseGetHistoryContributionViewModelItem>();

            for (int i = 0; i < numberOfItems; i++)
            {
                itemList.Add(new ResponseGetHistoryContributionViewModelItem());
            }

            return itemList;
        }
    }
}