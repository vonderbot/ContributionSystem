using ContributionSystem.Entities.Entities;
using System;
using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.BusinessLogic.Interfaces;
using System.Collections.Generic;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Enums;

namespace ContributionSystem.BusinessLogic.Services
{
    public class ContributionService : IContributionService
    {
        private const int Hundred = 100;
        private const int NumberOfMonthsInAYear = 12;
        private const int NumberOfDigitsAfterDecimalPoint = 2;
        private readonly IContributionRepository _contributionRepository;
        private readonly IMonthInfoRepository _monthInfoRepository;

        public ContributionService(IContributionRepository contributionRepository, IMonthInfoRepository monthInfoRepository)
        {
            _contributionRepository = contributionRepository;
            _monthInfoRepository = monthInfoRepository;
        }

        public ResponseCalculateContributionViewModel Calculate(RequestCalculateContributionViewModel request)
        {
            CheckRequest(request);
            var contribution = new Contribution() 
            {
                StartValue = request.StartValue,
                Term = request.Term,
                Percent = request.Percent
            };
            var allMonthsInfo = new List<ResponseCalculateContributionViewModelItem>();
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

            return new ResponseCalculateContributionViewModel()
            {
                CalculationMethod = request.CalculationMethod,
                Items = allMonthsInfo
            };
        }

        public IEnumerable<RequestCalculateContributionViewModel> GetRequestsHistory(RequestGetRequestsHistoryContrbutionViewModel request)
        {
            var contributions = _contributionRepository.GetContributions(request.NumberOfContrbutionsForLoad, request.NumberOfContrbutionsForSkip);
            var requests = new List<RequestCalculateContributionViewModel>();

            foreach (var element in contributions)
            {
                requests.Add(CreateRequestFromContribution(element));
            }

            return requests;
        }

        public void AddContribution(RequestCalculateContributionViewModel request, IEnumerable<ResponseCalculateContributionViewModelItem> responseItems)
        {
            var contribution = CreateContributionFromRequest(request);
            var monthsInfo = new List<MonthInfo>();

            foreach (var element in responseItems)
            {
                monthsInfo.Add(CreateMonthInfoFromResponseItem(element, contribution));
            }
            _contributionRepository.Create(contribution);
            _monthInfoRepository.Create(monthsInfo);
        }

        private MonthInfo CreateMonthInfoFromResponseItem(ResponseCalculateContributionViewModelItem responseItems, Contribution contribution)
        {
            return new MonthInfo()
            {
                MonthNumber = responseItems.MonthNumber,
                Income = responseItems.Income,
                Sum = responseItems.Sum,
                Contribution = contribution
            };
        }

        private Contribution CreateContributionFromRequest(RequestCalculateContributionViewModel request)
        {
            return new Contribution()
            {
                StartValue = request.StartValue,
                Term = request.Term,
                Percent = request.Percent,
                Date = request.Date,
                CalculationMethod = (CalculationMethodEnum)(int)request.CalculationMethod
            };
        }

        private RequestCalculateContributionViewModel CreateRequestFromContribution(Contribution contribution)
        {
            return new RequestCalculateContributionViewModel()
            {
                StartValue = contribution.StartValue,
                Term = contribution.Term,
                Percent = contribution.Percent,
                Date = contribution.Date,
                CalculationMethod = (CalculationMethodEnumView)(int)contribution.CalculationMethod
            };
        }

        private void CheckRequest(RequestCalculateContributionViewModel request)
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

        private void SimpleCalculate(Contribution contribution, List<ResponseCalculateContributionViewModelItem> allMonthsInfo)
        {
            var income = contribution.StartValue / Hundred * (contribution.Percent / NumberOfMonthsInAYear);

            for (var i = 0; i < contribution.Term; i++)
            {
                var monthInfo = new ResponseCalculateContributionViewModelItem()
                {
                    MonthNumber = i + 1,
                    Income = income,
                    Sum = Math.Round(contribution.StartValue + income * (i + 1), NumberOfDigitsAfterDecimalPoint)
                };
                RoundingMistakeCheck(monthInfo, income, ChoosePreviousElement(allMonthsInfo, contribution, i));
                allMonthsInfo.Add(monthInfo);
            }
        }

        private void ComplexCalculate(Contribution contribution, List<ResponseCalculateContributionViewModelItem> allMonthsInfo)
        {
            for (var i = 0; i < contribution.Term; i++)
            {
                var monthInfo = new ResponseCalculateContributionViewModelItem();
                monthInfo.MonthNumber = i + 1;
                decimal income = ComplexIncomeAndSumCalculating(contribution, monthInfo, ChoosePreviousElement(allMonthsInfo, contribution, i));
                RoundingMistakeCheck(monthInfo, income, ChoosePreviousElement(allMonthsInfo, contribution, i));
                allMonthsInfo.Add(monthInfo);
            }
            for (int i = 0; i < contribution.Term; i++)
            {
                allMonthsInfo[i].Sum = Math.Round(allMonthsInfo[i].Sum, NumberOfDigitsAfterDecimalPoint);
            }
        }

        private decimal ChoosePreviousElement(List<ResponseCalculateContributionViewModelItem> allMonthsInfo, Contribution contribution, int index)
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

        private void RoundingMistakeCheck(ResponseCalculateContributionViewModelItem monthInfo, decimal income, decimal previousElement)
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

        private decimal ComplexIncomeAndSumCalculating(Contribution contribution, ResponseCalculateContributionViewModelItem monthInfo, decimal previousElement)
        {
            var income = previousElement / Hundred * (contribution.Percent / NumberOfMonthsInAYear);
            monthInfo.Income = income;
            monthInfo.Sum = previousElement + income;

            return income;
        }
    }
}
