using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace ContributionSystem.UI.Services
{
    /// <inheritdoc/>
    public class ContributionService : BaseService, IContributionService
    {
        private const string СontrollerName = "contribution";

        /// <summary>
        /// ContributionService constructor.
        /// </summary>
        /// <param name="httpClient">HttpClient instance.</param>
        /// <param name="tokenProvider">IAccessTokenProvider instance.</param>
        public ContributionService(HttpClient httpClient, IAccessTokenProvider tokenProvider)
            : base(httpClient, tokenProvider)
        {
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task<ResponseGetHistoryByUserIdContributionViewModel> GetHistoryByUserId(int take, int skip)
        {
            try
            {
                var response = await _http.GetAsync($"{СontrollerName}/GetHistoryByUserId?Take={take}&Skip={skip}");
                await CheckResponseStatusCode(response);
                var details = await response.Content.ReadFromJsonAsync<ResponseGetHistoryByUserIdContributionViewModel>();

                return details;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception in service: {ex.Message}");
            }
        }

        /// <inheritdoc/>
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