using Microsoft.AspNetCore.Mvc;
using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

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
        public async Task<IActionResult> GetDetailsById([FromQuery]int id)
        {
            try
            {
                var response = await _contributionService.GetDetailsById(id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetHistory([FromQuery]RequestGetHistoryContributionViewModel request)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
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