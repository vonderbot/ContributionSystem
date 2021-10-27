using Microsoft.AspNetCore.Mvc;
using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> GetHistory([FromQuery]RequestGetRequestsHistoryContributionViewModel request)
        {
            try
            {
                var response = await _contributionService.GetHistory(request);

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Calculate(RequestCalculateContributionViewModel request)
        {
            try
            {
                var response = await _contributionService.Calculate(request);

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}