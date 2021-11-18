﻿using Bunit;
using ContributionSystem.UI.Components;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Moq;
using System;
using ContributionSystem.UI.UnitTests.Common;
using Xunit;

namespace ContributionSystem.UI.UnitTests.Components
{
    public class ContributionCalculatorFormTests : PageTestsBaseComponent
    {
        private const string CorrectSum = "1";
        private const string CorrectTerm = "1";
        private const string CorrectPercent = "100";
        private const string IncorrectElementUnderMinValue = "0";
        private const string IncorrectElementToMuchDecimalPlaces = "0.000000000000000000000000001";

        [Fact]
        public void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        {
            var page = TestContext.RenderComponent<ContributionCalculatorForm>();

            page.Find("#Percent").Should().NotBeNull();
            page.Find("#Term").Should().NotBeNull();
            page.Find("#Sum").Should().NotBeNull();
            page.Find("select").Should().NotBeNull();
            page.Find("button").Should().NotBeNull();
        }

        [Fact]
        public void WhenSubmitButtonClicked_ValidParameters_CalculateInvoked()
        {
            ContributionServiceMock.Setup(x => x.Сalculate(It.IsAny<RequestCalculateContributionViewModel>())).ReturnsAsync(GetCalculationResponse());
            var page = TestContext.RenderComponent<ContributionCalculatorForm>();
            InputsValuesAndSubmitForm(page, CorrectPercent, CorrectTerm, CorrectSum);

            ContributionServiceMock.Verify(m => m.Сalculate(It.IsAny<RequestCalculateContributionViewModel>()), Times.Once());
        }

        [Fact]
        public void WhenSubmitButtonClicked_ValidParameters_RequestViewModelEventCallbackInvoked()
        {
            ContributionServiceMock.Setup(x => x.Сalculate(It.IsAny<RequestCalculateContributionViewModel>())).ReturnsAsync(GetCalculationResponse());
            var eventCalled = false;
            var page = TestContext.RenderComponent<ContributionCalculatorForm>(parameters => parameters
                .Add(p => p.ResponseCalculateContributionViewModelChanged, () => { eventCalled = true; }));
            InputsValuesAndSubmitForm(page, CorrectPercent, CorrectTerm, CorrectSum);

            Assert.True(eventCalled);
        }

        [Fact]
        public void WhenSubmitButtonClicked_ValidParameters_ServerErrorMessageEventCallbackInvoked()
        {
            ContributionServiceMock.Setup(x => x.Сalculate(It.IsAny<RequestCalculateContributionViewModel>())).ThrowsAsync(new Exception("Mock exception"));
            var eventCalled = false;
            var page = TestContext.RenderComponent<ContributionCalculatorForm>(parameters => parameters
                .Add(p => p.ErrorMessageChanged, () => { eventCalled = true; }));
            InputsValuesAndSubmitForm(page, CorrectPercent, CorrectTerm, CorrectSum);

            Assert.True(eventCalled);
        }

        [Fact]
        public void WhenSubmitButtonClicked_InvalidPercent_ValidationMinElementMessageAppear()
        {
            var page = TestContext.RenderComponent<ContributionCalculatorForm>();
            InputsValuesAndSubmitForm(page, IncorrectElementUnderMinValue, CorrectTerm, CorrectSum);
            var validationMessage = page.Find(".validation-message").TextContent;

            page.FindAll("[aria-invalid]").Count.Should().Be(1);
            validationMessage.Should().BeEquivalentTo("Percent can`t be less then 0.01");
        }

        [Fact]
        public void WhenSubmitButtonClicked_InvalidPercent_ValidationDecimalPlacesMessageAppear()
        {
            var page = TestContext.RenderComponent<ContributionCalculatorForm>();
            InputsValuesAndSubmitForm(page, IncorrectElementToMuchDecimalPlaces, CorrectTerm, CorrectSum);
            var validationMessage = page.Find(".validation-message").TextContent;

            page.FindAll("[aria-invalid]").Count.Should().Be(1);
            validationMessage.Should().BeEquivalentTo("Percent can only have 6 numbers, 2 of them after the decimal point");
        }

        [Fact]
        public void WhenSubmitButtonClicked_InvalidTerm_ValidationMinElementMessageAppear()
        {
            var page = TestContext.RenderComponent<ContributionCalculatorForm>();
            InputsValuesAndSubmitForm(page, CorrectPercent, IncorrectElementUnderMinValue, CorrectSum);
            var validationMessage = page.Find(".validation-message").TextContent;

            page.FindAll("[aria-invalid]").Count.Should().Be(1);
            validationMessage.Should().BeEquivalentTo("Term can`t be less then 1");
        }

        [Fact]
        public void WhenSubmitButtonClicked_InvalidSum_ValidationMinElementMessageAppear()
        {
            var page = TestContext.RenderComponent<ContributionCalculatorForm>();
            InputsValuesAndSubmitForm(page, CorrectPercent, CorrectTerm, IncorrectElementUnderMinValue);
            var validationMessage = page.Find(".validation-message").TextContent;

            page.FindAll("[aria-invalid]").Count.Should().Be(1);
            validationMessage.Should().BeEquivalentTo("Sum can`t be less then 0.01");
        }

        [Fact]
        public void WhenSubmitButtonClicked_InvalidSum_ValidationDecimalPlacesMessageAppear()
        {
            var page = TestContext.RenderComponent<ContributionCalculatorForm>();
            InputsValuesAndSubmitForm(page, CorrectPercent, CorrectTerm, IncorrectElementToMuchDecimalPlaces);
            var validationMessage = page.Find(".validation-message").TextContent;

            page.FindAll("[aria-invalid]").Count.Should().Be(1);
            validationMessage.Should().BeEquivalentTo("Sum can only have 12 numbers, 2 of them after the decimal point");
        }

        private void InputsValuesAndSubmitForm(IRenderedComponent<ContributionCalculatorForm> page, string percent, string term, string sum)
        {
            page.Find("#Percent").Change(percent);
            page.Find("#Term").Change(term);
            page.Find("#Sum").Change(sum);
            page.Find("form").Submit();
        }
    }
}
