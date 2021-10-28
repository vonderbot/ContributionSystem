using FluentAssertions;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using NUnit.Framework;
using System;
using ContributionSystem.BusinessLogic.Services;
using System.Collections.Generic;
using ContributionSystem.DataAccess.Repositories;
using ContributionSystem.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ContributionSystem.BusinesLogic.UnitTests.Services
{
    public class ContributionServiceTests
    {
        private readonly ContributionService _contributionService;
        private const int CorrectStartValue = 1;
        private const int CorrectTerm = 3;
        private const int CorrectPercent = 100;

        public ContributionServiceTests()
        {
            _contributionService = new ContributionService(new ContributionRepository(new ContributionDbContext(new DbContextOptions<ContributionDbContext>())));
        }

        [Test]
        public void Calculate_ValidRequestWithSimpleCalculationMethod_ValidResponse()
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, CorrectStartValue, CorrectTerm, CorrectPercent);
            var correctResponse = GetSimpleCalculationResponse();
            var response = _contributionService.Calculate(request);
            response.Should().BeEquivalentTo(correctResponse);
        }

        [Test]
        public void Calculate_ValidRequestWithComplexCalculationMethod_ValidResponse()
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Complex, CorrectStartValue, CorrectTerm, CorrectPercent);
            var correctResponse = GetComplexCalculationResponse();
            var response = _contributionService.Calculate(request);
            response.Should().BeEquivalentTo(correctResponse);
        }

        [Test]
        public void Calculate_ValidRequest_TypeResponseCalculateContributionViewModel()
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, CorrectStartValue, CorrectTerm, CorrectPercent);
            var response = _contributionService.Calculate(request);
            response.Should().BeOfType<ResponseCalculateContributionViewModel>();
        }

        //[Test]
        //[TestCase(0, 3, 1)]
        //[TestCase(-1, 3, 1)]
        //public void Calculate_RequestWithZeroOrNegativeStartValue_ThrowException(decimal startValue, int term, decimal percent)
        //{
        //    var request = GetCalculationRequest(CalculationMethodEnumView.Simple, startValue, term, percent);
        //    Action act = () => _contributionService.Calculate(request);
        //    act.Should().Throw<Exception>()
        //       .WithMessage("Incorrect start value in request");
        //}

        //[Test]
        //[TestCase(1, 0, 1)]
        //[TestCase(1, -1, 1)]
        //public void Calculate_RequestWithZeroOrNegativeTerm_ThrowException(decimal startValue, int term, decimal percent)
        //{
        //    var request = GetCalculationRequest(CalculationMethodEnumView.Simple, startValue, term, percent);
        //    Action act = () => _contributionService.Calculate(request);
        //    act.Should().Throw<Exception>()
        //       .WithMessage("Incorrect term in request");
        //}

        //[Test]
        //[TestCase(1, 1, 0)]
        //[TestCase(1, 1, -1)]
        //public void Calculate_RequestWithZeroOrNegativePercent_ThrowException(decimal startValue, int term, decimal percent)
        //{
        //    var request = GetCalculationRequest(CalculationMethodEnumView.Simple, startValue, term, percent);
        //    Action act = () => _contributionService.Calculate(request);
        //    act.Should().Throw<Exception>()
        //        .WithMessage("Incorrect percent in request");
        //}

        //[Test]
        //public void Calculate_NullRequest_ThrowException()
        //{
        //    Action act = () => _contributionService.Calculate(null);
        //    act.Should().Throw<Exception>().WithMessage("Null request");
        //}

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

        private ResponseCalculateContributionViewModel GetSimpleCalculationResponse()
        {
            var correctResponse = new ResponseCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,
                Items = new List<MonthsInfoContributionViewModelItem>
                {
                    new MonthsInfoContributionViewModelItem
                    {
                        MonthNumber = 1,
                        Income = 0.08M,
                        Sum = 1.08M
                    },
                    new MonthsInfoContributionViewModelItem
                    {
                        MonthNumber = 2,
                        Income = 0.09M,
                        Sum = 1.17M
                    },
                    new MonthsInfoContributionViewModelItem
                    {
                        MonthNumber = 3,
                        Income = 0.08M,
                        Sum = 1.25M
                    }
                }
            };

            return correctResponse;
        }

        private ResponseCalculateContributionViewModel GetComplexCalculationResponse()
        {
            var correctResponse = new ResponseCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Complex,
                Items = new List<MonthsInfoContributionViewModelItem>
                {
                    new MonthsInfoContributionViewModelItem
                    {
                        MonthNumber = 1,
                        Income = 0.08M,
                        Sum = 1.08M
                    },
                    new MonthsInfoContributionViewModelItem
                    {
                        MonthNumber = 2,
                        Income = 0.09M,
                        Sum = 1.17M
                    },
                    new MonthsInfoContributionViewModelItem
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