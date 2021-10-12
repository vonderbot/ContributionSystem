using ContributionSystem.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.BusinessLogic.Interfaces;

namespace ContributionSystem.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class ContributionController : ControllerBase
    {
        private readonly IContributionService _contributionService;

        public ContributionController()
        {
            _contributionService = new ContributionService();
        }

        [HttpPost]
        public IActionResult Calculate(RequestCalculateContributionViewModel request)
        {
            ResponseCalculateContributionViewModel response = _contributionService.Calculate(request);

            return Ok(response);
        }
    }
}