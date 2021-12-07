using Azure.Identity;
using ContributionSystem.BusinessLogic.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.Graph;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ContributionSystem.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly GraphServiceClient graphClient;

        public UserService() 
        {
            graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(async request =>
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", "eyJ0eXAiOiJKV1QiLCJub25jZSI6Im1LdHJrR0FJWFpoUlktNEpBc0oxejlTSWIwSG9qdjZD" +
                    "RF8zTVNuSXBNc0kiLCJhbGciOiJSUzI1NiIsIng1dCI6Imwzc1EtNTBjQ0g0eEJWWkxIVEd3blNSNzY4MCIsImtpZCI6Imwzc1EtNTBjQ0g0eEJWWkxIVEd3blNSNzY4MCJ9.eyJhdWQ" +
                    "iOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8wNmY3MzQzYS0wNGZlLTQ4MmYtODk5Mi1lY2MyODg5ODVlOWMvIiwiaWF" +
                    "0IjoxNjM4ODg3MDg3LCJuYmYiOjE2Mzg4ODcwODcsImV4cCI6MTYzODg5MTU1MCwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkUyWmdZT0NYNW5IL2Q2ekNOZkxrc1k5SHJYMzNOaGpl" +
                    "T3JkKytqTWRyN2lyK1NueWlrY0EiLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IkJsYXpvciBDbGllbnQgQUFEIiwiYXBwaWQiOiI5MDhkZTgxYi1lNDc1LTRkZDctYjgyY" +
                    "S03ZDUxYzc5NTQ4OGQiLCJhcHBpZGFjciI6IjAiLCJmYW1pbHlfbmFtZSI6Ik1hcnRoYW5vbnNhIiwiZ2l2ZW5fbmFtZSI6Ik1hcnRoYSIsImlkdHlwIjoidXNlciIsImlwYWRkciI6Ij" +
                    "kxLjIwOC4xNTMuMSIsIm5hbWUiOiJNYXJ0aGEiLCJvaWQiOiJiZjFmNGMzYy1kYzI1LTRlZTgtYWFlMi1kOWRhN2Y4M2FiNDYiLCJwbGF0ZiI6IjMiLCJwdWlkIjoiMTAwMzIwMDFBRjd" +
                    "CNEEwNCIsInJoIjoiMC5BVThBT2pUM0J2NEVMMGlKa3V6Q2lKaGVuQnZvalpCMTVOZE51Q3A5VWNlVlNJMVBBQVUuIiwic2NwIjoib3BlbmlkIHByb2ZpbGUgVXNlci5SZWFkIFVzZXIuU" +
                    "mVhZC5BbGwgZW1haWwiLCJzaWduaW5fc3RhdGUiOlsia21zaSJdLCJzdWIiOiJ4cDVJbHRiSVRmN3dHWC1SWnJCNjhMX05MTEktamRsMVhPV3BrSFlpaFdrIiwidGVuYW50X3JlZ2lvbl" +
                    "9zY29wZSI6IkVVIiwidGlkIjoiMDZmNzM0M2EtMDRmZS00ODJmLTg5OTItZWNjMjg4OTg1ZTljIiwidW5pcXVlX25hbWUiOiJNYXJ0aGFAYnRpbW9zaGl0c2tpeXdvcmtnbWFpbC5vbm1" +
                    "pY3Jvc29mdC5jb20iLCJ1cG4iOiJNYXJ0aGFAYnRpbW9zaGl0c2tpeXdvcmtnbWFpbC5vbm1pY3Jvc29mdC5jb20iLCJ1dGkiOiJ5cW1QMzVTV1pVbXUzWXB0UWR2ZkFRIiwidmVyIjoi" +
                    "MS4wIiwid2lkcyI6WyJmZTkzMGJlNy01ZTYyLTQ3ZGItOTFhZi05OGMzYTQ5YTM4YjEiLCJiNzlmYmY0ZC0zZWY5LTQ2ODktODE0My03NmIxOTRlODU1MDkiXSwieG1zX3N0Ijp7InN1Y" +
                    "iI6IjlxeHpDelRTR0YxQkt6YlpiMVBIM1ZEWVdiNEpKbWxRd2c5bnRMdzVEWWcifSwieG1zX3RjZHQiOjE2MzcxNDExNjR9.IMLDW6KyQmw3kJHx525InAJ2pDHs2zkSxq6DvzvRe0UOU" +
                    "tSl3CczPfxwgoFBHijaBrnwnHteVT-UCO43PLL8Bxo4WdFQl-HmmYcBKgNh9UNWhbf1uQa_igVBhrQ6JGskI-AqpyUeGlh9mrvweD6dJ2jzuL-rzBkJz1m_H-HMh2d2fGM3N41FPWVwpP" +
                    "rXbJ6nxhShjGj0fPVMIuIZghvp5x2Df6-DCuqjzfot-UIGEimNlz8Wq6O9fAdKSXCdVF0ET3BP7tkb26R_HidFqjuml9JHb9C9T23ZZHAA-d_ZnrXeq60s6EkCbIBY3ZVXeYQfQlAZcgO" +
                    "6SauD36O0z9Dy1g");
            }));

            //var scopes = new[] { "User.Read.All", "User.ReadWrite.All", "AppRoleAssignment.ReadWrite.All", "Directory.AccessAsUser.All", "Directory.Read.All", "Directory.ReadWrite.All" };
            //var tenantId = "06f7343a-04fe-482f-8992-ecc288985e9c";
            //var clientId = "3dfbce03-4be6-4a68-9d7f-4c10852029";
            //var options = new TokenCredentialOptions
            //{
            //    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            //};
            //Func<DeviceCodeInfo, CancellationToken, Task> callback = (code, cancellation) =>
            //{
            //    Console.WriteLine(code.Message);
            //    return Task.FromResult(0);
            //};
            //var deviceCodeCredential = new DeviceCodeCredential(
            //    callback, tenantId, clientId, options);

            //graphClient = new GraphServiceClient(deviceCodeCredential, scopes);
        }

        public async Task<ResponseGetUsersListContributionViewModel> GetUsersList()
        {
            var users = await graphClient.Users.Request().GetAsync();
            var response = new ResponseGetUsersListContributionViewModel()
            {
                Items = users.Select(u => new ResponseGetUsersListContributionViewModelItem
                {
                    Id = u.Id,
                    Name = u.DisplayName,
                    Status = graphClient.Users[u.Id].Request().Select(c => c.AccountEnabled).GetAsync().Result.AccountEnabled.GetValueOrDefault()
                }).ToList()
            };

            return response;
        }

        public string GetUserId(ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User have no id");
            }
            else
            {
                return userId;
            }
        }
    }
}
