using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using ContributionSystem.ViewModels.Enums;
using System;

namespace ContributionSystem.UI.Pages
{
    public partial class ContributionCalculator : ComponentBase
    {
        [Inject]
        IContributionService ContributionService { get; set; }
        private RequestCalculateContributionViewModel Request;
        private ResponseCalculateContributionViewModel Content;
        private string ErrorMessage;

        public ContributionCalculator()
        {
            Request = new();
        }

        public async Task Calculate()
        {
            try
            {
                Content = await ContributionService.Сalculate(Request);
            }
            catch
            {
                ErrorMessage = "Error!";
            }
        }

        public void ChangeMethod(CalculationMethodEnumView method)
        {
            Request.CalculationMethod = method;
        }
    }
}
