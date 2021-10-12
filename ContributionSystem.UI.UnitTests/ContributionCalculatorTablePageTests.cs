using Bunit;
using ContributionSystem.UI.Components;
using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace ContributionSystem.UI.UnitTests
{
    public class ContributionCalculatorTablePageTests
    {
        private TestContext testContext;
        private Mock<IContributionService> contributionServiceMock;

        public ContributionCalculatorTablePageTests()
        {
            testContext = new TestContext();
            contributionServiceMock = new Mock<IContributionService>();
            testContext.Services.AddSingleton<IContributionService>(contributionServiceMock.Object);
        }

        [Fact]
        public void WhenPageRendered_NullResponce_ExpectedMarkupRendered()
        {
            // Arrange
            var page = testContext.RenderComponent<ContributionCalculatorTable>(parameters => parameters
                .Add(p => p.ResponseCalculateContributionViewModel, null));

            // Act

            // Assert
            page.Find("thead").Should().NotBeNull();
            Assert.True(ElementDoesNotExist(page, "tbody"));
        }

        [Fact]
        public void WhenPageRendered_ValidResponce_ExpectedMarkupRendered()
        {
            // Arrange
            var page = testContext.RenderComponent<ContributionCalculatorTable>(parameters => parameters
                .Add(p => p.ResponseCalculateContributionViewModel, GetCalculationResponce()));

            // Act

            // Assert
            page.Find("thead").Should().NotBeNull();
            page.Find("tbody").Should().NotBeNull();
            var rows = page.FindAll("tbody tr");
            rows.Count.Should().Be(GetCalculationResponce().Items.Length);
            var cells = page.FindAll("tbody tr td");
            var tmp = 0;
            for(int i = 0; i < cells.Count; i++)
            {
                switch (i % 3)
                {
                    case 0:
                        cells[i].TextContent.Should().BeEquivalentTo(GetCalculationResponce().Items[tmp].MonthNumber.ToString());
                        break;
                    case 1:
                        cells[i].TextContent.Should().BeEquivalentTo(GetCalculationResponce().Items[tmp].Income.ToString());
                        break;
                    case 2:
                        cells[i].TextContent.Should().BeEquivalentTo(GetCalculationResponce().Items[tmp].Sum.ToString());
                        tmp++;
                        break;
                }
            }
        }

        [Fact]
        public void WhenPageRendered_NoParametersPassed_ExpectedMarkupRendered()
        {
            // Arrange
            var page = testContext.RenderComponent<ContributionCalculatorTable>();

            // Act

            // Assert
            page.Find("thead").Should().NotBeNull();
            Assert.True(ElementDoesNotExist(page, "tbody"));
        }

        private ResponseCalculateContributionViewModel GetCalculationResponce()
        {
            return new ResponseCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,

                Items = new ResponseCalculateContributionViewModelItem[2]
                    {
                        new ResponseCalculateContributionViewModelItem
                        {
                            MonthNumber = 1,
                            Income = 0.08M,
                            Sum = 1.08M
                        },
                        new ResponseCalculateContributionViewModelItem
                        {
                            MonthNumber = 2,
                            Income = 0.08M,
                            Sum = 1.16M
                        }
                    }
            };
        }

        private bool ElementDoesNotExist(IRenderedComponent<ContributionCalculatorTable> page, string element) 
        {
            try
            {
                page.Find(element).Should().NotBeNull();
                return false;
            }
            catch
            {
                return true;
            }
        }
    }
}
