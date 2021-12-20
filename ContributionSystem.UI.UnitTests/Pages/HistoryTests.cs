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
            ContributionServiceMock.Setup(x => x.GetHistoryByUserId(Take, Skip)).ThrowsAsync(new Exception("Service exception"));
            var page = TestContext.RenderComponent<History>();

            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("Service exception");
        }

        [Fact]
        public void WhenButtonSeeDetailsClicked_DataForOneLoad_Redirect()
        {
            BaseComponentSetup(GetEmptyItemList(Take), Take, Skip, Take, UserId);
            var page = TestContext.RenderComponent<History>();
            page.Find("#SeeDetails").Click();

            Assert.Equal(URLs.Details, NavigationManager.Uri);
        }

        [Fact]
        public void WhenButtonLoadMoreClicked_DataForTwoLoads_ExpectedMarkupRendered()
        {
            BaseComponentSetup(GetEmptyItemList(Take), Take, Skip, Take * 2, UserId);
            BaseComponentSetup(GetEmptyItemList(Take), Take, Take + Skip, Take * 2, UserId);
            var page = TestContext.RenderComponent<History>();
            page.Find("#LoadMore").Click();
            CheckDataContainers(page, Take * 2);

            page.FindAll("#LoadMore").Should().BeEmpty();
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("End of history");
        }

        [Fact]
        public void WhenPageRendered_DataForTwoLoads_ExpectedMarkupRendered()
        {
            BaseComponentSetup(GetEmptyItemList(Take), Take, Skip, Take * 2, UserId);
            var page = TestContext.RenderComponent<History>();
            CheckDataContainers(page, Take);

            page.FindAll("#LoadMore").Should().NotBeEmpty();
            page.FindAll("div h1").Should().BeEmpty();
        }

        [Fact]
        public void WhenPageRendered_DataForOneLoad_ExpectedMarkupRendered()
        {
            BaseComponentSetup(GetEmptyItemList(Take), Take, Skip, Take, UserId);
            var page = TestContext.RenderComponent<History>();
            CheckDataContainers(page, Take);

            page.FindAll("#LoadMore").Should().BeEmpty();
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("End of history");
        }

        [Fact]
        public void WhenPageRendered_EmptyDataBase_ExpectedMarkupRendered()
        {
            BaseComponentSetup(new List<ResponseGetUsersListContributionViewModelItems>(), Take, Skip, 0, UserId);
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

        private void BaseComponentSetup(List<ResponseGetUsersListContributionViewModelItems> items, int take, int skip, int totalNumberOfRecords, string userId)
        {
            var response = new ResponseGetHistoryByUserIdContributionViewModel
            {
                UserId = userId,
                Items = items,
                Take = take,
                Skip = skip,
                TotalNumberOfUserRecords = totalNumberOfRecords
            };
            ContributionServiceMock.Setup(x => x.GetHistoryByUserId(take, skip)).ReturnsAsync(response);
        }

        private List<ResponseGetUsersListContributionViewModelItems> GetEmptyItemList(int numberOfItems)
        {
            var itemList = new List<ResponseGetUsersListContributionViewModelItems>();

            for (var i = 0; i < numberOfItems; i++)
            {
                itemList.Add(new ResponseGetUsersListContributionViewModelItems());
            }

            return itemList;
        }
    }
}