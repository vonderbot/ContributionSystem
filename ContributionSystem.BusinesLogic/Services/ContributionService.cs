using ContributionSystem.BusinesLogic.Interfaces;
using ContributionSystem.Entities.Entities;
using System;
using ContributionSystem.ViewModels.Models.Contribution;

namespace ContributionSystem.BusinesLogic.Services
{
    public class ContributionService : IContributionService
    {
        private const int Hundred = 100;
        private const int NumberOfMonthsInAYear = 12;
        private const int NumberOfDigitsAfterDecimalPoint = 2;
        public ResponseCalculateContributionViewModel SimplCalculate(RequestCalculateContributionViewModel request)
        {
            var contribution = new Contribution(request.StartValue, request.Term, request.Percent);
            decimal income = contribution.StartValue / Hundred * (contribution.Percent / NumberOfMonthsInAYear);
            var allMonthsInfo = new ResponseCalculateContributionViewModelItem[contribution.Term];
            for (int i = 0; i < contribution.Term; i++)
            {
                var monthInfo = new ResponseCalculateContributionViewModelItem()
                {
                    MonthNumber = i + 1,
                    Income = income,
                    Sum = contribution.StartValue + income * (i + 1)
                };
                RoundingMistakeCheck(monthInfo, income, i, DecimalObjCalculating(allMonthsInfo, contribution, i));
                allMonthsInfo[i] = monthInfo;
            }

            return new ResponseCalculateContributionViewModel() { Items = allMonthsInfo };
        }

        public ResponseCalculateContributionViewModel ComplexCalculate(RequestCalculateContributionViewModel request)
        {
            var contribution = new Contribution(request.StartValue, request.Term, request.Percent);
            var allMonthsInfo = new ResponseCalculateContributionViewModelItem[contribution.Term];
            for (int i = 0; i < contribution.Term; i++)
            {
                var monthInfo = new ResponseCalculateContributionViewModelItem();
                monthInfo.MonthNumber = i + 1;
                decimal income = ComplexIncomeAndSumCalculating(contribution, monthInfo, DecimalObjCalculating(allMonthsInfo, contribution, i));
                RoundingMistakeCheck(monthInfo, income, i, DecimalObjCalculating(allMonthsInfo, contribution, i));
                allMonthsInfo[i] = monthInfo;
            }

            return new ResponseCalculateContributionViewModel() { Items = allMonthsInfo };
        }

        private decimal DecimalObjCalculating(ResponseCalculateContributionViewModelItem[] allMonthsInfo, Contribution contribution, int i)
        {
            if (i != 0)
            {
                return allMonthsInfo[i - 1].Sum;
            }
            else
            {
                return contribution.StartValue;
            }
        }

        private void RoundingMistakeCheck(ResponseCalculateContributionViewModelItem monthInfo, decimal income, int i, decimal obj)
        {
            if (Math.Round(monthInfo.Sum, NumberOfDigitsAfterDecimalPoint) - Math.Round(obj, NumberOfDigitsAfterDecimalPoint) != Math.Round(income, NumberOfDigitsAfterDecimalPoint))
            {
                monthInfo.Income = Math.Round(monthInfo.Sum, NumberOfDigitsAfterDecimalPoint) - Math.Round(obj, NumberOfDigitsAfterDecimalPoint);
            }
        }

        private decimal ComplexIncomeAndSumCalculating(Contribution contribution, ResponseCalculateContributionViewModelItem monthInfo, decimal obj) 
        {
            decimal income = obj / Hundred * (contribution.Percent / NumberOfMonthsInAYear);
            monthInfo.Income = income;
            monthInfo.Sum = obj + income;
            return income;
        }
    }
}
