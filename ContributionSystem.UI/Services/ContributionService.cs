using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;

namespace ContributionSystem.UI.Services
{
    public class ContributionService : IContributionService
    {
        private readonly HttpClient _http;

        public ContributionService(HttpClient httpClient)
        {
            _http = httpClient;
        }

        public async Task<ResponseCalculateContributionViewModel> Сalculate(RequestCalculateContributionViewModel request)
        {
            var response = await _http.PostAsJsonAsync("https://localhost:44303/api/contribution/calculate", request);

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
