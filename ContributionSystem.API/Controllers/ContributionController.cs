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

        private readonly IRepositoryService _contributionRepositoryService;

        public ContributionController(IContributionService сontributionService, IRepositoryService contributionRepositoryService)
        {
            _contributionService = сontributionService;
            _contributionRepositoryService = contributionRepositoryService;
        }

        [HttpPost]
        public IActionResult GetRequestsHistory(RequestGetRequestsHistoryContrbutionViewModel request)
        {
            try
            {
                var response = _contributionRepositoryService.GetRequestsHistory(request);

                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Calculate(RequestCalculateContributionViewModel request)
        {
            try
            {
                var response = _contributionService.Calculate(request);
                _contributionRepositoryService.AddContribution(request, response.Items);

                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}