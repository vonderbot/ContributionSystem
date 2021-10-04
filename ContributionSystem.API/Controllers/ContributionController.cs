using ContributionSystem.BusinesLogic.Services;
using ContributionSystem.BusinesLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ContributionSystem.ViewModels.Models.Contribution;

namespace ContributionSystem.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class ContributionController : ControllerBase
    {
        private readonly IContributionService contributionService;

        public ContributionController()
        {
            contributionService = new ContributionService();
        }

        [HttpPost]
        public IActionResult Calculate(RequestCalculateContributionViewModel request)
        {
            ResponseCalculateContributionViewModel response = new();
            if (request.MethodNumber == 0)
            {
                response = contributionService.SimplCalculate(request);
            }
            else if(request.MethodNumber == 1)
            {
                response = contributionService.ComplexCalculate(request);
            }

            return Ok(response);
        }
    }
}