using ContributionSystem.BusinesLogic.Interfaces;
using ContributionSystem.Entities.Entities;
using ContributionSystem.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace ContributionSystem.BusinesLogic.Services
{
    public class ContributionService: IContributionService
    {
        public ResponseCalculateContributionViewModel Calculate(RequestCalculateContributionViewModel request)
        {
            //decimal newValue = Math.Round(request.StartValue, 2);
            //decimal newPercent = Math.Round(request.Percent, 2);
            Contribution contribution = new Contribution(request.StartValue, request.Term, request.Percent);
            decimal income = Math.Round(contribution.StartValue / 100 * (contribution.Percent / 12), 2);
            ResponseCalculateContributionViewModelItem[] allMonthsInfo = new ResponseCalculateContributionViewModelItem[contribution.Term];
            for (int i = 0; i < contribution.Term; i++)
            {
                ResponseCalculateContributionViewModelItem monthInfo = new ResponseCalculateContributionViewModelItem
                {
                    MonthNumber = i + 1,
                    Income = income,
                    Sum = contribution.StartValue + income * (i + 1)
                };
                allMonthsInfo[i] = monthInfo;
            }

            return new ResponseCalculateContributionViewModel() { Items = allMonthsInfo };
        }
    }
}
