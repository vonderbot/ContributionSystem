using FluentAssertions;
using ContributionSystem.BusinesLogic.Services;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using NUnit.Framework;
using System;
using ContributionSystem.BusinesLogic.Interfaces;

namespace ContributionSystem.UnitTests
{
    public class ContributionServiceTests
    {
        private IContributionService contributionService;

        public ContributionServiceTests()
        {
            contributionService = new ContributionService();
        }

        [Test]
        public void Calculate_ValidRequestWithSimpleCalculationMethod_ValidResponse()
        {
            //arrange
            var request = new RequestCalculateContributionViewModel 
            { 
                CalculationMethod = CalculationMethodEnumView.Simple,

                StartValue = 1,

                Term = 3,

                Percent = 100
            };
            var correctResponce = new ResponseCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,

                Items = new ResponseCalculateContributionViewModelItem[3]
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
                        Income = 0.09M,
                        Sum = 1.17M
                    },
                    new ResponseCalculateContributionViewModelItem
                    {
                        MonthNumber = 3,
                        Income = 0.08M,
                        Sum = 1.25M
                    }
                }
            };

            //act
            var response = contributionService.Calculate(request);

            //assert
            response.Should().BeEquivalentTo(correctResponce);
        }

        [Test]
        public void Calculate_ValidRequestWithComplexCalculationMethod_ValidResponse()
        {
            //arrange
            var request = new RequestCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Complex,

                StartValue = 1,

                Term = 3,

                Percent = 100
            };
            var correctResponce = new ResponseCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Complex,

                Items = new ResponseCalculateContributionViewModelItem[3]
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
                        Income = 0.09M,
                        Sum = 1.17M
                    },
                    new ResponseCalculateContributionViewModelItem
                    {
                        MonthNumber = 3,
                        Income = 0.10M,
                        Sum = 1.27M
                    }
                }
            };

            //act
            ResponseCalculateContributionViewModel response = contributionService.Calculate(request);
            
            //assert
            response.Should().BeEquivalentTo(correctResponce);
        }

        [Test]
        public void Calculate_ValidRequest_TypeResponseCalculateContributionViewModel()
        {
            //arrange
            var request = new RequestCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,

                StartValue = 1,

                Term = 1,

                Percent = 1
            };

            //act
            var response = contributionService.Calculate(request);

            //assert
            response.Should().BeOfType<ResponseCalculateContributionViewModel>();
        }

        [Test]
        public void Calculate_RequestWithZeroOrNegativeStartValue_ThrowException()
        {
            //arrange
            var request1 = new RequestCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,

                StartValue = 0,

                Term = 3,

                Percent = 1
            };
            var request2 = new RequestCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,

                StartValue = 0,

                Term = 3,

                Percent = 1
            };

            //act
            Action act1 = () => contributionService.Calculate(request1);
            Action act2 = () => contributionService.Calculate(request2);

            //assert
            act1.Should().Throw<Exception>()
                .WithMessage("Incorect start value in request");
            act2.Should().Throw<Exception>()
                .WithMessage("Incorect start value in request");
        }

        [Test]
        public void Calculate_RequestWithZeroOrNegativeTerm_ThrowException()
        {
            //arrange
            var request1 = new RequestCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,

                StartValue = 1,

                Term = 0,

                Percent = 100
            };
            var request2 = new RequestCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,

                StartValue = 1,

                Term = -1,

                Percent = 100
            };

            //act
            Action act1 = () => contributionService.Calculate(request1);
            Action act2 = () => contributionService.Calculate(request2);

            //assert
            act1.Should().Throw<Exception>()
                .WithMessage("Incorect term in request");
            act2.Should().Throw<Exception>()
                .WithMessage("Incorect term in request");
        }
        
        [Test]
        public void Calculate_RequestWithZeroOrNegativePercent_ThrowException()
        {
            //arrange
            var request1 = new RequestCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,

                StartValue = 1,

                Term = 1,

                Percent = 0
            };
            var request2 = new RequestCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,

                StartValue = 1,

                Term = 1,

                Percent = -1
            };

            //act
            Action act1 = () => contributionService.Calculate(request1);
            Action act2 = () => contributionService.Calculate(request2);

            //assert
            act1.Should().Throw<Exception>()
                .WithMessage("Incorect percent in request");
            act2.Should().Throw<Exception>()
                .WithMessage("Incorect percent in request");
        }

        [Test]
        public void Calculatå_NullRequest_ThrowException()
        {
            //arrange
            RequestCalculateContributionViewModel request = null;

            //act
            Action act = () => contributionService.Calculate(request);

            //assert
            act.Should().Throw<Exception>().WithMessage("Null request");
        }
    }
}