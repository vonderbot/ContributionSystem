using Microsoft.AspNetCore.Mvc;
using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ContributionSystem.API.Controllers
{
    /// <summary>
    /// Provides methods for work with request to ContributionService.
    /// </summary>
    [ApiController]
    [Route("/api/[controller]/[action]")]
    [Authorize]
    public class ContributionController : ControllerBase
    {
        private readonly IContributionService _contributionService;
        private readonly IUserService _userService;

        /// <summary>
        /// ContributionController constructor.
        /// </summary>
        /// <param name="сontributionService">IContributionService instance.</param>
        /// <param name="userService">IUserService instance.</param>
        public ContributionController(IContributionService сontributionService, IUserService userService)
        {
            _contributionService = сontributionService;
            _userService = userService;
        }

        /// <summary>
        /// Gets months info for contributions.
        /// </summary>
        /// <param name="id">Contribution id.</param>
        /// <returns>OkObjectResult with responce model.</returns>
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

        /// <summary>
        /// Gets user calculations history.
        /// </summary>
        /// <param name="request">Request model with info to get a part of records.</param>
        /// <returns>OkObjectResult with responce model.</returns>
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

        /// <summary>
        /// Calculates new contribution and adds it to db.
        /// </summary>
        /// <param name="request">Request model with info for calculation.</param>
        /// <returns>OkObjectResult with responce model.</returns>
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