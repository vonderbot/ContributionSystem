using ContributionSystem.BusinesLogic.Interfaces;
using ContributionSystem.Entities.Entities;
using ContributionSystem.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using ContributionSystem.ViewModels.Items;

namespace ContributionSystem.BusinesLogic.Services
{
    public class PostContributionService: IPostContributionService
    {
        public ResponsePostContributionViewModel Calculate(RequestPostContributionViewModel request)
        {
            decimal newValue = Math.Round(request.StartValue, 2);
            decimal newPercent = Math.Round(request.Percent, 2);
            Contribution contribution = new Contribution(newValue, request.Term, newPercent);
            decimal income = Math.Round((contribution.StartValue / 100 * ((decimal)contribution.Percent / 12)), 2);
            ResponsePostContributionViewModelItem[] allMonthsInfo = new ResponsePostContributionViewModelItem[contribution.Term];
            for (int i = 0; i < contribution.Term; i++)
            {
                ResponsePostContributionViewModelItem monthInfo = new ResponsePostContributionViewModelItem
                {
                    MonthNumber = i + 1,
                    Income = income,
                    Sum = contribution.StartValue + income * (i + 1)
                };
                allMonthsInfo[i] = monthInfo;
            }

            return new ResponsePostContributionViewModel() { Items = allMonthsInfo };
        }
    }
}
