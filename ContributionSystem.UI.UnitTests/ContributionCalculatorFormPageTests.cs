using Bunit;
using ContributionSystem.UI.Components;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ContributionSystem.UI.UnitTests
{
    public class ContributionCalculatorFormPageTests
    {
        private TestContext testContext;
        private Mock<IContributionService> contributionServiceMock;

        public ContributionCalculatorFormPageTests()
        {
            testContext = new TestContext();
            contributionServiceMock = new Mock<IContributionService>();
            testContext.Services.AddSingleton<IContributionService>(contributionServiceMock.Object);
        }

        [Fact]
        public void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        {
            // Arrange
            var page = testContext.RenderComponent<ContributionCalculatorForm>();

            // Act

            // Assert
            page.Find("#Percent").Should().NotBeNull();
            page.Find("#Term").Should().NotBeNull();
            page.Find("#Sum").Should().NotBeNull();
            page.Find("select").Should().NotBeNull();
            page.Find("button").Should().NotBeNull();
        }

        [Fact]
        public void WhenSubmitButtonClicked_ValidParameters_CalculateInvoked()
        {
            // Arrange
            contributionServiceMock.Setup(x => x.Сalculate(It.IsAny<RequestCalculateContributionViewModel>())).ReturnsAsync(GetCalculationResponce());

            // Act
            var page = testContext.RenderComponent<ContributionCalculatorForm>();
            InputsValuesAndSubmitForm(page, "100", "1", "1");

            // Assert
            contributionServiceMock.Verify(m => m.Сalculate(It.IsAny<RequestCalculateContributionViewModel>()), Times.Once());
        }

        [Fact]
        public void WhenSubmitButtonClicked_ValidParameters_RequestViewModelEventCallbackInvoked()
        {
            // Arrange
            contributionServiceMock.Setup(x => x.Сalculate(It.IsAny<RequestCalculateContributionViewModel>())).ReturnsAsync(GetCalculationResponce());
            bool eventCalled = false;

            // Act
            var page = testContext.RenderComponent<ContributionCalculatorForm>(parameters => parameters
                .Add(p => p.ResponseCalculateContributionViewModelChanged, () => { eventCalled = true; }));
            InputsValuesAndSubmitForm(page, "100", "1", "1");

            // Assert
            Assert.True(eventCalled);
        }

        [Fact]
        public void WhenSubmitButtonClicked_ValidParameters_ServerErrorMessageEventCallbackInvoked()
        {
            // Arrange
            contributionServiceMock.Setup(x => x.Сalculate(It.IsAny<RequestCalculateContributionViewModel>())).ThrowsAsync(new Exception("Mock exception"));
            bool eventCalled = false;

            // Act
            var page = testContext.RenderComponent<ContributionCalculatorForm>(parameters => parameters
                .Add(p => p.ErrorMessageChanged, () => { eventCalled = true; }));
            InputsValuesAndSubmitForm(page, "100", "1", "1");

            // Assert
            Assert.True(eventCalled);
        }

        [Fact]
        public void WhenSubmitButtonClicked_InvalidPercent_ValidationMinElementMessageAppear()
        {
            // Arrange
            var page = testContext.RenderComponent<ContributionCalculatorForm>();

            // Act
            InputsValuesAndSubmitForm(page, "0", "1", "1");
            var validationMessage = page.Find(".validation-message").TextContent;

            // Assert
            page.FindAll("[aria-invalid]").Count.Should().Be(1);
            validationMessage.Should().BeEquivalentTo("Percent can`t be less then 0.01");
        }

        [Fact]
        public void WhenSubmitButtonClicked_InvalidPercent_ValidationDecimalPlacesMessageAppear()
        {
            // Arrange
            var page = testContext.RenderComponent<ContributionCalculatorForm>();

            // Act
            InputsValuesAndSubmitForm(page, "0.000000000000000000000000001", "1", "1");
            var validationMessage = page.Find(".validation-message").TextContent;

            // Assert
            page.FindAll("[aria-invalid]").Count.Should().Be(1);
            validationMessage.Should().BeEquivalentTo("Percent can only have 6 numbers, 2 of them after the decimal point");
        }

        [Fact]
        public void WhenSubmitButtonClicked_InvalidTerm_ValidationMinElementMessageAppear()
        {
            // Arrange
            var page = testContext.RenderComponent<ContributionCalculatorForm>();

            // Act
            InputsValuesAndSubmitForm(page, "1", "0", "1");
            var validationMessage = page.Find(".validation-message").TextContent;

            // Assert
            page.FindAll("[aria-invalid]").Count.Should().Be(1);
            validationMessage.Should().BeEquivalentTo("Term can`t be less then 1");
        }

        [Fact]
        public void WhenSubmitButtonClicked_InvalidSum_ValidationMinElementMessageAppear()
        {
            // Arrange
            var page = testContext.RenderComponent<ContributionCalculatorForm>();

            // Act
            InputsValuesAndSubmitForm(page, "1", "1", "0");
            var validationMessage = page.Find(".validation-message").TextContent;

            // Assert
            page.FindAll("[aria-invalid]").Count.Should().Be(1);
            validationMessage.Should().BeEquivalentTo("Sum can`t be less then 0.01");
        }

        [Fact]
        public void WhenSubmitButtonClicked_InvalidSum_ValidationDecimalPlacesMessageAppear()
        {
            // Arrange
            var page = testContext.RenderComponent<ContributionCalculatorForm>();

            // Act
            InputsValuesAndSubmitForm(page, "1", "1", "0.000000000000000000000000001");
            var validationMessage = page.Find(".validation-message").TextContent;

            // Assert
            page.FindAll("[aria-invalid]").Count.Should().Be(1);
            validationMessage.Should().BeEquivalentTo("Sum can only have 12 numbers, 2 of them after the decimal point");
        }

        private void InputsValuesAndSubmitForm(IRenderedComponent<ContributionCalculatorForm> page, string Percent, string Term, string Sum)
        {
            page.Find("#Percent").Change(Percent);
            page.Find("#Term").Change(Term);
            page.Find("#Sum").Change(Sum);
            page.Find("form").Submit();
        }

        private ResponseCalculateContributionViewModel GetCalculationResponce()
        {
            return new ResponseCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,

                Items = new ResponseCalculateContributionViewModelItem[1]
                {
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
