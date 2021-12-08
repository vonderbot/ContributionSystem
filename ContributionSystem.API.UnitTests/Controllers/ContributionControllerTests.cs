//using ContributionSystem.API.Controllers;
//using ContributionSystem.BusinessLogic.Interfaces;
//using ContributionSystem.ViewModels.Models.Contribution;
//using FluentAssertions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace ContributionSystem.API.UnitTests.Controllers
//{
//    public class ContributionControllerTests
//    {
//        private readonly ContributionController _contributionController;

//        private const int ValidId = 1;
//        private const int InvalidId = 0;

//        public ContributionControllerTests()
//        {
//            var mockContributionService = new Mock<IContributionService>();
//            mockContributionService.Setup(repo => repo
//                .Calculate(It.IsAny<RequestCalculateContributionViewModel>()))
//                .ReturnsAsync( new ResponseCalculateContributionViewModel());
//            mockContributionService.Setup(repo => repo
//                .Calculate(null))
//                .ThrowsAsync(new Exception());
//            mockContributionService.Setup(repo => repo
//                .GetHistoryByUserId(It.IsAny<RequestGetHistoryByUserIdContributionViewModel>()))
//                .ReturnsAsync(new ResponseGetHistoryByUserIdContributionViewModel());
//            mockContributionService.Setup(repo => repo
//                .GetHistoryByUserId(null))
//                .ThrowsAsync(new Exception());
//            mockContributionService.Setup(repo => repo
//                .GetDetailsById(It.Is<int>(p => p > 0)))
//                .ReturnsAsync(new ResponseGetDetailsByIdContributionViewModel());
//            mockContributionService.Setup(repo => repo
//                .GetDetailsById(It.Is<int>(p => p <= 0)))
//                .ThrowsAsync(new Exception());
//            var mockUserService = new Mock<IUserService>();
//            _contributionController = new ContributionController(mockContributionService.Object, mockUserService.Object)
//            {
//                ControllerContext = new ControllerContext()
//                {
//                    HttpContext = new DefaultHttpContext()
//                }
//            };
//            var claims = new List<Claim>()
//            {
//                new Claim(ClaimTypes.NameIdentifier, "Id"),
//            };
//            var identity = new ClaimsIdentity(claims, "TestAuthType");
//            _contributionController.HttpContext.User = new ClaimsPrincipal(identity);
//        }

//        [Test]
//        public async Task GetDetailsById_ValidId_The200Result()
//        {
//            var response = await _contributionController.GetDetailsById(ValidId);
//            var okObjectResult = response as OkObjectResult;

//            okObjectResult.Should().NotBeNull();
//            okObjectResult.Value.Should().BeOfType<ResponseGetDetailsByIdContributionViewModel>();
//        }

//        [Test]
//        public async Task GetDetailsById_InvalidId_The400Result()
//        {
//            var response = await _contributionController.GetDetailsById(InvalidId);
//            var badRequestObjectResult = response as BadRequestObjectResult;

//            badRequestObjectResult.Should().NotBeNull();
//            badRequestObjectResult.StatusCode.Value.Should().Be(400);
//        }

//        [Test]
//        public async Task GetHistory_ValidRequest_The200Result()
//        {
//            var response = await _contributionController.GetHistoryByUserId(new RequestGetHistoryByUserIdContributionViewModel());
//            var okObjectResult = response as OkObjectResult;

//            okObjectResult.Should().NotBeNull();
//            okObjectResult.Value.Should().BeOfType<ResponseGetHistoryByUserIdContributionViewModel>();
//        }

//        [Test]
//        public async Task GetHistory_NullRequest_The400Result()
//        {
//            var response = await _contributionController.GetHistoryByUserId(null);
//            var badRequestObjectResult = response as BadRequestObjectResult;

//            badRequestObjectResult.Should().NotBeNull();
//            badRequestObjectResult.StatusCode.Value.Should().Be(400);
//        }

//        [Test]
//        public async Task Calculate_ValidRequest_The200Result()
//        {
//            var response = await _contributionController.Calculate(new RequestCalculateContributionViewModel());
//            var okObjectResult = response as OkObjectResult;

//            okObjectResult.Should().NotBeNull();
//            okObjectResult.Value.Should().BeOfType<ResponseCalculateContributionViewModel>();
//        }

//        [Test]
//        public async Task Calculate_NullRequest_The400Result()
//        {
//            var response = await _contributionController.Calculate(null);
//            var badRequestObjectResult = response as BadRequestObjectResult;

//            badRequestObjectResult.Should().NotBeNull();
//            badRequestObjectResult.StatusCode.Value.Should().Be(400);
//        }
//    }
//}