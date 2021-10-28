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
        private const string СontrollerName = "contribution";

        private readonly HttpClient _http;

        public ContributionService(HttpClient httpClient)
        {
            _http = httpClient;
        }

        public async Task<ResponseGetDetailsByIdContributionViewModel> GetDetailsById(int id)
        {
            var response = await _http.GetAsync($"{СontrollerName}/GetDetailsById?id={id}");
            await CheckResponseStatusCode(response);

            return await response.Content.ReadFromJsonAsync<ResponseGetDetailsByIdContributionViewModel>();
        }

        public async Task<ResponseGetHistoryContributionViewModel> GetHistory(int take, int skip)
        {
            var response = await _http.GetAsync($"{СontrollerName}/GetHistory?Take={take}&Skip={skip}");
            await CheckResponseStatusCode(response);

            return await response.Content.ReadFromJsonAsync<ResponseGetHistoryContributionViewModel>();
        }

        public async Task<ResponseCalculateContributionViewModel> Сalculate(RequestCalculateContributionViewModel request)
        {
            var response = await _http.PostAsJsonAsync($"{СontrollerName}/calculate", request);
            await CheckResponseStatusCode(response);

            return await response.Content.ReadFromJsonAsync<ResponseCalculateContributionViewModel>();
        }

        private async Task CheckResponseStatusCode(HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var exception = await response.Content.ReadFromJsonAsync<string>();
                throw new Exception(exception);
            }
        }
    }
}