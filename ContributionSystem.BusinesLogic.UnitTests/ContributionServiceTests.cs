using FluentAssertions;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using NUnit.Framework;
using System;
using ContributionSystem.BusinessLogic.Services;

namespace ContributionSystem.UnitTests
{
    public class ContributionServiceTests
    {
        private readonly ContributionService _contributionService;
        private const int _ñorrectStartValue = 1;
        private const int _ñorrectTerm = 3;
        private const int _ñorrectPercent = 100;

        public ContributionServiceTests()
        {
            _contributionService = new ContributionService();
        }

        [Test]
        public void Calculate_ValidRequestWithSimpleCalculationMethod_ValidResponse()
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, _ñorrectStartValue, _ñorrectTerm, _ñorrectPercent);
            var correctResponse = GetSimpleCalculkationResponse();
            var response = _contributionService.Calculate(request);
            response.Should().BeEquivalentTo(correctResponse);
        }

        [Test]
        public void Calculate_ValidRequestWithComplexCalculationMethod_ValidResponse()
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Complex, _ñorrectStartValue, _ñorrectTerm, _ñorrectPercent);
            var correctResponse = GetComplexCalculkationResponse();
            var response = _contributionService.Calculate(request);
            response.Should().BeEquivalentTo(correctResponse);
        }

        [Test]
        public void Calculate_ValidRequest_TypeResponseCalculateContributionViewModel()
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, _ñorrectStartValue, _ñorrectTerm, _ñorrectPercent);
            var response = _contributionService.Calculate(request);
            response.Should().BeOfType<ResponseCalculateContributionViewModel>();
        }

        [Test]
        [TestCase(0, 3, 1)]
        [TestCase(-1, 3, 1)]
        public void Calculate_RequestWithZeroOrNegativeStartValue_ThrowException(decimal startValue, int term, decimal percent)
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, startValue, term, percent);
            Action act = () => _contributionService.Calculate(request);
            act.Should().Throw<Exception>()
               .WithMessage("Incorect start value in request");
        }

        [Test]
        [TestCase(1, 0, 1)]
        [TestCase(1, -1, 1)]
        public void Calculate_RequestWithZeroOrNegativeTerm_ThrowException(decimal startValue, int term, decimal percent)
        {
             var request = GetCalculationRequest(CalculationMethodEnumView.Simple, startValue, term, percent);
            Action act = () => _contributionService.Calculate(request);
            act.Should().Throw<Exception>()
               .WithMessage("Incorect term in request");
        }
        
        [Test]
        [TestCase(1, 1, 0)]
        [TestCase(1, 1, -1)]
        public void Calculate_RequestWithZeroOrNegativePercent_ThrowException(decimal startValue, int term, decimal percent)
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, startValue, term, percent);
            Action act = () => _contributionService.Calculate(request);
            act.Should().Throw<Exception>()
                .WithMessage("Incorect percent in request");
        }

        [Test]
        public void Calculate_NullRequest_ThrowException()
        {
            RequestCalculateContributionViewModel request = null;
            Action act = () => _contributionService.Calculate(request);
            act.Should().Throw<Exception>().WithMessage("Null request");
        }

        private static RequestCalculateContributionViewModel GetCalculationRequest(CalculationMethodEnumView calculationMethod, decimal startValue, int term, decimal percent)
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

        private static ResponseCalculateContributionViewModel GetSimpleCalculkationResponse()
        {
            var correctResponse = new ResponseCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,

                Items = new ResponseCalculateContributionViewModelItem[_ñorrectTerm]
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

        private static ResponseCalculateContributionViewModel GetComplexCalculkationResponse()
        {
            var correctResponse = new ResponseCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Complex,

                Items = new ResponseCalculateContributionViewModelItem[_ñorrectTerm]
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