using ContributionSystem.ViewModels.Models.Contribution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication5.Services
{
    public class ContributionCalculatorService : IContributionCalculatorService
    {
        private readonly HttpClient httpClient;

        public ContributionCalculatorService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<IEnumerable<ResponseCalculateContributionViewModel>> CalculateContribution()
        {
            throw new NotImplementedException();
        }
    }
}
