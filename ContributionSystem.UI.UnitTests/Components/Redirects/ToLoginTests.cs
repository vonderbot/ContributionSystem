using Bunit;
using ContributionSystem.UI.UnitTests.Common;
using Xunit;
using ContributionSystem.UI.Components.Redirects;

namespace ContributionSystem.UI.UnitTests.Components.Redirects
{
    public class ToLoginTests : PageTestsBaseComponent
    {
        [Fact]
        public void WhenPageRendered_NoParametersPassed_RedirectToLogin()
        {
            var page = TestContext.RenderComponent<ToLogin>();

            Assert.Equal(URLs.Login, NavigationManager.Uri);
        }
    }
}

