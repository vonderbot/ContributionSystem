using FluentAssertions;
using NUnit.Framework;
using System;
using ContributionSystem.BusinessLogic.Services;
using System.Collections.Generic;
using System.Security.Claims;
using Moq;
using Microsoft.Graph;
using System.Threading.Tasks;
using ContributionSystem.ViewModels.Models.Contribution;
using System.Threading;

namespace ContributionSystem.BusinesLogic.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private const string ValidUserId = "23";
        private const string InvalidUserId = "-1";
        private const string UserName = "Name";
        private const string UserEmail = "Email";
        private const bool UserStatus = true;

        public UserServiceTests()
        {
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockHttpProvider = new Mock<IHttpProvider>();
            var mockGraphClient = new Mock<GraphServiceClient>(mockAuthProvider.Object, mockHttpProvider.Object);
            var user = new User();
            mockGraphClient.Setup(g => g.Users[ValidUserId]
            .Request()
            .UpdateAsync(It.IsAny<User>(), CancellationToken.None))
                .ReturnsAsync(user);
            mockGraphClient.Setup(g => g.Users[InvalidUserId]
            .Request()
            .UpdateAsync(It.IsAny<User>(), CancellationToken.None))
                .ThrowsAsync(new Exception());
            mockGraphClient.Setup(g => g.Users
            .Request()
            .Select("Id,DisplayName,Mail,AccountEnabled")
            .GetAsync(CancellationToken.None)).Returns(Task.Run(() => GetUserslistGraphRequest())).Verifiable();
            _userService = new UserService(mockGraphClient.Object);
        }

        [Test]
        public async Task GetUsersList_NoParametersPassed_ValidResponce()
        {
            var response = await _userService.GetUsersList();

            response.Should().BeEquivalentTo(GetGetUsersListRequest());
        }

        [Test]
        public async Task ChangeUserStatus_NullRequest_ThrowException()
        {
            Func<Task> act = async () => await _userService.ChangeUserStatus(null);

            await act.Should().ThrowAsync<Exception>().WithMessage("Null request");
        }

        [Test]
        public async Task ChangeUserStatus_NullUserId_ThrowException()
        {
            Func<Task> act = async () => await _userService.ChangeUserStatus(GetChangeUserStatusRequest(null, UserStatus));

            await act.Should().ThrowAsync<Exception>().WithMessage("User id can`t be null or empty");
        }

        [Test]
        public async Task ChangeUserStatus_InvalidUserId_ThrowException()
        {
            Func<Task> act = async () => await _userService.ChangeUserStatus(GetChangeUserStatusRequest(InvalidUserId, UserStatus));

            await act.Should().ThrowAsync<Exception>();
        }

        [Test]
        public async Task ChangeUserStatus_ValidUserId_ValidResponse()
        {
            Func<Task> act = async () => await _userService.ChangeUserStatus(GetChangeUserStatusRequest(ValidUserId, UserStatus));

            await act.Should().NotThrowAsync<Exception>();
        }

        [Test]
        public void GetUserId_UserWithId_ValidResponse()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, ValidUserId),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var response = _userService.GetUserId(new ClaimsPrincipal(identity));

            response.Should().BeEquivalentTo(ValidUserId);
        }

        [Test]
        public void GetUserId_UserWithoutId_ThrowException()
        {
            var claims = new List<Claim>();
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            Func<string> act = () => _userService.GetUserId(new ClaimsPrincipal(identity));

            act.Should().Throw<Exception>().WithMessage("User have no id");
        }
        private RequestChangeUserStatusContributionViewModel GetChangeUserStatusRequest(string id, bool userStatus)
        {
            var correctResponse = new RequestChangeUserStatusContributionViewModel() 
            { 
                Id = id, 
                AccountEnabled = userStatus
            };
            return correctResponse;
        }

        private ResponseGetUsersListContributionViewModel GetGetUsersListRequest()
        {
            var correctResponse = new ResponseGetUsersListContributionViewModel
            { 
                Items = new List<ResponseGetUsersListContributionViewModelItem>
                {
                    new ResponseGetUsersListContributionViewModelItem
                    {
                        Id = ValidUserId,
                        Email = UserEmail,
                        Name = UserName,
                        Status = UserStatus
                    }
                }
            };
            return correctResponse;
        }

        private IGraphServiceUsersCollectionPage GetUserslistGraphRequest()
        {
            var correctResponse = new GraphServiceUsersCollectionPage();
            correctResponse.Add(new User
            {
                Id = ValidUserId,
                DisplayName = UserName,
                Mail = UserEmail,
                AccountEnabled = UserStatus
            });
            return correctResponse;
        }
    }
}
