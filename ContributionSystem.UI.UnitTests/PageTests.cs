using Bunit;
using ContributionSystem.UI.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContributionSystem.UI.UnitTests
{
    abstract public class PageTests
    {
        protected readonly TestContext _testContext;
        protected readonly Mock<IContributionService> _contributionServiceMock;

        public PageTests()
        {
            _testContext = new TestContext();
            _contributionServiceMock = new Mock<IContributionService>();
            _testContext.Services.AddSingleton<IContributionService>(_contributionServiceMock.Object);
        }

        public abstract void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered();
    }
}
