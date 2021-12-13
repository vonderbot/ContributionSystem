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
        private const string UserId = "23";

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
                .GetByUserId(It.IsAny<int>(), It.Is<int>(p => p >= TotalNumberOfRecords), It.Is<string>(p => p == UserId)))
                .ReturnsAsync(contributionList);
            contributionList.Add(contribution);
            mock.Setup(repo => repo
                .GetByUserId(It.Is<int>(p => p >= TotalNumberOfRecords), It.Is<int>(p => p < TotalNumberOfRecords), It.Is<string>(p => p == UserId)))
                .ReturnsAsync(contributionList);
            mock.Setup(repo => repo
                .GetNumberOfRecords())
                .ReturnsAsync(TotalNumberOfRecords);
            mock.Setup(repo => repo
                .GetNumberOfUserRecords(It.Is<string>(p => p == UserId)))
                .ReturnsAsync(TotalNumberOfRecords);
            _contributionService = new ContributionService(mock.Object);
        }

        [Test]
        public async Task GetHistory_NullRequest_ThrowException()
        {
            Func<Task> act = async () => await _contributionService.GetHistoryByUserId(null);

            await act.Should().ThrowAsync<Exception>()
               .WithMessage("Null request");
        }

        [Test]
        public async Task GetHistory_RequestWithInvalidTake_ThrowException()
        {
            var request = GetGetHistoryRequest(InvalidTake, Skip, UserId);
            Func<Task> act = async () => await _contributionService.GetHistoryByUserId(request);

            await act.Should().ThrowAsync<Exception>()
               .WithMessage("Attempt to take an invalid amount of contributions");
        }

        [Test]
        public async Task GetHistory_RequestWithInvalidSkip_ThrowException()
        {
            var request = GetGetHistoryRequest(Take, InvalidSkip, UserId);
            Func<Task> act = async () => await _contributionService.GetHistoryByUserId(request);

            await act.Should().ThrowAsync<Exception>()
               .WithMessage("Attempt to skip an invalid amount of contributions");
        }

        [Test]
        public async Task GetHistory_ValidRequest_ValidResponse()
        {
            var request = GetGetHistoryRequest(Take, Skip, UserId);
            var contributionList = new List<Contribution>
            {
                GetContribution(
                    GetCalculationRequest(CalculationMethodEnumView.Simple, CorrectStartValue, CorrectTerm,
                        CorrectPercent), GetSimpleCalculationResponse())
            };
            var correctResponse = GetGetHistoryResponse(request, contributionList);
            var response = await _contributionService.GetHistoryByUserId(request);

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
        
        [TestCase(0, 3, 1)]
        [TestCase(-1, 3, 1)]
        public async Task Calculate_RequestWithZeroOrNegativeStartValue_ThrowException(decimal startValue, int term, decimal percent)
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, startValue, term, percent);
            Func<Task> act = async () => await _contributionService.Calculate(request);

            await act.Should().ThrowAsync<Exception>()
               .WithMessage("Incorrect start value in request");
        }
        
        [TestCase(1, 0, 1)]
        [TestCase(1, -1, 1)]
        public async Task Calculate_RequestWithZeroOrNegativeTerm_ThrowException(decimal startValue, int term, decimal percent)
        {
            var request = GetCalculationRequest(CalculationMethodEnumView.Simple, startValue, term, percent);
            Func<Task> act = async () => await _contributionService.Calculate(request);

            await act.Should().ThrowAsync<Exception>()
               .WithMessage("Incorrect term in request");
        }
        
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

        private RequestGetHistoryByUserIdContributionViewModel GetGetHistoryRequest(int take, int skip, string userId)
        {
            return new RequestGetHistoryByUserIdContributionViewModel
            {
                UserId = userId,
                Take = take,
                Skip = skip
            };
        }
        private ResponseGetHistoryByUserIdContributionViewModel GetGetHistoryResponse(RequestGetHistoryByUserIdContributionViewModel request, List<Contribution> contributions)
        {
            var items = contributions.Select(u => new ResponseGetUsersListContributionViewModelItems
            {
                Percent = u.Percent,
                Term = u.Term,
                Sum = u.StartValue,
                Date = u.Date,
                Id = u.Id
            });
            var response = new ResponseGetHistoryByUserIdContributionViewModel
            {
                UserId = request.UserId,
                Items = items,
                TotalNumberOfUserRecords = TotalNumberOfRecords,
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