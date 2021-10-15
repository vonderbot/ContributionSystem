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

        public ContributionController(IContributionService сontributionService)
        {
            _contributionService = сontributionService;
        }

        [HttpPost]
        public IActionResult Calculate(RequestCalculateContributionViewModel request)
        {
            try
            {
                var response = _contributionService.Calculate(request);
                return Ok(response);
            }
            catch
            {
                return BadRequest("BadRequest");
            }
        }
    }
}