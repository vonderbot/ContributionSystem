using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace ContributionSystem.UI.Services
{
    public class ContributionService : IContributionService
    {
        private readonly HttpClient http;

        public ContributionService()
        {
            http = new HttpClient();
        }

        public async Task<ResponseCalculateContributionViewModel> Сalculate(RequestCalculateContributionViewModel request)
        {
            var response = await http.PostAsJsonAsync("https://localhost:44303/api/contribution/calculate", request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<ResponseCalculateContributionViewModel>();
            }
            else
            {
                throw new Exception("Server response is incorrect");
            }
        }
    }
}
