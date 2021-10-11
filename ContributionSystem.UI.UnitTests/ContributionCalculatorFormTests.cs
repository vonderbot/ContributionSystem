﻿using Bunit;
using ContributionSystem.UI.Components;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace ContributionSystem.UI.UnitTests
{
    public class ContributionCalculatorFormTests
    {
        private TestContext testContext;
        private Mock<IContributionService> contributionServiceMock;

        public ContributionCalculatorFormTests()
        {
            testContext = new TestContext();
        }

        [Fact]
        public void ContributionCalculatorFormEventCallbackTest()
        {
            // Arrange
            var model = new ResponseCalculateContributionViewModel
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
            contributionServiceMock = new Mock<IContributionService>();
           contributionServiceMock.Setup(x => x.Сalculate(It.IsAny<RequestCalculateContributionViewModel>()))
                .ReturnsAsync(model);
            var testContext = new TestContext();
            testContext.Services.AddSingleton<IContributionService>(contributionServiceMock.Object);
            bool eventCalled = false;
0
            // Act
            var page = testContext.RenderComponent<ContributionCalculatorForm>(parameters => parameters
                .Add(p => p.ResponseCalculateContributionViewModelChanged, () => { eventCalled = true; }));
            page.Find("#Percent").Change("100");
            page.Find("#Term").Change("1");
            page.Find("#Sum").Change("1");
            page.Find("form").Submit();

            // Assert
            Assert.True(eventCalled);
        }

        [Fact]
        public void ContributionCalculatorFormValidationTest()
        {
            // Arrange
            var contributionServiceMock = Mock.Create<IContributionService>();
            testContext.Services.AddSingleton<IContributionService>(contributionServiceMock);
            var page = testContext.RenderComponent<ContributionCalculatorForm>();

            // Act
            page.Find("#Percent").Change("0");
            page.Find("#Term").Change("F");
            page.Find("#Sum").Change("0.0000000003450000012");
            page.Find("form").Submit();
            var errors = page.FindAll("[aria-invalid]");

            // Assert
            Assert.Equal(3, errors.Count);
        }
    }
}