using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.BusinessLogic.Interfaces;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ContributionSystem.BusinessLogic.Services
{
    /// <inheritdoc/>
    public class ContributionService : IContributionService
    {
        private const int Hundred = 100;
        private const int NumberOfMonthsInAYear = 12;
        private const int NumberOfDigitsAfterDecimalPoint = 2;

        private readonly IContributionRepository _contributionRepository;

        /// <summary>
        /// ContributionService constructor.
        /// </summary>
        /// <param name="contributionRepository">IContributionRepository instance.</param>
        public ContributionService(IContributionRepository contributionRepository)
        {
            _contributionRepository = contributionRepository;
        }

        /// <inheritdoc/>
        public async Task<ResponseGetDetailsByIdContributionViewModel> GetDetailsById(int id)
        {
            var contribution = await _contributionRepository.GetById(id);
            var response = new ResponseGetDetailsByIdContributionViewModel
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

            return response;
        }

        /// <inheritdoc/>
        public async Task<ResponseGetHistoryByUserIdContributionViewModel> GetHistoryByUserId(RequestGetHistoryByUserIdContributionViewModel request)
        {
            CheckGetHistoryByUserIdRequest(request);
            var contributions = await _contributionRepository.GetByUserId(request.Take, request.Skip, request.UserId);
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
                TotalNumberOfUserRecords = await _contributionRepository.GetNumberOfUserRecords(request.UserId), 
                Take = request.Take,
                Skip = request.Skip
            };

            return response;
        }

        /// <inheritdoc/>
        public async Task<ResponseCalculateContributionViewModel> Calculate(RequestCalculateContributionViewModel request)
        {
            CheckCalculationRequest(request);
            var contribution = new Contribution()
            {
                StartValue = request.StartValue,
                Term = request.Term,
                Percent = request.Percent
            };
            var allMonthsInfo = new List<MonthsInfoContributionViewModelItem>();
            switch (request.CalculationMethod)
            {
                case CalculationMethodEnumView.Simple:
                    SimpleCalculate(contribution, allMonthsInfo);
                    break;

                case CalculationMethodEnumView.Complex:
                    ComplexCalculate(contribution, allMonthsInfo);
                    break;

                default:
                    throw new Exception("Incorrect calculation method");
            }
            var response = new ResponseCalculateContributionViewModel()
            {
                CalculationMethod = request.CalculationMethod,
                Items = allMonthsInfo
            };
            await AddContribution(request, response.Items);

            return response;
        }

        private void CheckGetHistoryByUserIdRequest(RequestGetHistoryByUserIdContributionViewModel request)
        {
            if (request == null)
            {
                throw new Exception("Null request");
            }
            else if (request.Take < 1)
            {
                throw new Exception("Attempt to take an invalid amount of contributions");
            }
            else if (request.Skip < 0)
            {
                throw new Exception("Attempt to skip an invalid amount of contributions");
            }
            else if (string.IsNullOrEmpty(request.UserId))
            {
                throw new Exception("Attempt to get history without user id");
            }
        }

        private async Task AddContribution(RequestCalculateContributionViewModel request, IEnumerable<MonthsInfoContributionViewModelItem> responseItems)
        {
            var monthsInfo = responseItems.Select(u => new MonthInfo
            {
                MonthNumber = u.MonthNumber,
                Income = u.Income,
                Sum = u.Sum
            }).ToList();
            var contribution = new Contribution()
            {
                UserId = request.UserId,
                StartValue = request.StartValue,
                Term = request.Term,
                Percent = request.Percent,
                Date = DateTime.UtcNow.Date.ToShortDateString(),
                CalculationMethod = (CalculationMethodEnum)(int)request.CalculationMethod,
                Details = monthsInfo
            };
            await _contributionRepository.Create(contribution);
            await _contributionRepository.Save();
        }

        private void CheckCalculationRequest(RequestCalculateContributionViewModel request)
        {
            if (request == null)
            {
                throw new Exception("Null request");
            }
            else if (request.StartValue <= 0)
            {
                throw new Exception("Incorrect start value in request");
            }
            else if(request.Term <= 0)
            {
                throw new Exception("Incorrect term in request");
            }
            else if(request.Percent <= 0)
            {
                throw new Exception("Incorrect percent in request");
            }
        }

        private void SimpleCalculate(Contribution contribution, IList<MonthsInfoContributionViewModelItem> allMonthsInfo)
        {
            var income = contribution.StartValue / Hundred * (contribution.Percent / NumberOfMonthsInAYear);

            for (var i = 0; i < contribution.Term; i++)
            {
                var monthInfo = new MonthsInfoContributionViewModelItem()
                {
                    MonthNumber = i + 1,
                    Income = income,
                    Sum = Math.Round(contribution.StartValue + income * (i + 1), NumberOfDigitsAfterDecimalPoint)
                };
                RoundingMistakeCheck(monthInfo, income, ChoosePreviousElement(allMonthsInfo, contribution, i));
                allMonthsInfo.Add(monthInfo);
            }
        }

        private void ComplexCalculate(Contribution contribution, IList<MonthsInfoContributionViewModelItem> allMonthsInfo)
        {
            for (var i = 0; i < contribution.Term; i++)
            {
                var monthInfo = new MonthsInfoContributionViewModelItem { MonthNumber = i + 1 };
                var income = ComplexIncomeAndSumCalculating(contribution, monthInfo, ChoosePreviousElement(allMonthsInfo, contribution, i));
                RoundingMistakeCheck(monthInfo, income, ChoosePreviousElement(allMonthsInfo, contribution, i));
                allMonthsInfo.Add(monthInfo);
            }
            for (int i = 0; i < contribution.Term; i++)
            {
                allMonthsInfo[i].Sum = Math.Round(allMonthsInfo[i].Sum, NumberOfDigitsAfterDecimalPoint);
            }
        }

        private decimal ChoosePreviousElement(IList<MonthsInfoContributionViewModelItem> allMonthsInfo, Contribution contribution, int index)
        {
            if (index != 0)
            {
                return allMonthsInfo[index - 1].Sum;
            }
            else
            {
                return contribution.StartValue;
            }
        }

        private void RoundingMistakeCheck(MonthsInfoContributionViewModelItem monthInfo, decimal income, decimal previousElement)
        {
            if (Math.Round(monthInfo.Sum, NumberOfDigitsAfterDecimalPoint) - Math.Round(previousElement, NumberOfDigitsAfterDecimalPoint) != Math.Round(income, NumberOfDigitsAfterDecimalPoint))
            {
                monthInfo.Income = Math.Round(monthInfo.Sum, NumberOfDigitsAfterDecimalPoint) - Math.Round(previousElement, NumberOfDigitsAfterDecimalPoint);
            }
            else
            {
                monthInfo.Income = Math.Round(monthInfo.Income, NumberOfDigitsAfterDecimalPoint);
            }
        }

        private decimal ComplexIncomeAndSumCalculating(Contribution contribution, MonthsInfoContributionViewModelItem monthInfo, decimal previousElement)
        {
            var income = previousElement / Hundred * (contribution.Percent / NumberOfMonthsInAYear);
            monthInfo.Income = income;
            monthInfo.Sum = previousElement + income;

            return income;
        }
    }
}
