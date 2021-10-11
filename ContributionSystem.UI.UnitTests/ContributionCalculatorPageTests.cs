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
        //private TestContext testContext;

        //public ContributionCalculatorPageTests()
        //{
        //    //testContext = new TestContext();
        //    //testContext.Services.AddScoped<IContributionService, ContributionService>();
        //}

        //[Fact]
        //public void FormShouldRender ()
        //{
        //    // Arrange

        //    // Act
        //    var page = testContext.RenderComponent<ContributionCalculator>();
        //    var form = testContext.RenderComponent<ContributionCalculatorForm>();

        //    // Assert
        //    page.Find("Form".ToString()).MarkupMatches(form);
        //}

        //[Fact]
        //public void TableShouldRender()
        //{
        //    // Arrange

        //    // Act
        //    var page = testContext.RenderComponent<ContributionCalculator>();
        //    var table = testContext.RenderComponent<ContributionCalculatorTable>();

        //    // Assert
        //    page.Find("table".ToString()).MarkupMatches(table);
        //}

        [Fact]
        public void MockTest()
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

            // Act
            var page = testContext.RenderComponent<ContributionCalculator>();
            page.Find("#Percent").Change("100");
            page.Find("#Term").Change("1");
            page.Find("#Sum").Change("1");
            page.Find("form").Submit();
            var changes = page.GetChangesSinceFirstRender();
            var actualContributionTable = page.FindComponent<ContributionCalculatorTable>();

            // Assert
            var expectedContributionTable = testContext.RenderComponent<ContributionCalculatorTable>((nameof(ContributionCalculatorTable.ResponseCalculateContributionViewModel), model));
            actualContributionTable.MarkupMatches(expectedContributionTable.Markup);
        }

        //[Fact]
        //public void Mock2Test()
        //{
        //    // Arrange
        //    var exception = new Exception("Null request");

        //    var contributionServiceMock = Mock.Create<IContributionService>();
        //    Mock.Arrange(() => contributionServiceMock.Ñalculate(Arg.IsNull<RequestCalculateContributionViewModel>()))
        //        .Returns(Task.FromResult<Exception>(exception));

        //    var testContext = new TestContext();
        //    testContext.Services.AddSingleton<IContributionService>(contributionServiceMock);

        //    // Act
        //    var page = testContext.RenderComponent<ContributionCalculator>();
        //    page.Find("#Percent").Change("100");
        //    page.Find("#Term").Change("1");
        //    page.Find("#Sum").Change("1");
        //    page.Find("form").Submit();
        //    var changes = page.GetChangesSinceFirstRender();
        //    var actualContributionTable = page.FindComponent<ContributionCalculatorTable>();

        //    // Assert
        //    var expectedContributionTable = testContext.RenderComponent<ContributionCalculatorTable>((nameof(ContributionCalculatorTable.ResponseCalculateContributionViewModel), model));
        //    actualContributionTable.MarkupMatches(expectedContributionTable.Markup);
        //}

        //[Fact]
        //public void Test3()
        //{
        //    // Arrange
        //    var page = testContext.RenderComponent<ContributionCalculator>();
        //    //Act
        //    //page.fi
        //    //var inputs = page.FindAll("input");
        //    //page.Find("input").i(1);
        //    //inputs[0].Change(100);
        //    //inputs[1].Change(1);
        //    //inputs[2].Change(1);

        //    page.Find("#Percent").Change(100M);
        //    page.Find("#Term").Change(1);
        //    page.Find("#Sum").Change(1M);

        //    page.Find("form").Submit();

        //    var inputs = page.FindAll("validation-message");
        //    for (int i =1; i<= inputs.Count();i++)
        //    {
        //        Assert.Equal(inputs[i], inputs[i-1]);
        //    }

        //    //Assert.Equal(page.Find("#Percent".ToString()).ToString(), page.Find("#Percent".ToString()).InnerHtml);
        //    //Assert.Equal(page.Find("#Term".ToString()).ToString(), page.Find("#Term".ToString()).InnerHtml);
        //    //Assert.Equal(page.Find("#Sum".ToString()).ToString(), page.Find("#Sum".ToString()).InnerHtml);

        //    ////Act
        //    //page.Find("form").Submit();
        //    //var tmpe = page.GetChangesSinceFirstRender();

        //    //// Assert
        //    //var correctResponse = new ResponseCalculateContributionViewModel
        //    //{
        //    //    CalculationMethod = CalculationMethodEnumView.Simple,

        //    //    Items = new ResponseCalculateContributionViewModelItem[1]
        //    //   {
        //    //        new ResponseCalculateContributionViewModelItem
        //    //        {
        //    //            MonthNumber = 1,
        //    //            Income = 0.08M,
        //    //            Sum = 1.08M
        //    //        }
        //    //   }
        //    //};
        //    ////page.SetParametersAndRender(parameters => parameters.Add<ContributionCalculatorTable>(pr => pr.Add(p => p.ResponseCalculateContributionViewModel, correctResponse)));
        //    //var tmp = testContext.RenderComponent<ContributionCalculatorTable>(parameters => parameters.Add(p => p.ResponseCalculateContributionViewModel, correctResponse));
        //    //page.Find("table").MarkupMatches(tmp);
        //    ////page.Find("h1");
        //}
    }
}
