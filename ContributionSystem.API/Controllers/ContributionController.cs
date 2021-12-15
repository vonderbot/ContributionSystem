using Microsoft.AspNetCore.Mvc;
using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ContributionSystem.API.Controllers
{
    /// <summary>
    /// Provides endpoints to work with Contribution.
    /// </summary>
    [ApiController]
    [Route("/api/[controller]/[action]")]
    [Authorize]
    public class ContributionController : ControllerBase
    {
        private readonly IContributionService _contributionService;
        private readonly IUserService _userService;

        /// <summary>
        /// Creates a new instance of <see cref="ContributionController" />.
        /// </summary>
        /// <param name="сontributionService"><see cref="IContributionService" /> instance.</param>
        /// <param name="userService"><see cref="IUserService" /> instance.</param>
        public ContributionController(IContributionService сontributionService, IUserService userService)
        {
            _contributionService = сontributionService;
            _userService = userService;
        }

        /// <summary>
        /// Gets months information for contribution by specified identifier.
        /// </summary>
        /// <param name="id">Contribution identifier.</param>
        /// <returns><see cref="OkObjectResult" /> with response model, wich provides calculation information per months.</returns>
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
        /// <param name="request">Request model with specified parameters to retriev calculation history.</param>
        /// <returns><see cref="OkObjectResult" /> with response model, wich provides a list of records.</returns>
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
        /// Calculates new contribution and saves it to the system.
        /// </summary>
        /// <param name="request">Request model with information for calculation.</param>
        /// <returns><see cref="OkObjectResult" /> with response model, wich provides calculation result.</returns>
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