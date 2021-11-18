using ContributionSystem.API.Controllers;
using ContributionSystem.BusinessLogic.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ContributionSystem.API.UnitTests.Controllers
{
    public class ContributionControllerTests
    {
        private readonly ContributionController _contributionController;

        private const int ValidId = 1;
        private const int InvalidId = 0;

        public ContributionControllerTests()
        {
            var mock = new Mock<IContributionService>();
            mock.Setup(repo => repo
                .Calculate(It.IsAny<RequestCalculateContributionViewModel>()))
                .ReturnsAsync( new ResponseCalculateContributionViewModel());
            mock.Setup(repo => repo
                .Calculate(null))
                .ThrowsAsync(new Exception());
            mock.Setup(repo => repo
                .GetHistory(It.IsAny<RequestGetHistoryContributionViewModel>()))
                .ReturnsAsync(new ResponseGetHistoryContributionViewModel());
            mock.Setup(repo => repo
                .GetHistory(null))
                .ThrowsAsync(new Exception());
            mock.Setup(repo => repo
                .GetDetailsById(It.Is<int>(p => p > 0)))
                .ReturnsAsync(new ResponseGetDetailsByIdContributionViewModel());
            mock.Setup(repo => repo
                .GetDetailsById(It.Is<int>(p => p <= 0)))
                .ThrowsAsync(new Exception());
            _contributionController = new ContributionController(mock.Object);
        }

        [Test]
        public async Task GetDetailsById_ValidId_The200Result()
        {
            var response = await _contributionController.GetDetailsById(ValidId);
            var okObjectResult = response as OkObjectResult;

            okObjectResult.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<ResponseGetDetailsByIdContributionViewModel>();
        }

        [Test]
        public async Task GetDetailsById_InvalidId_The400Result()
        {
            var response = await _contributionController.GetDetailsById(InvalidId);
            var badRequestObjectResult = response as BadRequestObjectResult;

            badRequestObjectResult.Should().NotBeNull();
            badRequestObjectResult.StatusCode.Value.Should().Be(400);
        }

        [Test]
        public async Task GetHistory_ValidRequest_The200Result()
        {
            var response = await _contributionController.GetHistory(new RequestGetHistoryContributionViewModel());
            var okObjectResult = response as OkObjectResult;

            okObjectResult.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<ResponseGetHistoryContributionViewModel>();
        }

        [Test]
        public async Task GetHistory_NullRequest_The400Result()
        {
            var response = await _contributionController.GetHistory(null);
            var badRequestObjectResult = response as BadRequestObjectResult;

            badRequestObjectResult.Should().NotBeNull();
            badRequestObjectResult.StatusCode.Value.Should().Be(400);
        }

        [Test]
        public async Task Calculate_ValidRequest_The200Result()
        {
            var response = await _contributionController.Calculate(new RequestCalculateContributionViewModel());
            var okObjectResult = response as OkObjectResult;

            okObjectResult.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<ResponseCalculateContributionViewModel>();
        }

        [Test]
        public async Task Calculate_NullRequest_The400Result()
        {
            var response = await _contributionController.Calculate(null);
            var badRequestObjectResult = response as BadRequestObjectResult;

            badRequestObjectResult.Should().NotBeNull();
            badRequestObjectResult.StatusCode.Value.Should().Be(400);
        }
    }
}