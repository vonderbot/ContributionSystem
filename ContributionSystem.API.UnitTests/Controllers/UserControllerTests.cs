using ContributionSystem.API.Controllers;
using ContributionSystem.BusinessLogic.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.ViewModels.Models.User;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ContributionSystem.API.UnitTests.Controllers
{
    public class UserControllerTests
    {
        private readonly UserController _userController;

        private const int ValidId = 1;
        private const int InvalidId = 0;
        private const string ValidUserId = "23";
        private const bool UserStatus = true;

        public UserControllerTests()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(repo => repo
                .GetUsersList())
                .ReturnsAsync(new ResponseGetUsersListUserViewModel());
            mockUserService.Setup(repo => repo
                .ChangeUserStatus(new RequestChangeUserStatusUserViewModel()))
                .Returns(Task.FromResult(default(object)));
            mockUserService.Setup(repo => repo
                .ChangeUserStatus(null))
                .ThrowsAsync(new Exception());
            _userController = new UserController(mockUserService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, "Id"),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            _userController.HttpContext.User = new ClaimsPrincipal(identity);
        }

        [Test]
        public async Task ChangeUserStatus_NullRequest_The400Result()
        {
            var response = await _userController.ChangeUserStatus(null);
            var badRequestObjectResult = response as BadRequestObjectResult;

            badRequestObjectResult.Should().NotBeNull();
            badRequestObjectResult.StatusCode.Value.Should().Be(400);
        }

        [Test]
        public async Task ChangeUserStatus_ValidRequest_The200Result()
        {
            var response = await _userController.ChangeUserStatus(new RequestChangeUserStatusUserViewModel());
            var okResult = response as OkResult;

            okResult.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task GetUsersList_NoParametersPassed_The200Result()
        {
            var response = await _userController.GetUsersList();
            var okObjectResult = response as OkObjectResult;

            okObjectResult.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<ResponseGetUsersListUserViewModel>();
        }
    }
}
