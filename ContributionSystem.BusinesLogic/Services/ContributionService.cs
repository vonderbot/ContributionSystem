using ContributionSystem.BusinesLogic.Interfaces;
using ContributionSystem.Entities.Entities;
using System;
using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.ViewModels.Enums;

namespace ContributionSystem.BusinesLogic.Services
{
    public class ContributionService : IContributionService
    {
        private const int Hundred = 100;
        private const int NumberOfMonthsInAYear = 12;
        private const int NumberOfDigitsAfterDecimalPoint = 2;

        public ResponseCalculateContributionViewModel Calculate(RequestCalculateContributionViewModel request)
        {
            var contribution = new Contribution(request.StartValue, request.Term, request.Percent);
            var allMonthsInfo = new ResponseCalculateContributionViewModelItem[contribution.Term];
            if (request.CalculationMethod == CalculationMethodEnumView.CalculationMethod.Simple) {
                decimal income = contribution.StartValue / Hundred * (contribution.Percent / NumberOfMonthsInAYear);
                for (int i = 0; i < contribution.Term; i++)
                {
                    var monthInfo = new ResponseCalculateContributionViewModelItem()
                    {
                        MonthNumber = i + 1,
                        Income = income,
                        Sum = contribution.StartValue + income * (i + 1)
                    };
                    RoundingMistakeCheck(monthInfo, income, ChoosePreviousElemet(allMonthsInfo, contribution, i));
                    allMonthsInfo[i] = monthInfo;
                }
            }
            else if (request.CalculationMethod == CalculationMethodEnumView.CalculationMethod.Complex) {
                for (int i = 0; i < contribution.Term; i++)
                {
                    var monthInfo = new ResponseCalculateContributionViewModelItem();
                    monthInfo.MonthNumber = i + 1;
                    decimal income = ComplexIncomeAndSumCalculating(contribution, monthInfo, ChoosePreviousElemet(allMonthsInfo, contribution, i));
                    RoundingMistakeCheck(monthInfo, income, ChoosePreviousElemet(allMonthsInfo, contribution, i));
                    allMonthsInfo[i] = monthInfo;
                }
            }
            else
            {
                throw new Exception();
            }

            return new ResponseCalculateContributionViewModel()
            {
                CalculationMethod = request.CalculationMethod,
                Items = allMonthsInfo
            };
        }

        private decimal ChoosePreviousElemet(ResponseCalculateContributionViewModelItem[] allMonthsInfo, Contribution contribution, int index)
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

        private void RoundingMistakeCheck(ResponseCalculateContributionViewModelItem monthInfo, decimal income, decimal previousElemet)
        {
            if (Math.Round(monthInfo.Sum, NumberOfDigitsAfterDecimalPoint) - Math.Round(previousElemet, NumberOfDigitsAfterDecimalPoint) != Math.Round(income, NumberOfDigitsAfterDecimalPoint))
            {
                monthInfo.Income = Math.Round(monthInfo.Sum, NumberOfDigitsAfterDecimalPoint) - Math.Round(previousElemet, NumberOfDigitsAfterDecimalPoint);
            }
        }

        private decimal ComplexIncomeAndSumCalculating(Contribution contribution, ResponseCalculateContributionViewModelItem monthInfo, decimal previousElemet) 
        {
            decimal income = previousElemet / Hundred * (contribution.Percent / NumberOfMonthsInAYear);
            monthInfo.Income = income;
            monthInfo.Sum = previousElemet + income;
            return income;
        }
    }
}
