using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;
using System.Collections.Generic;

namespace ContributionSystem.UI.Services
{
    public class ContributionService : IContributionService
    {
        private readonly HttpClient _http;

        public ContributionService(HttpClient httpClient)
        {
            _http = httpClient;
        }

        public async Task<List<RequestCalculateContributionViewModel>> GetRequestsHistory(int numberOfContrbutionForLoad, int numberOfContrbutionForSkip)
        {
            var request = new RequestGetRequestsHistoryContrbutionViewModel()
            {
                NumberOfContrbutionsForLoad = numberOfContrbutionForLoad,
                NumberOfContrbutionsForSkip = numberOfContrbutionForSkip
            };
            var response = await _http.PostAsJsonAsync("https://localhost:44303/api/contribution/GetRequestsHistory", request);
            CheckResponseStatusCode(response);

            return await response.Content.ReadFromJsonAsync<List<RequestCalculateContributionViewModel>>();
        }

        public async Task<ResponseCalculateContributionViewModel> Сalculate(RequestCalculateContributionViewModel request)
        {
            var response = await _http.PostAsJsonAsync("https://localhost:44303/api/contribution/calculate", request);
            CheckResponseStatusCode(response);

            return await response.Content.ReadFromJsonAsync<ResponseCalculateContributionViewModel>();
        }

        private void CheckResponseStatusCode(HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Server response is incorrect");
            }
        }
    }
}