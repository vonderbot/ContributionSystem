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
        private readonly IContributionRepositoryService _contributionRepositoryService;

        public ContributionController(IContributionService сontributionService, IContributionRepositoryService contributionRepositoryService)
        {
            _contributionService = сontributionService;
            _contributionRepositoryService = contributionRepositoryService;
        }

        [HttpPost]
        public IActionResult Calculate(RequestCalculateContributionViewModel request)
        {
            try
            {
                var response = _contributionService.Calculate(request);
                _contributionRepositoryService.AddContribution(request);
                return Ok(response);
            }
            catch
            {
                return BadRequest("BadRequest");
            }
        }
    }
}