using ContributionSystem.UI.Interfaces;
using ContributionSystem.ViewModels.Models.Contribution;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace ContributionSystem.UI.Pages
{
    public partial class Details : ComponentBase
    {
        [Inject]
        IContributionService ContributionService { get; set; }

        [Parameter]
        public int Id { get; set; }

        private ResponseCalculateContributionViewModel _responseCalculateContributionViewModel { get; set; }
        private string _message;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        public async Task LoadData()
        {
            try
            {
                _message = "loading...";
                var response = await ContributionService.GetDetails(Id);
                _responseCalculateContributionViewModel = new ResponseCalculateContributionViewModel 
                {
                    Items = response.Items,
                    CalculationMethod = response.CalculationMethod
                };
            }
            catch (Exception ex)
            {
                _message = ex.Message;
            }
        }
    }
}
