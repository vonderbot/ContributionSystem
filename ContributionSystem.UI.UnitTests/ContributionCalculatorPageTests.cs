using Bunit;
using ContributionSystem.UI.Services;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.UI.Components;
using ContributionSystem.UI.Pages;
using ContributionSystem.ViewModels.Models.Contribution;
using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using ContributionSystem.ViewModels.Enums;
using Telerik.JustMock;
using System.Threading.Tasks;

namespace ContributionSystem.UI.UnitTests
{
    public class ContributionCalculatorPageTests
    {
        private TestContext testContext;

        public ContributionCalculatorPageTests()
        {
            testContext = new TestContext();
        }

        //[Fact]
        //public void ContributionCalculatorPageTest()
        //{
        //    // Arrange
        //    var model = new ResponseCalculateContributionViewModel
        //    {
        //        CalculationMethod = CalculationMethodEnumView.Simple,

        //        Items = new ResponseCalculateContributionViewModelItem[1]
        //       {
        //            new ResponseCalculateContributionViewModelItem
        //            {
        //                MonthNumber = 1,
        //                Income = 0.08M,
        //                Sum = 1.08M
        //            }
        //       }
        //    };

        //    var contributionServiceMock = Mock.Create<IContributionService>();
        //    Mock.Arrange(() => contributionServiceMock.Ñalculate(Arg.IsAny<RequestCalculateContributionViewModel>()))
        //        .Returns(Task.FromResult<ResponseCalculateContributionViewModel>(model));

        //    testContext.Services.AddSingleton<IContributionService>(contributionServiceMock);

        //    // Act
        //    var page = testContext.RenderComponent<ContributionCalculator>();
        //    page.Find("#Percent").Change("100");
        //    page.Find("#Term").Change("1");
        //    page.Find("#Sum").Change("1");
        //    page.Find("form").Submit();
        //    var actualContributionTable = page.FindComponent<ContributionCalculatorTable>();

        //    // Assert
        //    var expectedContributionTable = testContext.RenderComponent<ContributionCalculatorTable>((nameof(ContributionCalculatorTable.ResponseCalculateContributionViewModel), model));
        //    actualContributionTable.MarkupMatches(expectedContributionTable.Markup);
        //}

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
            var contributionServiceMock = Mock.Create<IContributionService>();
            Mock.Arrange(() => contributionServiceMock.Ñalculate(Arg.IsAny<RequestCalculateContributionViewModel>()))
                .Returns(Task.FromResult<ResponseCalculateContributionViewModel>(model));
            var testContext = new TestContext();
            testContext.Services.AddSingleton<IContributionService>(contributionServiceMock);
            bool eventCalled = false;

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

        [Fact]
        public void ContributionCalculatorTableTest()
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
            var page = testContext.RenderComponent<ContributionCalculatorTable>((nameof(ContributionCalculatorTable.ResponseCalculateContributionViewModel), model));

            // Act
            var expected = @"<tbody><tr><td scope = ""row"">1</td><td>0.08</td><td>1.08</td></tr></ tbody>";

            // Assert
            page.Find("tbody").MarkupMatches(expected);
        }
    }
}
