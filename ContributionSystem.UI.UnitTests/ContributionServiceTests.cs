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

namespace ContributionSystem.UI.UnitTests
{
    public class ContributionServiceTests
    {
        //private readonly ContributionService contributionService;

        //public ContributionServiceTests()
        //{
        //    contributionService = new ContributionService();
        //}

        //[Fact]
        //public void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        //{
        //    // Arrange

        //    var handlerMock = new Mock<HttpMessageHandler>();
        //    var response = new HttpResponseMessage
        //    {
        //        StatusCode = HttpStatusCode.OK,
        //        Content = new StringContent(@"[{ ""id"": 1, ""title"": ""Cool post!""}, { ""id"": 100, ""title"": ""Some title""}]"),
        //    };

        //    handlerMock
        //       .Protected()
        //       .Setup<Task<HttpResponseMessage>>(
        //          "SendAsync",
        //          ItExpr.IsAny<HttpRequestMessage>(),
        //          ItExpr.IsAny<CancellationToken>())
        //       .ReturnsAsync(response);
        //    var httpClient = new HttpClient(handlerMock.Object);
        //    var posts = new Posts(httpClient);

        //    // Act

        //    // Assert
        //}

    }
}
