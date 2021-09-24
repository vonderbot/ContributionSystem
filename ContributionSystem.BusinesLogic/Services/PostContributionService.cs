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
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace ContributionSystem.BusinesLogic.Services
{
    public class PostContributionService: IPostContributionService
    {
        public ResponsePostContributionViewModel Calculate(RequestPostContributionViewModel request)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(request);
            if (!Validator.TryValidateObject(request, context, results, true))
            {
                throw new Exception("Bad request");
            }

            //decimal newValue = Math.Round(request.StartValue, 2);
            //decimal newPercent = Math.Round(request.Percent, 2);
            Contribution contribution = new Contribution(request.StartValue, request.Term, request.Percent);
            context = new ValidationContext(contribution);
            if (!Validator.TryValidateObject(contribution, context, results, true))
            {
                throw new Exception("Wrong contribution create.");
            }

            decimal income = Math.Round(contribution.StartValue / 100 * (contribution.Percent / 12), 2);
            ResponsePostContributionViewModelItem[] allMonthsInfo = new ResponsePostContributionViewModelItem[contribution.Term];
            for (int i = 0; i < contribution.Term; i++)
            {
                ResponsePostContributionViewModelItem monthInfo = new ResponsePostContributionViewModelItem
                {
                    MonthNumber = i + 1,
                    Income = income,
                    Sum = contribution.StartValue + income * (i + 1)
                };
                context = new ValidationContext(monthInfo);
                if (!Validator.TryValidateObject(monthInfo, context, null, true))
                {
                    throw new Exception("Wrong response item create.");
                }
                allMonthsInfo[i] = monthInfo;
            }
            context = new ValidationContext(allMonthsInfo);
            if (!Validator.TryValidateObject(allMonthsInfo, context, null, true))
            {
                throw new Exception("Wrong response create.");
            }

            return new ResponsePostContributionViewModel() { Items = allMonthsInfo };
        }
    }
}
