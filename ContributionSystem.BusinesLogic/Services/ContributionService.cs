using ContributionSystem.Entities.Entities;
using System;
using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.BusinessLogic.Interfaces;

namespace ContributionSystem.BusinessLogic.Services
{
    public class ContributionService : IContributionService
    {
        private const int _hundred = 100;
        private const int _numberOfMonthsInAYear = 12;
        private const int _NumberOfDigitsAfterDecimalPoint = 2;

        public ResponseCalculateContributionViewModel Calculate(RequestCalculateContributionViewModel request)
        {
            CheckRequest(request);
            var contribution = new Contribution(request.StartValue, request.Term, request.Percent);
            var allMonthsInfo = new ResponseCalculateContributionViewModelItem[contribution.Term];
            switch (request.CalculationMethod)
            {
                case CalculationMethodEnumView.Simple:
                    SimpleCalculate(contribution, allMonthsInfo);
                    break;
                case CalculationMethodEnumView.Complex:
                    ComplexCalculate(contribution, allMonthsInfo);
                    break;
                default:
                    throw new Exception("Incorect calculation method");
            }

            return new ResponseCalculateContributionViewModel()
            {
                CalculationMethod = request.CalculationMethod,
                Items = allMonthsInfo
            };
        }

        private static void CheckRequest(RequestCalculateContributionViewModel request)
        {
            if (request == null)
            {
                throw new Exception("Null request");
            }
            if (request.StartValue <= 0)
            {
                throw new Exception("Incorect start value in request");
            }
            if (request.Term <= 0)
            {
                throw new Exception("Incorect term in request");
            }
            if (request.Percent <= 0)
            {
                throw new Exception("Incorect percent in request");
            }
        }

        private static void SimpleCalculate(Contribution contribution, ResponseCalculateContributionViewModelItem[] allMonthsInfo)
        {
            decimal income = contribution.StartValue / _hundred * (contribution.Percent / _numberOfMonthsInAYear);
            for (int i = 0; i < contribution.Term; i++)
            {
                var monthInfo = new ResponseCalculateContributionViewModelItem()
                {
                    MonthNumber = i + 1,
                    Income = income,
                    Sum = Math.Round(contribution.StartValue + income * (i + 1), _NumberOfDigitsAfterDecimalPoint)
                };
                RoundingMistakeCheck(monthInfo, income, ChoosePreviousElemet(allMonthsInfo, contribution, i));
                allMonthsInfo[i] = monthInfo;
            }
        }

        private static void ComplexCalculate(Contribution contribution, ResponseCalculateContributionViewModelItem[] allMonthsInfo)
        {
            for (int i = 0; i < contribution.Term; i++)
            {
                var monthInfo = new ResponseCalculateContributionViewModelItem();
                monthInfo.MonthNumber = i + 1;
                decimal income = ComplexIncomeAndSumCalculating(contribution, monthInfo, ChoosePreviousElemet(allMonthsInfo, contribution, i));
                RoundingMistakeCheck(monthInfo, income, ChoosePreviousElemet(allMonthsInfo, contribution, i));
                allMonthsInfo[i] = monthInfo;
            }
            for (int i = 0; i < contribution.Term; i++)
            {
                allMonthsInfo[i].Sum = Math.Round(allMonthsInfo[i].Sum, _NumberOfDigitsAfterDecimalPoint);
            }
        }

        private static decimal ChoosePreviousElemet(ResponseCalculateContributionViewModelItem[] allMonthsInfo, Contribution contribution, int index)
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

        private static void RoundingMistakeCheck(ResponseCalculateContributionViewModelItem monthInfo, decimal income, decimal previousElemet)
        {
            if (Math.Round(monthInfo.Sum, _NumberOfDigitsAfterDecimalPoint) - Math.Round(previousElemet, _NumberOfDigitsAfterDecimalPoint) != Math.Round(income, _NumberOfDigitsAfterDecimalPoint))
            {
                monthInfo.Income = Math.Round(monthInfo.Sum, _NumberOfDigitsAfterDecimalPoint) - Math.Round(previousElemet, _NumberOfDigitsAfterDecimalPoint);
            }
            else
            {
                monthInfo.Income = Math.Round(monthInfo.Income, _NumberOfDigitsAfterDecimalPoint);
            }
        }

        private static decimal ComplexIncomeAndSumCalculating(Contribution contribution, ResponseCalculateContributionViewModelItem monthInfo, decimal previousElemet)
        {
            decimal income = previousElemet / _hundred * (contribution.Percent / _numberOfMonthsInAYear);
            monthInfo.Income = income;
            monthInfo.Sum = previousElemet + income;
            return income;
        }
    }
}
