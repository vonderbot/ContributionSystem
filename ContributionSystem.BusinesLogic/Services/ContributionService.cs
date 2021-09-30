using ContributionSystem.BusinesLogic.Interfaces;
using ContributionSystem.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using ContributionSystem.ViewModels.Models.Contribution;

namespace ContributionSystem.BusinesLogic.Services
{
    public class ContributionService: IContributionService
    {
        public ResponseCalculateContributionViewModel SimplCalculate(RequestCalculateContributionViewModel request)
        {
            Contribution contribution = new Contribution(request.StartValue, request.Term, request.Percent);
            decimal income = contribution.StartValue / 100 * (contribution.Percent / 12);
            ResponseCalculateContributionViewModelItem[] allMonthsInfo = new ResponseCalculateContributionViewModelItem[contribution.Term];
            for (int i = 0; i < contribution.Term; i++)
            {
                ResponseCalculateContributionViewModelItem monthInfo = new ResponseCalculateContributionViewModelItem
                {
                    MonthNumber = i + 1,
                    Income = income,
                    Sum = contribution.StartValue + income * (i + 1)
                };
                if (i != 0)
                {
                    if (Math.Round(monthInfo.Sum, 2) - Math.Round(allMonthsInfo[i - 1].Sum, 2) != Math.Round(income, 2))
                    {
                        monthInfo.Income = Math.Round(monthInfo.Sum, 2) - Math.Round(allMonthsInfo[i - 1].Sum, 2);
                    }
                }
                else
                {
                    if (Math.Round(monthInfo.Sum, 2) - Math.Round(contribution.StartValue, 2) != Math.Round(income, 2))
                    {
                        monthInfo.Income = Math.Round(monthInfo.Sum, 2) - Math.Round(contribution.StartValue, 2);
                    }
                }
                allMonthsInfo[i] = monthInfo;
            }

            return new ResponseCalculateContributionViewModel() { Items = allMonthsInfo };
        }
        public ResponseCalculateContributionViewModel ComplexCalculate(RequestCalculateContributionViewModel request)
        {
            Contribution contribution = new Contribution(request.StartValue, request.Term, request.Percent);
            ResponseCalculateContributionViewModelItem[] allMonthsInfo = new ResponseCalculateContributionViewModelItem[contribution.Term];
            for (int i = 0; i < contribution.Term; i++)
            {

                ResponseCalculateContributionViewModelItem monthInfo = new ResponseCalculateContributionViewModelItem();
                if (i != 0)
                {

                    decimal income = allMonthsInfo[i - 1].Sum / 100 * (contribution.Percent / 12);
                    monthInfo.MonthNumber = i + 1;
                    monthInfo.Income = income;
                    monthInfo.Sum = allMonthsInfo[i - 1].Sum + income;
                    if (Math.Round(monthInfo.Sum, 2) - Math.Round(allMonthsInfo[i - 1].Sum, 2) != Math.Round(income, 2))
                    {
                        monthInfo.Income = Math.Round(monthInfo.Sum, 2) - Math.Round(allMonthsInfo[i - 1].Sum, 2);
                    }
                }
                else 
                {

                    decimal income = contribution.StartValue / 100 * (contribution.Percent / 12);
                    monthInfo.MonthNumber = i + 1;
                    monthInfo.Income = income;
                    monthInfo.Sum = contribution.StartValue + income;
                    if (Math.Round(monthInfo.Sum, 2) - Math.Round(contribution.StartValue, 2) != Math.Round(income, 2))
                    {
                        monthInfo.Income = Math.Round(monthInfo.Sum, 2) - Math.Round(contribution.StartValue, 2);
                    }
                }
                allMonthsInfo[i] = monthInfo;
            }

            return new ResponseCalculateContributionViewModel() { Items = allMonthsInfo };
        }
    }
}
