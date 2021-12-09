using Bunit;
using ContributionSystem.UI.Pages;
using ContributionSystem.UI.UnitTests.Common;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace ContributionSystem.UI.UnitTests.Pages
{
    public class UserListTests : PageTestsBaseComponent
    {
        private const int NumberOfUsers = 1;


        [Fact]
        public void WhenPageRendered_NoParameterPassed_ExpectedMarkupRendered()
        {
            BaseComponentSetup(GetEmptyItemList(NumberOfUsers));
            var page = TestContext.RenderComponent<UserList>();

            page.Find("thead").Should().NotBeNull();
            page.FindAll("tbody").Should().NotBeNull();

            var rows = page.FindAll("tbody tr");
            var cells = page.FindAll("tbody tr td");
            var rowsCounter = 0;
            rows.Count.Should().Be(NumberOfUsers);

            for (var i = 0; i < cells.Count; i++)
            {
                switch (i % 4)
                {
                    case 0:
                        cells[i].TextContent.Should().BeEquivalentTo("Mail not specified");
                        break;
                    case 1:
                        cells[i].TextContent.Should().BeEquivalentTo("Name not specified");
                        break;
                    case 2:
                        cells[i].TextContent.Should().BeEquivalentTo("Disabled");
                        rowsCounter++;
                        break;
                    //case 3:
                    //    cells[i].TextContent.Should().BeEquivalentTo(items[rowsCounter].Income.ToString());
                    //    break;
                }
            }
        }

        private void BaseComponentSetup(ResponseGetUsersListContributionViewModel response)
        {
            ContributionServiceMock.Setup(x => x.GetUsersList()).ReturnsAsync(response);
        }

        private ResponseGetUsersListContributionViewModel GetEmptyItemList(int NumberOfUsers)
        {
            var itemList = new List<ResponseGetUsersListContributionViewModelItem>();

            for (var i = 0; i < NumberOfUsers; i++)
            {
                itemList.Add(new ResponseGetUsersListContributionViewModelItem());
            }

            var response = new ResponseGetUsersListContributionViewModel
            {
                Items = itemList
            };

            return response;
        }
    }
}
