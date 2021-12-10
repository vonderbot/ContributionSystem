using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using System.Net.Http;
using System.Net;
using Moq.Protected;
using System.Threading;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Xunit;
using System.Text.Json;
using FluentAssertions;
using ContributionSystem.UI.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace ContributionSystem.UI.UnitTests.Services
{
    public class UserServiceTests
    {
        private const string ValidUserId = "23";
        private const string UserName = "Name";
        private const string UserEmail = "Email";
        private const bool UserStatus = true;

        private IUserService _contributionService;
        private IAccessTokenProvider _tokenProvider;

        public UserServiceTests()
        {
            _tokenProvider = new Mock<IAccessTokenProvider>().Object;
        }

        [Fact]
        public async Task ChangeUserStatus_NullRequest_ThrowException()
        {
            _contributionService = new UserService(MoqHttpClientSetup(HttpStatusCode.BadRequest, "Server response is incorrect"), _tokenProvider);
            Func<Task> act = async () => await _contributionService.ChangeUserStatus(null);

            await act.Should().ThrowAsync<Exception>().WithMessage("Exception in service: Server response is incorrect");
        }

        [Fact]
        public async Task ChangeUserStatus_ValidRequest_ValidResponse()
        {
            _contributionService = new UserService(MoqHttpClientSetup(HttpStatusCode.OK, null), _tokenProvider);
            Func<Task> act = async () => await _contributionService.ChangeUserStatus(GetChangeUserStatusRequest(ValidUserId, UserStatus));

            await act.Should().NotThrowAsync<Exception>();
        }

        [Fact]
        public async Task GetUsersList_InvalidServerResponse_ThrowException()
        {
            _contributionService = new UserService(MoqHttpClientSetup(HttpStatusCode.BadRequest, "Server response is incorrect"), _tokenProvider);
            Func<Task> act = async () => await _contributionService.GetUsersList();

            await act.Should().ThrowAsync<Exception>().WithMessage("Exception in service: Server response is incorrect");
        }

        [Fact]
        public async Task GetUsersList_ValidServerResponse_ValidResponse()
        {
            var jsonResponse = JsonSerializer.Serialize(GetUsersListResponse());
            _contributionService = new UserService(MoqHttpClientSetup(HttpStatusCode.OK, jsonResponse), _tokenProvider);
            var moqResponse = await _contributionService.GetUsersList();

            moqResponse.Should().BeEquivalentTo(GetUsersListResponse());
        }

        private RequestChangeUserStatusUserViewModel GetChangeUserStatusRequest(string id, bool userStatus)
        {
            var correctResponse = new RequestChangeUserStatusUserViewModel()
            {
                Id = id,
                AccountEnabled = userStatus
            };
            return correctResponse;
        }

        private ResponseGetUsersListUserViewModel GetUsersListResponse()
        {
            var correctResponse = new ResponseGetUsersListUserViewModel
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

        private HttpClient MoqHttpClientSetup(HttpStatusCode statusCode, string content)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = statusCode
            };
            response.Content = content == null ? new StringContent(String.Empty) : new StringContent(content);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://localhost:44303/api/")
            };

            return httpClient;
        }
    }
}
