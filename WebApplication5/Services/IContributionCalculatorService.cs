using ContributionSystem.ViewModels.Models.Contribution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Services
{
    public interface IContributionCalculatorService
    {
        Task<IEnumerable<ResponseCalculateContributionViewModel>> CalculateContribution();
    }
}
