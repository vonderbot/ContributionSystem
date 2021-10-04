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
        IContributionService ContributionCalculatorService { get; set; }
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
                Content = await ContributionCalculatorService.Сalculate(Request);
            }
            catch
            {
                ErrorMessage = "Error!";
            }
        }

        public void ChangeMethod(int methodNumber)
        {
            Request.Method = (CalculationMethodEnumView.CalculationMethod)Enum.GetValues(typeof(CalculationMethodEnumView.CalculationMethod)).GetValue(methodNumber);
        }
    }
}
