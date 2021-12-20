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
    /// <inheritdoc cref="IContributionService" />
    public class ContributionService : BaseService, IContributionService
    {
        private const string СontrollerName = "contribution";

        /// <summary>
        /// Creates a new instance of <see cref="ContributionService" />.
        /// </summary>
        /// <param name="httpClient"><see cref="HttpClient" /> instance.</param>
        /// <param name="tokenProvider"><see cref="IAccessTokenProvider" /> instance.</param>
        public ContributionService(HttpClient httpClient, IAccessTokenProvider tokenProvider)
            : base(httpClient, tokenProvider)
        {
        }

        /// <inheritdoc/>
        public async Task<ResponseGetDetailsByIdContributionViewModel> GetDetailsById(int id)
        {
            try
            {
                var response = await Http.GetAsync($"{СontrollerName}/get-details-by-id?id={id}");
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
                var response = await Http.GetAsync($"{СontrollerName}/get-history-by-user-id?Take={take}&Skip={skip}");
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
                var response = await Http.PostAsJsonAsync($"{СontrollerName}/calculate", request);
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