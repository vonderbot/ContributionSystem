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
            try
            {
                var response = await _http.GetAsync($"{СontrollerName}/GetDetailsById?id={id}");
                await CheckResponseStatusCode(response);
                ResponseGetDetailsByIdContributionViewModel details;
                details = await response.Content.ReadFromJsonAsync<ResponseGetDetailsByIdContributionViewModel>();

                return details;
            }
            catch
            {
                throw new Exception("Server wrong model");
            }
        }

        public async Task<ResponseGetHistoryContributionViewModel> GetHistory(int take, int skip)
        {
            try
            {
                var response = await _http.GetAsync($"{СontrollerName}/GetHistory?Take={take}&Skip={skip}");
                await CheckResponseStatusCode(response);
                ResponseGetHistoryContributionViewModel details;
                details = await response.Content.ReadFromJsonAsync<ResponseGetHistoryContributionViewModel>();

                return details;
            }
            catch
            {
                throw new Exception("Server wrong model");
            }
        }

        public async Task<ResponseCalculateContributionViewModel> Сalculate(RequestCalculateContributionViewModel request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync($"{СontrollerName}/calculate", request);
                await CheckResponseStatusCode(response);
                ResponseCalculateContributionViewModel details;
                details = await response.Content.ReadFromJsonAsync<ResponseCalculateContributionViewModel>();

                return details;
            }
            catch
            {
                throw new Exception("Server wrong model");
            }
        }

        private async Task CheckResponseStatusCode(HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var exception = await response.Content.ReadAsStringAsync();
                throw new Exception(exception);
            }
        }
    }
}