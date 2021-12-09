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
using ContributionSystem.ViewModels.Enums;
using Xunit;
using System.Text.Json;
using FluentAssertions;
using ContributionSystem.UI.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace ContributionSystem.UI.UnitTests.Services
{
    public class ContributionServiceTests
    {
        private const decimal CorrectSum = 1;
        private const int CorrectTerm = 1;
        private const decimal CorrectPercent = 100;
        private const int Take = 1;
        private const int Skip = 0;
        private const int Id = 1;
        private const string ValidUserId = "23";
        private const string UserName = "Name";
        private const string UserEmail = "Email";
        private const bool UserStatus = true;

        private IContributionService _contributionService;
        private IAccessTokenProvider _tokenProvider;

        public ContributionServiceTests()
        {
            _tokenProvider = new Mock<IAccessTokenProvider>().Object;
        }

        [Fact]
        public async Task ChangeUserStatus_NullRequest_ThrowException()
        {
            _contributionService = new ContributionService(MoqHttpClientSetup(HttpStatusCode.BadRequest, "Server response is incorrect"), _tokenProvider);
            Func<Task> act = async () => await _contributionService.ChangeUserStatus(null);

            await act.Should().ThrowAsync<Exception>().WithMessage("Exception in service: Server response is incorrect");
        }

        [Fact]
        public async Task ChangeUserStatus_ValidRequest_ValidResponse()
        {
            _contributionService = new ContributionService(MoqHttpClientSetup(HttpStatusCode.OK, null), _tokenProvider);
            Func<Task> act = async () => await _contributionService.ChangeUserStatus(GetChangeUserStatusRequest(ValidUserId, UserStatus));

            await act.Should().NotThrowAsync<Exception>();
        }

        [Fact]
        public async Task GetUsersList_InvalidServerResponse_ThrowException()
        {
            _contributionService = new ContributionService(MoqHttpClientSetup(HttpStatusCode.BadRequest, "Server response is incorrect"), _tokenProvider);
            Func<Task> act = async () => await _contributionService.GetUsersList();

            await act.Should().ThrowAsync<Exception>().WithMessage("Exception in service: Server response is incorrect");
        }

        [Fact]
        public async Task GetUsersList_ValidServerResponse_ValidResponse()
        {
            var jsonResponse = JsonSerializer.Serialize(GetUsersListResponse());
            _contributionService = new ContributionService(MoqHttpClientSetup(HttpStatusCode.OK, jsonResponse), _tokenProvider);
            var moqResponse = await _contributionService.GetUsersList();

            moqResponse.Should().BeEquivalentTo(GetUsersListResponse());
        }

        [Fact]
        public async Task GetDetailsById_ValidRequest_ValidResponse()
        {
            var jsonResponse = JsonSerializer.Serialize(GetDetailsByIdResponse());
            _contributionService = new ContributionService(MoqHttpClientSetup(HttpStatusCode.OK, jsonResponse), _tokenProvider);
            var moqResponse = await _contributionService.GetDetailsById(Id);

            moqResponse.Should().BeEquivalentTo(GetDetailsByIdResponse());
        }

        [Fact]
        public async Task GetDetailsById_NullRequest_ThrowException()
        {
            _contributionService = new ContributionService(MoqHttpClientSetup(HttpStatusCode.BadRequest, "Server response is incorrect"), _tokenProvider);
            Func<Task> act = async () => await _contributionService.GetHistoryByUserId(Take, Skip);

            await act.Should().ThrowAsync<Exception>().WithMessage("Exception in service: Server response is incorrect");
        }

        [Fact]
        public async Task GetHistory_ValidRequest_ValidResponse()
        {
            var jsonResponse = JsonSerializer.Serialize(GetHistoryResponse());
            _contributionService = new ContributionService(MoqHttpClientSetup(HttpStatusCode.OK, jsonResponse), _tokenProvider);
            var moqResponse = await _contributionService.GetHistoryByUserId(Take, Skip);

            moqResponse.Should().BeEquivalentTo(GetHistoryResponse());
        }

        [Fact]
        public async Task GetHistory_NullRequest_ThrowException()
        {
            _contributionService = new ContributionService(MoqHttpClientSetup(HttpStatusCode.BadRequest, "Server response is incorrect"), _tokenProvider);
            Func<Task> act = async () => await _contributionService.GetHistoryByUserId(Take, Skip);

            await act.Should().ThrowAsync<Exception>().WithMessage("Exception in service: Server response is incorrect");
        }

        [Fact]
        public async Task Calculate_ValidRequest_ValidResponse()
        {
            var jsonResponse = JsonSerializer.Serialize(GetCalculationResponse());
            _contributionService = new ContributionService(MoqHttpClientSetup(HttpStatusCode.OK, jsonResponse), _tokenProvider);
            var moqResponse = await _contributionService.Сalculate(GetCalculationRequest(CorrectSum, CorrectTerm, CorrectPercent));

            moqResponse.Should().BeEquivalentTo(GetCalculationResponse());
        }

        [Fact]
        public async Task Calculate_NullRequest_ThrowException()
        {
            _contributionService = new ContributionService(MoqHttpClientSetup(HttpStatusCode.BadRequest, "Server response is incorrect"), _tokenProvider);
            Func<Task> act = async () => await _contributionService.Сalculate(null);

            await act.Should().ThrowAsync<Exception>().WithMessage("Exception in service: Server response is incorrect");
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

        private ResponseGetUsersListContributionViewModel GetUsersListResponse()
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

        private ResponseGetHistoryByUserIdContributionViewModel GetHistoryResponse()
        {
            return new ResponseGetHistoryByUserIdContributionViewModel
            {
                TotalNumberOfUserRecords = 1,
                Take = Take,
                Skip = Skip,
                Items = new List<ResponseGetUsersListContributionViewModelItems>{
                    new ResponseGetUsersListContributionViewModelItems{
                        Percent =1,
                        Term =1,
                        Sum =1,
                        Date = "01/11/2021",
                        Id = 1
                    }
                }
            };
        }

        private ResponseGetDetailsByIdContributionViewModel GetDetailsByIdResponse()
        {
            return new ResponseGetDetailsByIdContributionViewModel
            {
                ContributionId = 1,
                Items = new List<ResponseGetDetailsByIdContributionViewModelItem>
                {
                    new ResponseGetDetailsByIdContributionViewModelItem
                    {
                        Id = 1,
                        MonthNumber = 1,
                        Income = 1,
                        Sum = 1
                    }
                }
            };
        }

        private ResponseCalculateContributionViewModel GetCalculationResponse()
        {
            return new ResponseCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,
                Items = new List<MonthsInfoContributionViewModelItem>{
                    new MonthsInfoContributionViewModelItem
                    {
                        MonthNumber = 1,
                        Income = 0.08M,
                        Sum = 1.08M
                    }
                }
            };
        }

        private RequestCalculateContributionViewModel GetCalculationRequest(decimal startValue, int term, decimal percent)
        {
            return new RequestCalculateContributionViewModel()
            {
                CalculationMethod = 0,
                StartValue = startValue,
                Term = term,
                Percent = percent
            };
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