using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ContributionSystem.UI.Services;
using System.Net.Http;
using System.Net;
using Moq.Protected;
using System.Threading;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.ViewModels.Enums;
using FluentAssertions;

namespace ContributionSystem.UI.UnitTests.Services
{
    public class ContributionServiceTests
    {
        private const decimal CorrectSum = 1;
        private const int CorrectTerm = 1;
        private const decimal CorrectPercent = 100;
        private const string ValidRequest = @"{ ""CalculationMethod"": 0, ""Items"": [{""MonthNumber"": 1, ""Income"": 0.08, ""Sum"": 1.08}]}";
        private IContributionService _contributionService;

        [Fact]
        public async void Calculate_ValidRequest_ValidResponse()
        {
            var httpClient = MoqHttpClientSetup(HttpStatusCode.OK, ValidRequest);
            _contributionService = new ContributionService(httpClient);
            var moqResponse = await _contributionService.Сalculate(GetCalculationRequest(CorrectSum, CorrectTerm, CorrectPercent));
            moqResponse.Should().BeEquivalentTo(GetCalculationResponse());
        }

        [Fact]
        public async void Calculate_NullRequest_ThrowException()
        {
            var httpClient = MoqHttpClientSetup(HttpStatusCode.BadRequest, null);
            _contributionService = new ContributionService(httpClient);
            Func<Task> act = async () => await _contributionService.Сalculate(null);
            await act.Should().ThrowAsync<Exception>().WithMessage("Server response is incorrect");
        }

        private HttpClient MoqHttpClientSetup(HttpStatusCode statusCode, string content)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = statusCode,
            };

            if (content == null)
            {
                response.Content = null;
            }
            else
            {
                response.Content = new StringContent(content);
            }
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            return new HttpClient(handlerMock.Object);
        }

        private ResponseCalculateContributionViewModel GetCalculationResponse()
        {
            return new ResponseCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,
                Items = new List<ResponseCalculateContributionViewModelItem>{
                    new ResponseCalculateContributionViewModelItem
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
    }
}
