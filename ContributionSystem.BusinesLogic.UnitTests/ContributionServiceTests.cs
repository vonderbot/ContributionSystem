using FluentAssertions;
using ContributionSystem.BusinesLogic.Services;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using NUnit.Framework;
using System;

namespace ContributionSystem.UnitTests
{
    public class ContributionServiceTests
    {
        private readonly ContributionService contributionService;

        public ContributionServiceTests()
        {
            contributionService = new ContributionService();
        }

        [Test]
        public void Calculate_ValidRequestWithSimpleCalculationMethod_ValidResponse()
        {
            //arrange
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, 1, 3, 100);
            var correctResponse = GetSimpleCalculkationResponse();

            //act
            var response = contributionService.Calculate(request);

            //assert
            response.Should().BeEquivalentTo(correctResponse);
        }

        [Test]
        public void Calculate_ValidRequestWithComplexCalculationMethod_ValidResponse()
        {
            //arrange
            var request = GetCalculationRequest(CalculationMethodEnumView.Complex, 1, 3, 100);
            var correctResponse = GetComplexCalculkationResponse();

            //act
            ResponseCalculateContributionViewModel response = contributionService.Calculate(request);
            
            //assert
            response.Should().BeEquivalentTo(correctResponse);
        }

        [Test]
        public void Calculate_ValidRequest_TypeResponseCalculateContributionViewModel()
        {
            //arrange
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, 1, 1, 1);

            //act
            var response = contributionService.Calculate(request);

            //assert
            response.Should().BeOfType<ResponseCalculateContributionViewModel>();
        }

        [Test]
        [TestCase(0, 3, 1)]
        [TestCase(-1, 3, 1)]
        public void Calculate_RequestWithZeroOrNegativeStartValue_ThrowException(decimal startValue, int term, decimal percent)
        {
            //arrange
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, startValue, term, percent);

            //act
            Action act = () => contributionService.Calculate(request);

            //assert
            act.Should().Throw<Exception>()
               .WithMessage("Incorect start value in request");
        }

        [Test]
        [TestCase(1, 0, 1)]
        [TestCase(1, -1, 1)]
        public void Calculate_RequestWithZeroOrNegativeTerm_ThrowException(decimal startValue, int term, decimal percent)
        {
            //arrange
             var request = GetCalculationRequest(CalculationMethodEnumView.Simple, startValue, term, percent);

            //act
            Action act = () => contributionService.Calculate(request);

            //assert
            act.Should().Throw<Exception>()
               .WithMessage("Incorect term in request");
        }
        
        [Test]
        [TestCase(1, 1, 0)]
        [TestCase(1, 1, -1)]
        public void Calculate_RequestWithZeroOrNegativePercent_ThrowException(decimal startValue, int term, decimal percent)
        {
            //arrange
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, startValue, term, percent);

            //act
            Action act = () => contributionService.Calculate(request);

            //assert
            act.Should().Throw<Exception>()
                .WithMessage("Incorect percent in request");
        }

        [Test]
        public void Calculate_NullRequest_ThrowException()
        {
            //arrange
            RequestCalculateContributionViewModel request = null;

            //act
            Action act = () => contributionService.Calculate(request);

            //assert
            act.Should().Throw<Exception>().WithMessage("Null request");
        }

        private RequestCalculateContributionViewModel GetCalculationRequest(CalculationMethodEnumView calculationMethod, decimal startValue, int term, decimal percent)
        {
            var request = new RequestCalculateContributionViewModel
            {
                CalculationMethod = calculationMethod,
                StartValue = startValue,
                Term = term,
                Percent = percent
            };

            return request;
        }

        private ResponseCalculateContributionViewModel GetSimpleCalculkationResponse()
        {
            var correctResponse = new ResponseCalculateContributionViewModel
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

            return correctResponse;
        }

        private ResponseCalculateContributionViewModel GetComplexCalculkationResponse()
        {
            var correctResponse = new ResponseCalculateContributionViewModel
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

            return correctResponse;
        }
    }
}