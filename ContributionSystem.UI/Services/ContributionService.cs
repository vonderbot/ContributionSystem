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
                var details = await response.Content.ReadFromJsonAsync<ResponseGetDetailsByIdContributionViewModel>();

                return details;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception in service: {ex.Message}");
            }
        }

        public async Task<ResponseGetHistoryByUserIdContributionViewModel> GetHistoryByUserId(int take, int skip, string userId)
        {
            try
            {
                var response = await _http.GetAsync($"{СontrollerName}/GetHistoryByUserId?Take={take}&Skip={skip}&UserId={userId}");
                await CheckResponseStatusCode(response);
                var details = await response.Content.ReadFromJsonAsync<ResponseGetHistoryByUserIdContributionViewModel>();

                return details;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception in service: {ex.Message}");
            }
        }

        public async Task<ResponseCalculateContributionViewModel> Сalculate(RequestCalculateContributionViewModel request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync($"{СontrollerName}/calculate", request);
                await CheckResponseStatusCode(response);
                var details = await response.Content.ReadFromJsonAsync<ResponseCalculateContributionViewModel>();

                return details;
            }
            catch(Exception ex)
            {
                throw new Exception($"Exception in service: {ex.Message}");
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