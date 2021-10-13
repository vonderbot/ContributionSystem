using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ContributionSystem.UI.UnitTests
{
    public class ContributionServiceTests
    {
        private IContributionService _contributionService;

        public ContributionServiceTests()
        {
            _contributionService = new ContributionService(new HttpClient());
        }

        [Fact]
        public async void Calculate_ValidRequest_ValidResponse()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{ ""CalculationMethod"": 0, ""Items"": [{""MonthNumber"": 1, ""Income"": 0.08, ""Sum"": 1.08}]}"),
            };
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);
            _contributionService = new ContributionService(httpClient);
            var request = new RequestCalculateContributionViewModel()
            {
                CalculationMethod = 0,
                StartValue = 1,
                Term = 1,
                Percent = 100
            };
            var moqResponse = await _contributionService.Сalculate(null);
            moqResponse.Should().BeEquivalentTo(GetCalculationResponse());
        }

        [Fact]
        public async void Calculate_NullRequest_ThrowException()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(@"{ ""CalculationMethod"": 0, ""Items"": [{""MonthNumber"": 1, ""Income"": 0.08, ""Sum"": 1.08}]}"),
            };
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);
            _contributionService = new ContributionService(httpClient);
            try
            {
                await _contributionService.Сalculate(null);
            }
            catch(Exception exception)
            {
                exception.Message.Should().BeEquivalentTo("Server response is incorrect");
            }
        }

        protected static ResponseCalculateContributionViewModel GetCalculationResponse()
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
    }
}
