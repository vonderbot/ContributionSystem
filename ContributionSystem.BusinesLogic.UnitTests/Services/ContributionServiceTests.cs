using FluentAssertions;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using NUnit.Framework;
using System;
using ContributionSystem.BusinessLogic.Services;
using System.Collections.Generic;
using Moq;
using ContributionSystem.DataAccess.Interfaces;
using System.Threading.Tasks;
using ContributionSystem.Entities.Entities;
using ContributionSystem.Entities.Enums;
using System.Linq;

namespace ContributionSystem.BusinesLogic.UnitTests.Services
{
    public class ContributionServiceTests
    {
        private readonly ContributionService _contributionService;

        private const int Take = 1;
        private const int Skip = 0;
        private const int InvalidTake = 0;
        private const int InvalidSkip = -1;
        private const int TotalNumberOfRecords = 1;
        private const int CorrectStartValue = 1;
        private const int CorrectTerm = 3;
        private const int CorrectPercent = 100;
        private const int ValidId = 1;
        private const int InvalidId = 0;

        public ContributionServiceTests()
        {
            var mock = new Mock<IContributionRepository>();
            var contribution = GetContribution(GetCalculationRequest(CalculationMethodEnumView.Simple, CorrectStartValue, CorrectTerm, CorrectPercent), GetSimpleCalculationResponse());
            mock.Setup(repo => repo
                .GetById(It.Is<int>(p => p == ValidId)))
                .ReturnsAsync(contribution);
            mock.Setup(repo => repo
                .GetById(It.Is<int>(p => p != ValidId)))
                .ThrowsAsync(new Exception("Can't find contribution"));
            var contributionList = new List<Contribution>();
            mock.Setup(repo => repo
                .Get(It.Is<int>(p => p <= 0), It.Is<int>(p => p >= 0)))
                .ThrowsAsync(new Exception("The number of rows provided for a FETCH clause must be greater then zero."));
            mock.Setup(repo => repo
                .Get(It.IsAny<int>(), It.Is<int>(p => p < 0)))
                .ThrowsAsync(new Exception("The offset specified in a OFFSET clause may not be negative"));
            mock.Setup(repo => repo
                .Get(It.Is<int>(p => p > InvalidTake), It.Is<int>(p => p >= TotalNumberOfRecords)))
                .ReturnsAsync(contributionList);
            contributionList.Add(contribution);
            mock.Setup(repo => repo
                .Get(It.Is<int>(p => p >= TotalNumberOfRecords), It.Is<int>(p => p < TotalNumberOfRecords && p >= 0)))
                .ReturnsAsync(contributionList);
            mock.Setup(repo => repo
                .GetNumberOfRecords())
                .ReturnsAsync(1);
            _contributionService = new ContributionService(mock.Object);
        }

        [Test]
        public async Task GetHistory_NullRequest_ThrowException()
        {
            RequestGetHistoryContributionViewModel request = null;
            Func<Task> act = async () => await _contributionService.GetHistory(request);
            await act.Should().ThrowAsync<Exception>()
               .WithMessage("Null request");
        }

        [Test]
        public async Task GetHistory_InvalidTakeInRequest_ThrowException()
        {
            var request = new RequestGetHistoryContributionViewModel
            {
                Take = InvalidTake,
                Skip = Skip
            };
            Func<Task> act = async () => await _contributionService.GetHistory(request);
            await act.Should().ThrowAsync<Exception>()
               .WithMessage("The number of rows provided for a FETCH clause must be greater then zero.");
        }

        [Test]
        public async Task GetHistory_InvalidSkipInRequest_ThrowException()
        {
            var request = new RequestGetHistoryContributionViewModel
            {
                Take = Take,
                Skip = InvalidSkip
            };
            Func<Task> act = async () => await _contributionService.GetHistory(request);
            await act.Should().ThrowAsync<Exception>()
               .WithMessage("The offset specified in a OFFSET clause may not be negative");
        }

        [Test]
        public async Task GetHistory_ValidRequest_ValidResponse()
        {
            var request = new RequestGetHistoryContributionViewModel
            {
                Take = Take,
                Skip = Skip
            };
            var contributionList = new List<Contribution>();
            contributionList.Add(GetContribution(GetCalculationRequest(CalculationMethodEnumView.Simple, CorrectStartValue, CorrectTerm, CorrectPercent), GetSimpleCalculationResponse()));
            var correctResponse = GetGetHistoryResponse(request, contributionList);
            var response = await _contributionService.GetHistory(request);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(correctResponse);
        }

        [Test]
        public async Task GetDetailsById_InvalidId_ThrowException()
        {
            Func<Task> act = async () => await _contributionService.GetDetailsById(InvalidId);
            await act.Should().ThrowAsync<Exception>()
               .WithMessage("Can't find contribution");
        }

        [Test]
        public async Task GetDetailsById_ValidId_ValidResponse()
        {
            var correctResponse = GetGetDetailsByIdResponse(
                GetContribution(
                    GetCalculationRequest(CalculationMethodEnumView.Simple, CorrectStartValue, CorrectTerm, CorrectPercent), GetSimpleCalculationResponse()));
            var response = await _contributionService.GetDetailsById(ValidId);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(correctResponse);
        }

        [Test]
        public async Task Calculate_ValidRequestWithSimpleCalculationMethod_ValidResponse()
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, CorrectStartValue, CorrectTerm, CorrectPercent);
            var correctResponse = GetSimpleCalculationResponse();
            var response = await _contributionService.Calculate(request);
            response.Should().BeEquivalentTo(correctResponse);
        }

        [Test]
        public async Task Calculate_ValidRequestWithComplexCalculationMethod_ValidResponse()
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Complex, CorrectStartValue, CorrectTerm, CorrectPercent);
            var correctResponse = GetComplexCalculationResponse();
            var response = await _contributionService.Calculate(request);
            response.Should().BeEquivalentTo(correctResponse);
        }

        [Test]
        public async Task Calculate_ValidRequest_TypeResponseCalculateContributionViewModel()
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, CorrectStartValue, CorrectTerm, CorrectPercent);
            var response = await _contributionService.Calculate(request);
            response.Should().BeOfType<ResponseCalculateContributionViewModel>();
        }

        [Test]
        [TestCase(0, 3, 1)]
        [TestCase(-1, 3, 1)]
        public async Task Calculate_RequestWithZeroOrNegativeStartValue_ThrowException(decimal startValue, int term, decimal percent)
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, startValue, term, percent);
            Func<Task> act = async () => await _contributionService.Calculate(request);
            await act.Should().ThrowAsync<Exception>()
               .WithMessage("Incorrect start value in request");
        }

        [Test]
        [TestCase(1, 0, 1)]
        [TestCase(1, -1, 1)]
        public async Task Calculate_RequestWithZeroOrNegativeTerm_ThrowException(decimal startValue, int term, decimal percent)
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, startValue, term, percent);
            Func<Task> act = async () => await _contributionService.Calculate(request);
            await act.Should().ThrowAsync<Exception>()
               .WithMessage("Incorrect term in request");
        }

        [Test]
        [TestCase(1, 1, 0)]
        [TestCase(1, 1, -1)]
        public async Task Calculate_RequestWithZeroOrNegativePercent_ThrowException(decimal startValue, int term, decimal percent)
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, startValue, term, percent);
            Func<Task> act = async () => await _contributionService.Calculate(request);
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Incorrect percent in request");
        }

        [Test]
        public async Task Calculate_NullRequest_ThrowException()
        {
            Func<Task> act = async () => await _contributionService.Calculate(null);
            await act.Should().ThrowAsync<Exception>().WithMessage("Null request");
        }

        private ResponseGetHistoryContributionViewModel GetGetHistoryResponse(RequestGetHistoryContributionViewModel request, List<Contribution> contributions)
        {
            var items = contributions.Select(u => new ResponseGetHistoryContributionViewModelItem
            {
                Percent = u.Percent,
                Term = u.Term,
                Sum = u.StartValue,
                Date = u.Date,
                Id = u.Id
            });
            var response = new ResponseGetHistoryContributionViewModel
            {
                Items = items,
                TotalNumberOfRecords = TotalNumberOfRecords,
                Take = request.Take,
                Skip = request.Skip
            };

            return response;
        }

        private ResponseGetDetailsByIdContributionViewModel GetGetDetailsByIdResponse(Contribution contribution)
        {
            return new ResponseGetDetailsByIdContributionViewModel()
            {
                ContributionId = contribution.Id,
                Items = contribution.Details.Select(u => new ResponseGetDetailsByIdContributionViewModelItem
                {
                    Id = u.Id,
                    MonthNumber = u.MonthNumber,
                    Income = u.Income,
                    Sum = u.Sum
                }).ToList()
            };
        }

        private Contribution GetContribution(RequestCalculateContributionViewModel requestCalculationModel, ResponseCalculateContributionViewModel responseCalculationModel)
        {
            var monthsInfo = responseCalculationModel.Items.Select(u => new MonthInfo
            {
                MonthNumber = u.MonthNumber,
                Income = u.Income,
                Sum = u.Sum
            }).ToList();
            var contribution = new Contribution()
            {
                StartValue = requestCalculationModel.StartValue,
                Term = requestCalculationModel.Term,
                Percent = requestCalculationModel.Percent,
                Date = DateTime.UtcNow.Date.ToShortDateString(),
                CalculationMethod = (CalculationMethodEnum)(int)requestCalculationModel.CalculationMethod,
                Details = monthsInfo
            };
            return contribution;
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