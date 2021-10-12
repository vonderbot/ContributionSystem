using Bunit;
using ContributionSystem.UI.Components;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ContributionSystem.UI.UnitTests
{
    public class ContributionCalculatorFormPageTests
    {
        private readonly TestContext _testContext;
        private readonly Mock<IContributionService> _contributionServiceMock; 
        private const string _сorrectSum = "1";
        private const string _сorrectTerm = "1";
        private const string _сorrectPercent = "100";
        private const string _incorrectElementUnderMinValue = "0";
        private const string _incorrectElementToMuchDecimalPlaces = "0.000000000000000000000000001";

        public ContributionCalculatorFormPageTests()
        {
            _testContext = new TestContext();
            _contributionServiceMock = new Mock<IContributionService>();
            _testContext.Services.AddSingleton<IContributionService>(_contributionServiceMock.Object);
        }

        [Fact]
        public void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        {
            var page = _testContext.RenderComponent<ContributionCalculatorForm>();
            page.Find("#Percent").Should().NotBeNull();
            page.Find("#Term").Should().NotBeNull();
            page.Find("#Sum").Should().NotBeNull();
            page.Find("select").Should().NotBeNull();
            page.Find("button").Should().NotBeNull();
        }

        [Fact]
        public void WhenSubmitButtonClicked_ValidParameters_CalculateInvoked()
        {
            _contributionServiceMock.Setup(x => x.Сalculate(It.IsAny<RequestCalculateContributionViewModel>())).ReturnsAsync(GetCalculationResponse());
            var page = _testContext.RenderComponent<ContributionCalculatorForm>();
            InputsValuesAndSubmitForm(page, _сorrectPercent, _сorrectTerm, _сorrectSum);
            _contributionServiceMock.Verify(m => m.Сalculate(It.IsAny<RequestCalculateContributionViewModel>()), Times.Once());
        }

        [Fact]
        public void WhenSubmitButtonClicked_ValidParameters_RequestViewModelEventCallbackInvoked()
        {
            _contributionServiceMock.Setup(x => x.Сalculate(It.IsAny<RequestCalculateContributionViewModel>())).ReturnsAsync(GetCalculationResponse());
            var eventCalled = false;
            var page = _testContext.RenderComponent<ContributionCalculatorForm>(parameters => parameters
                .Add(p => p.ResponseCalculateContributionViewModelChanged, () => { eventCalled = true; }));
            InputsValuesAndSubmitForm(page, _сorrectPercent, _сorrectTerm, _сorrectSum);
            Assert.True(eventCalled);
        }

        [Fact]
        public void WhenSubmitButtonClicked_ValidParameters_ServerErrorMessageEventCallbackInvoked()
        {
            _contributionServiceMock.Setup(x => x.Сalculate(It.IsAny<RequestCalculateContributionViewModel>())).ThrowsAsync(new Exception("Mock exception"));
            var eventCalled = false;
            var page = _testContext.RenderComponent<ContributionCalculatorForm>(parameters => parameters
                .Add(p => p.ErrorMessageChanged, () => { eventCalled = true; }));
            InputsValuesAndSubmitForm(page, _сorrectPercent, _сorrectTerm, _сorrectSum);
            Assert.True(eventCalled);
        }

        [Fact]
        public void WhenSubmitButtonClicked_InvalidPercent_ValidationMinElementMessageAppear()
        {
            var page = _testContext.RenderComponent<ContributionCalculatorForm>();
            InputsValuesAndSubmitForm(page, _incorrectElementUnderMinValue, _сorrectTerm, _сorrectSum);
            var validationMessage = page.Find(".validation-message").TextContent;
            page.FindAll("[aria-invalid]").Count.Should().Be(1);
            validationMessage.Should().BeEquivalentTo("Percent can`t be less then 0.01");
        }

        [Fact]
        public void WhenSubmitButtonClicked_InvalidPercent_ValidationDecimalPlacesMessageAppear()
        {
            var page = _testContext.RenderComponent<ContributionCalculatorForm>();
            InputsValuesAndSubmitForm(page, _incorrectElementToMuchDecimalPlaces, _сorrectTerm, _сorrectSum);
            var validationMessage = page.Find(".validation-message").TextContent;
            page.FindAll("[aria-invalid]").Count.Should().Be(1);
            validationMessage.Should().BeEquivalentTo("Percent can only have 6 numbers, 2 of them after the decimal point");
        }

        [Fact]
        public void WhenSubmitButtonClicked_InvalidTerm_ValidationMinElementMessageAppear()
        {
            var page = _testContext.RenderComponent<ContributionCalculatorForm>();
            InputsValuesAndSubmitForm(page, _сorrectPercent, _incorrectElementUnderMinValue, _сorrectSum);
            var validationMessage = page.Find(".validation-message").TextContent;
            page.FindAll("[aria-invalid]").Count.Should().Be(1);
            validationMessage.Should().BeEquivalentTo("Term can`t be less then 1");
        }

        [Fact]
        public void WhenSubmitButtonClicked_InvalidSum_ValidationMinElementMessageAppear()
        {
            var page = _testContext.RenderComponent<ContributionCalculatorForm>();
            InputsValuesAndSubmitForm(page, _сorrectPercent, _сorrectTerm, _incorrectElementUnderMinValue);
            var validationMessage = page.Find(".validation-message").TextContent;
            page.FindAll("[aria-invalid]").Count.Should().Be(1);
            validationMessage.Should().BeEquivalentTo("Sum can`t be less then 0.01");
        }

        [Fact]
        public void WhenSubmitButtonClicked_InvalidSum_ValidationDecimalPlacesMessageAppear()
        {
            var page = _testContext.RenderComponent<ContributionCalculatorForm>();
            InputsValuesAndSubmitForm(page, _сorrectPercent, _сorrectTerm, _incorrectElementToMuchDecimalPlaces);
            var validationMessage = page.Find(".validation-message").TextContent;
            page.FindAll("[aria-invalid]").Count.Should().Be(1);
            validationMessage.Should().BeEquivalentTo("Sum can only have 12 numbers, 2 of them after the decimal point");
        }

        private static void InputsValuesAndSubmitForm(IRenderedComponent<ContributionCalculatorForm> page, string percent, string term, string sum)
        {
            page.Find("#Percent").Change(percent);
            page.Find("#Term").Change(term);
            page.Find("#Sum").Change(sum);
            page.Find("form").Submit();
        }

        private static ResponseCalculateContributionViewModel GetCalculationResponse()
        {
            return new ResponseCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,
                Items = new List<ResponseCalculateContributionViewModelItem>{
                    new ResponseCalculateContributionViewModelItem
                    {
                        MonthNumber = 1,
                        Income = 0.08M,
                        Sum = 1.08M
                    }
                }
            };
        }
    }
}
