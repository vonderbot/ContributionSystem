using ContributionSystem.Entities.Entities;
using System;
using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.BusinessLogic.Interfaces;
using System.Collections.Generic;

namespace ContributionSystem.BusinessLogic.Services
{
    public class ContributionService : IContributionService
    {
        private const int Hundred = 100;
        private const int NumberOfMonthsInAYear = 12;
        private const int NumberOfDigitsAfterDecimalPoint = 2;

        public ResponseCalculateContributionViewModel Calculate(RequestCalculateContributionViewModel request)
        {
            CheckRequest(request);
            var contribution = new Contribution(request.StartValue, request.Term, request.Percent);
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

        private static void CheckRequest(RequestCalculateContributionViewModel request)
        {
            if (request == null)
            {
                throw new Exception("Null request");
            }
            if (request.StartValue <= 0)
            {
                throw new Exception("Incorrect start value in request");
            }
            if (request.Term <= 0)
            {
                throw new Exception("Incorrect term in request");
            }
            if (request.Percent <= 0)
            {
                throw new Exception("Incorrect percent in request");
            }
        }

        private static void SimpleCalculate(Contribution contribution, List<ResponseCalculateContributionViewModelItem> allMonthsInfo)
        {
            decimal income = contribution.StartValue / Hundred * (contribution.Percent / NumberOfMonthsInAYear);
            for (int i = 0; i < contribution.Term; i++)
            {
                var monthInfo = new ResponseCalculateContributionViewModelItem()
                {
                    MonthNumber = i + 1,
                    Income = income,
                    Sum = Math.Round(contribution.StartValue + income * (i + 1), NumberOfDigitsAfterDecimalPoint)
                };
                RoundingMistakeCheck(monthInfo, income, ChoosePreviousElemet(allMonthsInfo, contribution, i));
                allMonthsInfo.Add(monthInfo);
            }
        }

        private static void ComplexCalculate(Contribution contribution, List<ResponseCalculateContributionViewModelItem> allMonthsInfo)
        {
            for (int i = 0; i < contribution.Term; i++)
            {
                var monthInfo = new ResponseCalculateContributionViewModelItem();
                monthInfo.MonthNumber = i + 1;
                decimal income = ComplexIncomeAndSumCalculating(contribution, monthInfo, ChoosePreviousElemet(allMonthsInfo, contribution, i));
                RoundingMistakeCheck(monthInfo, income, ChoosePreviousElemet(allMonthsInfo, contribution, i));
                allMonthsInfo.Add(monthInfo);
            }
            for (int i = 0; i < contribution.Term; i++)
            {
                allMonthsInfo[i].Sum = Math.Round(allMonthsInfo[i].Sum, NumberOfDigitsAfterDecimalPoint);
            }
        }

        private static decimal ChoosePreviousElemet(List<ResponseCalculateContributionViewModelItem> allMonthsInfo, Contribution contribution, int index)
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
            if (Math.Round(monthInfo.Sum, NumberOfDigitsAfterDecimalPoint) - Math.Round(previousElemet, NumberOfDigitsAfterDecimalPoint) != Math.Round(income, NumberOfDigitsAfterDecimalPoint))
            {
                monthInfo.Income = Math.Round(monthInfo.Sum, NumberOfDigitsAfterDecimalPoint) - Math.Round(previousElemet, NumberOfDigitsAfterDecimalPoint);
            }
            else
            {
                monthInfo.Income = Math.Round(monthInfo.Income, NumberOfDigitsAfterDecimalPoint);
            }
        }

        private static decimal ComplexIncomeAndSumCalculating(Contribution contribution, ResponseCalculateContributionViewModelItem monthInfo, decimal previousElemet)
        {
            decimal income = previousElemet / Hundred * (contribution.Percent / NumberOfMonthsInAYear);
            monthInfo.Income = income;
            monthInfo.Sum = previousElemet + income;
            return income;
        }
    }
}
