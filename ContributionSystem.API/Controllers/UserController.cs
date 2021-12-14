using Microsoft.AspNetCore.Mvc;
using ContributionSystem.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ContributionSystem.ViewModels.Models.User;

namespace ContributionSystem.API.Controllers
{
    /// <summary>
    /// Provides methods for work with request to UserService.
    /// </summary>
    [ApiController]
    [Route("/api/[controller]/[action]")]
    [Authorize(Roles = "UserAdmin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// UserController constructor.
        /// </summary>
        /// <param name="userService">IUserService instance.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Сhanges user status.
        /// </summary>
        /// <param name="request">Request model with user info.</param>
        /// <returns>OkObjectResult</returns>
        [HttpPost]
        public async Task<IActionResult> ChangeUserStatus(RequestChangeUserStatusUserViewModel request)
        {
            try
            {
                await _userService.ChangeUserStatus(request);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>OkObjectResult with responce model.</returns>
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
    }
}