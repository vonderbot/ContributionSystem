using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContributionSystem.UI.Services
{
    public class ContributionService : IContributionService
    {
        private readonly string _controllerName;
        private readonly HttpClient _http;

        public ContributionService(HttpClient httpClient)
        {
            _http = httpClient;
            _controllerName = "contribution/";
        }

        public async Task<List<ResponseGetRequestsHistoryContributionViewModel>> GetRequestsHistory(int take, int skip)
        {
            var request = new RequestGetRequestsHistoryContributionViewModel()
            {
                NumberOfContrbutionsForLoad = take,
                NumberOfContrbutionsForSkip = skip
            };
            var properties = from p in request.GetType().GetProperties()
                             where p.GetValue(request, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(request, null).ToString());
            string queryString = "?" + String.Join("&", properties.ToArray());
            var response = await _http.GetAsync(_controllerName + "GetRequestsHistory/" + queryString);
            await CheckResponseStatusCode(response);

            return await response.Content.ReadFromJsonAsync<List<ResponseGetRequestsHistoryContributionViewModel>>();
        }

        public async Task<ResponseCalculateContributionViewModel> Сalculate(RequestCalculateContributionViewModel request)
        {
            var response = await _http.PostAsJsonAsync(_controllerName + "calculate/", request);
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