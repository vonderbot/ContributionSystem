using Microsoft.AspNetCore.Mvc;
using ContributionSystem.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ContributionSystem.ViewModels.Models.User;

namespace ContributionSystem.API.Controllers
{
    /// <summary>
    /// Provides endpoints to work with User.
    /// </summary>
    [ApiController]
    [Route("/api/[controller]/")]
    [Authorize(Roles = "UserAdmin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Creates a new instance of <see cref="UserController" />.
        /// </summary>
        /// <param name="userService"><see cref="IUserService" /> instance.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Сhanges user status.
        /// </summary>
        /// <param name="request">Request model with user information.</param>
        /// <returns><see cref="OkObjectResult" /></returns>
        [HttpPost]
        [Route("change-user-status")]
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
        /// <returns><see cref="OkObjectResult" /> with response model, wich provides a list of users.</returns>
        [HttpGet]
        [Route("get-users-list")]
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