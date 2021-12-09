using Bunit;
using ContributionSystem.UI.Pages;
using ContributionSystem.UI.UnitTests.Common;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ContributionSystem.UI.UnitTests.Pages
{
    public class UserListTests : PageTestsBaseComponent
    {

        private const int NumberOfUsers = 1;
        private const string ValidUserId = "23";
        private const string UserName = "Name";
        private const string UserEmail = "Email";
        private const bool UserStatus = true;

        [Fact]
        public void WhenStatusButtonClicked_NoParametersPassed_ExpectedMarkupRendered()
        {
            BaseComponentSetup(GetUsersListResponse(NumberOfUsers));
            var page = TestContext.RenderComponent<UserList>();
            page.Find("#StatusButton").Click();
        }

        [Fact]
        public void WhenPageRendered_UserWithoutpermissions_ExpectedMarkupRendered()
        {
            TestAuthorizationContext.SetRoles(new string[] { "User" });
            var page = TestContext.RenderComponent<UserList>();

            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("You do not have permissions to view this page.");
        }

        [Fact]
        public void WhenPageRendered_ServiceException_ExpectedMarkupRendered()
        {
            ContributionServiceMock.Setup(x => x.GetUsersList()).ThrowsAsync(new Exception("Service exception"));
            var page = TestContext.RenderComponent<UserList>();

            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("Service exception");
        }

        [Fact]
        public void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        {
            BaseComponentSetup(new ResponseGetUsersListContributionViewModel());
            var page = TestContext.RenderComponent<UserList>();

            page.Find("thead").Should().NotBeNull();
            page.FindAll("tbody").Should().BeEmpty();
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("Users data is empty");
        }

        [Fact]
        public void WhenPageRendered_OneUser_ExpectedMarkupRendered()
        {
            BaseComponentSetup(GetUsersListResponse(NumberOfUsers));
            var page = TestContext.RenderComponent<UserList>();

            page.Find("thead").Should().NotBeNull();
            page.FindAll("tbody").Should().NotBeNull();
            page.FindAll("div h1").Should().BeEmpty();

            var rows = page.FindAll("tbody tr");
            var cells = page.FindAll("tbody tr td");
            var rowsCounter = 0;
            rows.Count.Should().Be(NumberOfUsers);

            for (var i = 0; i < cells.Count; i++)
            {
                switch (i % 4)
                {
                    case 0:
                        cells[i].TextContent.Should().BeEquivalentTo(UserEmail);
                        break;
                    case 1:
                        cells[i].TextContent.Should().BeEquivalentTo(UserName);
                        break;
                    case 2:
                        cells[i].TextContent.Should().BeEquivalentTo("Enabled");
                        rowsCounter++;
                        break;
                }
            }

            page.FindAll("#StatusButton").Count.Should().Be(NumberOfUsers);
        }

        private void BaseComponentSetup(ResponseGetUsersListContributionViewModel response)
        {
            ContributionServiceMock.Setup(x => x.GetUsersList()).ReturnsAsync(response);
            ContributionServiceMock.Setup(x => x.ChangeUserStatus(GetChangeUserStatusRequest(ValidUserId, !UserStatus))).Returns(Task.FromResult(default(object)));
        }

        private ResponseGetUsersListContributionViewModel GetUsersListResponse(int NumberOfUsers)
        {
            var itemList = new List<ResponseGetUsersListContributionViewModelItem>();

            for (var i = 0; i < NumberOfUsers; i++)
            {
                itemList.Add(new ResponseGetUsersListContributionViewModelItem()
                {
                    Id = ValidUserId,
                    Email = UserEmail,
                    Name = UserName,
                    Status = UserStatus
                });
            }

            var response = new ResponseGetUsersListContributionViewModel
            {
                Items = itemList
            };

            return response;
        }

        private RequestChangeUserStatusContributionViewModel GetChangeUserStatusRequest(string id, bool userStatus)
        {
            var correctResponse = new RequestChangeUserStatusContributionViewModel()
            {
                Id = id,
                NewStatus = userStatus
            };
            return correctResponse;
        }
    }
}
