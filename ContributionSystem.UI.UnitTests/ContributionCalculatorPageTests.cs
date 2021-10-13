using Bunit;
using ContributionSystem.UI.Components;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.UI.Pages;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace ContributionSystem.UI.UnitTests
{
    public class ContributionCalculatorPageTests : PageTests
    {
        private const string _сorrectSum = "1";
        private const string _сorrectTerm = "1";
        private const string _сorrectPercent = "100";

        public ContributionCalculatorPageTests() : base() { }

        [Fact]
        public override void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        {
            var page = _testContext.RenderComponent<ContributionCalculator>();
            page.FindComponent<ContributionCalculatorForm>().Should().NotBeNull();
            page.FindComponent<ContributionCalculatorTable>().Should().NotBeNull();
            page.FindAll("div h1").Should().BeEmpty();
        }

        [Fact]
        public void WhenSubmitButtonClicked_ValidParameters_ServerExceptionRendered()
        {
            _contributionServiceMock.Setup(x => x.Сalculate(It.IsAny<RequestCalculateContributionViewModel>())).ThrowsAsync(new Exception("Mock exception"));
            var page = _testContext.RenderComponent<ContributionCalculator>();
            page.Find("#Percent").Change(_сorrectPercent);
            page.Find("#Term").Change(_сorrectTerm);
            page.Find("#Sum").Change(_сorrectSum);
            page.Find("form").Submit();
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("Mock exception");
        }
    }
}
