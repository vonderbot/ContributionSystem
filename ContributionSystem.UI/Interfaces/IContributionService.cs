using ContributionSystem.ViewModels.Models.Contribution;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Interfaces
{
    public interface IContributionService
    {
        public Task<ResponseCalculateContributionViewModel> Сalculate(RequestCalculateContributionViewModel request);
    }
}
