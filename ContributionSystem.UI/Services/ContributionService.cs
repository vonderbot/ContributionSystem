using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace ContributionSystem.UI.Services
{
    public class ContributionService : IContributionService
    {
        private readonly HttpClient Http;

        public ContributionService()
        {
            Http = new HttpClient();
        }

        public async Task<ResponseCalculateContributionViewModel> Сalculate(RequestCalculateContributionViewModel request)
        {
            var response = await Http.PostAsJsonAsync("https://localhost:44308/api/contribution/calculate", request);
            if (((int)response.StatusCode) == 200)
            {
                return await response.Content.ReadFromJsonAsync<ResponseCalculateContributionViewModel>();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
