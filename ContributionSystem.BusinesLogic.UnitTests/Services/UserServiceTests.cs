using FluentAssertions;
using NUnit.Framework;
using System;
using ContributionSystem.BusinessLogic.Services;
using System.Collections.Generic;
using System.Security.Claims;
using Moq;
using Microsoft.Graph;

namespace ContributionSystem.BusinesLogic.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private const string NameIdentifier = "Id";

        public UserServiceTests()
        {
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockHttpProvider = new Mock<IHttpProvider>();
            var mockGraphClient = new Mock<GraphServiceClient>(mockAuthProvider.Object, mockHttpProvider.Object);
            _userService = new UserService(mockGraphClient.Object);
        }

        [Test]
        public void GetUserId_UserWithId_ValidResponse()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, "Id"),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var response = _userService.GetUserId(new ClaimsPrincipal(identity));

            response.Should().BeEquivalentTo(NameIdentifier);
        }

        [Test]
        public void GetUserId_UserWithoutId_ThrowException()
        {
            var claims = new List<Claim>();
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            Func<string> act = () => _userService.GetUserId(new ClaimsPrincipal(identity));

            act.Should().Throw<Exception>().WithMessage("User have no id");
        }
    }
}
