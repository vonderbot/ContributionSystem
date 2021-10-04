using ContributionSystem.ViewModels.Models.Contribution;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Services
{
    public class ContributionCalculatorService
    {
        public async Task<HttpResponseMessage> СontributionСalculate(RequestCalculateContributionViewModel request)
        {
            var Http = new HttpClient();
            return await Http.PostAsJsonAsync("https://localhost:44308/api/contribution/calculate", request);
        }
    }
}
