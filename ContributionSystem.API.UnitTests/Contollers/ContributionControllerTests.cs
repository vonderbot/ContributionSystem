using ContributionSystem.API.Controllers;
using ContributionSystem.BusinessLogic.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ContributionSystem.API.UnitTests.Contollers
{
    public class ContributionControllerTests
    {
        private readonly ContributionController contributionController;

        private const int PozitiveNumber = 1;
        private const int Zero = 0;

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
            contributionController = new ContributionController(mock.Object);
        }

        [Test]
        public async Task GetDetailsById_Positivenumber_OkObjectResultWithResponseCalculateContributionViewModel()
        {
            var response = await contributionController.GetDetailsById(PozitiveNumber);
            var okObjectResult = response as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<ResponseGetDetailsByIdContributionViewModel>();
        }

        [Test]
        public async Task GetDetailsById_Zero_ThrowException()
        {
            var response = await contributionController.GetDetailsById(Zero);
            var BadRequestObjectResult = response as BadRequestObjectResult;
            BadRequestObjectResult.Should().NotBeNull();
            BadRequestObjectResult.StatusCode.ToString().Should().BeEquivalentTo("400");

        }

        [Test]
        public async Task GetHistory_ValidRequest_OkObjectResultWithResponseCalculateContributionViewModel()
        {
            var response = await contributionController.GetHistory(new RequestGetHistoryContributionViewModel());
            var okObjectResult = response as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<ResponseGetHistoryContributionViewModel>();
        }

        [Test]
        public async Task GetHistory_NullRequest_ThrowException()
        {
            var response = await contributionController.GetHistory(null);
            var BadRequestObjectResult = response as BadRequestObjectResult;
            BadRequestObjectResult.Should().NotBeNull();
            BadRequestObjectResult.StatusCode.ToString().Should().BeEquivalentTo("400");

        }

        [Test]
        public async Task Calculate_ValidRequest_OkObjectResultWithResponseCalculateContributionViewModel()
        {
            var response = await contributionController.Calculate(new RequestCalculateContributionViewModel());
            var okObjectResult = response as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<ResponseCalculateContributionViewModel>();
        }

        [Test]
        public async Task Calculate_NullRequest_ThrowException()
        {
            var response = await contributionController.Calculate(null);
            var BadRequestObjectResult = response as BadRequestObjectResult;
            BadRequestObjectResult.Should().NotBeNull();
            BadRequestObjectResult.StatusCode.ToString().Should().BeEquivalentTo("400");

        }
    }
}