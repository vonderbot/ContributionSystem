using Microsoft.AspNetCore.Mvc;
using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ContributionSystem.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    [Authorize]
    public class ContributionController : ControllerBase
    {
        private readonly IContributionService _contributionService;
        private readonly IUserService _userService;

        public ContributionController(IContributionService сontributionService, IUserService userService)
        {
            _contributionService = сontributionService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersList()
        {
            try
            {
                var response = await _userService.GetUsersList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        public async Task<IActionResult> GetHistoryByUserId([FromQuery]RequestGetHistoryByUserIdContributionViewModel request)
        {
            try
            {
                request.UserId = _userService.GetUserId(ControllerContext.HttpContext.User);
                var response = await _contributionService.GetHistoryByUserId(request);

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
                request.UserId = _userService.GetUserId(ControllerContext.HttpContext.User);
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