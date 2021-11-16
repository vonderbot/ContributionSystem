﻿using Bunit;
using ContributionSystem.UI.Components;
using ContributionSystem.UI.UnitTests.Common;
using FluentAssertions;
using Xunit;

namespace ContributionSystem.UI.UnitTests.Components
{
    public class NavbarTests : PageTestsBaseComponent
    {
        [Fact]
        public void WhenHistoryButtonClicked_NoParametersPassed_Redirect()
        {
            var page = _baseComponent._testContext.RenderComponent<Navbar>();
            page.Find("#History").Click();
            Assert.Equal("http://localhost/History", _baseComponent.navigationManager.Uri);
        }

        [Fact]
        public void WhenCalculationsButtonClicked_NoParametersPassed_Redirect()
        {
            var page = _baseComponent._testContext.RenderComponent<Navbar>();
            page.Find("#Calculations").Click();
            Assert.Equal("http://localhost/Main", _baseComponent.navigationManager.Uri);
        }

        [Fact]
        public void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        {
            var page = _baseComponent._testContext.RenderComponent<Navbar>();
            page.Find("#Calculations").Should().NotBeNull();
            page.FindAll("#History").Should().NotBeNull();
        }
    }
}
