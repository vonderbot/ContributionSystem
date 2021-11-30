using Bunit;
using ContributionSystem.UI.UnitTests.Common;
using Xunit;
using ContributionSystem.UI.Components.Redirects;

namespace ContributionSystem.UI.UnitTests.Components.Redirects
{
    public class ToMainTests : PageTestsBaseComponent
    {
        [Fact]
        public void WhenPageRendered_NoParametersPassed_RedirectToLogin()
        {
            var page = TestContext.RenderComponent<ToMain>();

            Assert.Equal(URLs.Calculation, NavigationManager.Uri);
        }
    }
}
