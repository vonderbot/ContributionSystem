﻿using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace ContributionSystem.UI.Services
{
    public class ContributionService : IContributionService
    {
        private const string СontrollerName = "contribution";

        private readonly HttpClient _http;

        [Inject]
        IAccessTokenProvider TokenProvider { get; set; }

        public ContributionService(HttpClient httpClient, IAccessTokenProvider tokenProvider)
        {
            _http = httpClient;
            TokenProvider = tokenProvider;
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
                var tokenResult = await TokenProvider.RequestAccessToken();
                tokenResult.TryGetToken(out var token);
                var request = new HttpRequestMessage(HttpMethod.Get, $"{СontrollerName}/GetHistoryByUserId?Take={take}&Skip={skip}&UserId={userId}");
                request.Headers.Authorization = new AuthenticationHeaderValue(token.Value);
                var response = await _http.SendAsync(request);
                //var response = await _http.GetAsync($"{СontrollerName}/GetHistoryByUserId?Take={take}&Skip={skip}&UserId={userId}");
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