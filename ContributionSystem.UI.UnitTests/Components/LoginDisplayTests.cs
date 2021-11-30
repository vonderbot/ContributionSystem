﻿using Bunit;
using ContributionSystem.UI.Components;
using FluentAssertions;
using ContributionSystem.UI.UnitTests.Common;
using Xunit;

namespace ContributionSystem.UI.UnitTests.Components
{
    public class LoginDisplayTests : PageTestsBaseComponent
    {
        [Fact]
        public void WhenLooutButtonClicked_UserAuthorized_RedirectToLogoutPage()
        {
            var page = TestContext.RenderComponent<LoginDisplay>();
            page.Find("#Logout").Click();
            Assert.Equal(URLs.Logout, NavigationManager.Uri);
        }

        [Fact]
        public void WhenPageRendered_UserAuthorized_ExpectedMarkupRendered()
        {
            var page = TestContext.RenderComponent<LoginDisplay>();
            page.Find("#Username").Should().NotBeNull();
            page.Find("#Logout").Should().NotBeNull();
        }
    }
}