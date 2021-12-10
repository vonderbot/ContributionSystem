﻿using Bunit;
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
        private const int StatusIndex = 2;

        [Fact]
        public void WhenStatusButtonClicked_OneUser_ChangeUserStatusInvoked()
        {
            ContributionServiceMock.Setup(x => x.GetUsersList()).ReturnsAsync(GetUsersListResponse(NumberOfUsers));
            ContributionServiceMock.Setup(x => x.ChangeUserStatus(GetChangeUserStatusRequest(ValidUserId, !UserStatus))).Returns(Task.FromResult(default(object)));
            var page = TestContext.RenderComponent<UserList>();
            var cells = page.FindAll("tbody tr td");
            cells[StatusIndex].TextContent.Should().BeEquivalentTo("Enabled");
            page.Find("#StatusButton").Click();
            cells = page.FindAll("tbody tr td");

            ContributionServiceMock.Verify(m => m.ChangeUserStatus(It.IsAny<RequestChangeUserStatusContributionViewModel>()), Times.Once());
            cells[StatusIndex].TextContent.Should().BeEquivalentTo("Disabled");
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
            ContributionServiceMock.Setup(x => x.GetUsersList()).ReturnsAsync(new ResponseGetUsersListContributionViewModel());
            var page = TestContext.RenderComponent<UserList>();

            page.Find("thead").Should().NotBeNull();
            page.FindAll("tbody").Should().BeEmpty();
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("Users data is empty");
        }

        [Fact]
        public void WhenPageRendered_OneUser_ExpectedMarkupRendered()
        {
            ContributionServiceMock.Setup(x => x.GetUsersList()).ReturnsAsync(GetUsersListResponse(NumberOfUsers));
            var page = TestContext.RenderComponent<UserList>();

            page.Find("thead").Should().NotBeNull();
            page.FindAll("tbody").Should().NotBeNull();
            page.FindAll("div h1").Should().BeEmpty();

            var rows = page.FindAll("tbody tr");
            var cells = page.FindAll("tbody tr td");
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
                        break;
                }
            }

            page.FindAll("#StatusButton").Count.Should().Be(NumberOfUsers);
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
            var request = new RequestChangeUserStatusContributionViewModel()
            {
                Id = id,
                NewStatus = userStatus
            };

            return request;
        }
    }
}