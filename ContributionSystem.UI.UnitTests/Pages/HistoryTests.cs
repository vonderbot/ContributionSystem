using Bunit;
using ContributionSystem.UI.Pages;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using ContributionSystem.UI.UnitTests.Common;
using Xunit;
using System;

namespace ContributionSystem.UI.UnitTests.Pages
{
    public class HistoryTests : PageTestsBaseComponent
    {
        private const int Take = 8;
        private const int Skip = 0;

        [Fact]
        public void WhenPageRendered_ServiceException_ExpectedMarkupRendered()
        {
            _contributionServiceMock.Setup(x => x.GetHistory(Take, Skip)).ThrowsAsync(new Exception("Service exception"));
            var page = TestContext.RenderComponent<History>();

            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("Service exception");
        }

        [Fact]
        public void WhenButtonSeeDetailsClicked_DataForOneLoad_Redirect()
        {
            BaseComponentSetup(GetEmptyItemList(Take), Take, Skip, Take);
            var page = TestContext.RenderComponent<History>();
            page.Find("#SeeDetails").Click();

            Assert.Equal("http://localhost/Details/0", navigationManager.Uri);
        }

        [Fact]
        public void WhenButtonLoadMoreClicked_DataForTwoLoads_ExpectedMarkupRendered()
        {
            BaseComponentSetup(GetEmptyItemList(Take), Take, Skip, Take * 2);
            BaseComponentSetup(GetEmptyItemList(Take), Take, Take + Skip, Take * 2);
            var page = TestContext.RenderComponent<History>();
            page.Find("#LoadMore").Click();
            CheckDataContainers(page, Take * 2);

            page.FindAll("#LoadMore").Should().BeEmpty();
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("End of history");
        }

        [Fact]
        public void WhenPageRendered_DataForTwoLoads_ExpectedMarkupRendered()
        {
            BaseComponentSetup(GetEmptyItemList(Take), Take, Skip, Take * 2);
            var page = TestContext.RenderComponent<History>();
            CheckDataContainers(page, Take);

            page.FindAll("#LoadMore").Should().NotBeEmpty();
            page.FindAll("div h1").Should().BeEmpty();
        }

        [Fact]
        public void WhenPageRendered_DataForOneLoad_ExpectedMarkupRendered()
        {
            BaseComponentSetup(GetEmptyItemList(Take), Take, Skip, Take);
            var page = TestContext.RenderComponent<History>();
            CheckDataContainers(page, Take);

            page.FindAll("#LoadMore").Should().BeEmpty();
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("End of history");
        }

        [Fact]
        public void WhenPageRendered_EmptyDataBase_ExpectedMarkupRendered()
        {
            BaseComponentSetup(new List<ResponseGetHistoryContributionViewModelItem>(), Take, Skip, 0);
            var page = TestContext.RenderComponent<History>();
            CheckDataContainers(page, 0);

            page.FindAll("#LoadMore").Should().BeEmpty();
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("History is empty");
        }

        private void CheckDataContainers(IRenderedComponent<History> page, int count)
        {
            page.FindAll("#DataContainer").Count.Should().Be(count);
            page.FindAll("#Percent").Count.Should().Be(count);
            page.FindAll("#Term").Count.Should().Be(count);
            page.FindAll("#Sum").Count.Should().Be(count);
            page.FindAll("#Date").Count.Should().Be(count);
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
            _contributionServiceMock.Setup(x => x.GetHistory(take, skip)).ReturnsAsync(response);
        }

        private List<ResponseGetHistoryContributionViewModelItem> GetEmptyItemList(int numberOfItems)
        {
            var itemList = new List<ResponseGetHistoryContributionViewModelItem>();

            for (var i = 0; i < numberOfItems; i++)
            {
                itemList.Add(new ResponseGetHistoryContributionViewModelItem());
            }

            return itemList;
        }
    }
}