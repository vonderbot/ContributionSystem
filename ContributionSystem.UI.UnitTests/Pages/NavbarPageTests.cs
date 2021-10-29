using Bunit;
using ContributionSystem.UI.Components;
using FluentAssertions;
using Xunit;

namespace ContributionSystem.UI.UnitTests.Pages
{
    public class NavbarPageTests : PageTestsBaseComponent
    {
        [Fact]
        public void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        {
            var page = _baseComponent._testContext.RenderComponent<Navbar>();
            page.Find("#Calculations").Should().NotBeNull();
            page.FindAll("#History").Should().NotBeNull();
        }
    }
}
