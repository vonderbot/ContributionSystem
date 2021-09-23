using ContributionSystem.BusinesLogic.Interfaces;
using ContributionSystem.Entities.Entities;
using ContributionSystem.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ContributionSystem.BusinesLogic.Services
{
    public class ContributionService: IContributionService
    {
        public ArrayList MonthsInfo(RequestPostContributionViewModel request)
        {
            decimal newValue = Math.Round(request.StartValue, 2);
            Contribution contribution = new Contribution(newValue, request.Term, request.Percent);
            ArrayList allMonthsInfo = new ArrayList();
            for (int i = 0; i < contribution.Term; i++)
            {
                ArrayList month = new ArrayList();
                decimal income = Math.Round((contribution.StartValue / 100 * ((decimal)contribution.Percent / 12)), 2);
                month.Add(i + 1);
                month.Add(income);
                month.Add(contribution.StartValue + income * (i + 1));
                allMonthsInfo.Add(month);
            }

            return allMonthsInfo;
        }
    }
}
