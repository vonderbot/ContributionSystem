using Microsoft.AspNetCore.Mvc;
using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.BusinessLogic.Interfaces;
using System;

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
        public IActionResult GetRequestsHistory(RequestGetRequestsHistoryContrbutionViewModel request)
        {
            try
            {
                var response = _contributionService.GetRequestsHistory(request);

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Calculate(RequestCalculateContributionViewModel request)
        {
            try
            {
                var response = _contributionService.Calculate(request);
                _contributionService.AddContribution(request, response.Items);

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}