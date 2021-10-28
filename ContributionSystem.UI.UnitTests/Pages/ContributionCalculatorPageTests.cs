using Bunit;
using ContributionSystem.UI.Components;
using ContributionSystem.UI.Pages;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace ContributionSystem.UI.UnitTests.Pages
{
    public class ContributionCalculatorPageTests : PageTestsBaseComponent
    {
        private const string CorrectSum = "1";
        private const string CorrectTerm = "1";
        private const string CorrectPercent = "100";

        //[Fact]
        //public void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        //{
        //    var page = _baseComponent._testContext.RenderComponent<ContributionCalculator>();
        //    page.FindComponent<ContributionCalculatorForm>().Should().NotBeNull();
        //    page.FindComponent<ContributionCalculatorTable>().Should().NotBeNull();
        //    page.FindAll("div h1").Should().BeEmpty();
        //}

        [Fact]
        public void WhenSubmitButtonClicked_ValidParameters_ServerExceptionRendered()
        {
            _baseComponent._contributionServiceMock.Setup(x => x.Сalculate(It.IsAny<RequestCalculateContributionViewModel>())).ThrowsAsync(new Exception("Mock exception"));
            var page = _baseComponent._testContext.RenderComponent<ContributionCalculator>();
            page.Find("#Percent").Change(CorrectPercent);
            page.Find("#Term").Change(CorrectTerm);
            page.Find("#Sum").Change(CorrectSum);
            page.Find("form").Submit();
            page.Find("div h1").InnerHtml.Should().BeEquivalentTo("Mock exception");
        }
    }
}
