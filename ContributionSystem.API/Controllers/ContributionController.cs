using ContributionSystem.BusinesLogic.Services;
using ContributionSystem.BusinesLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ContributionSystem.ViewModels.Models.Contribution;

namespace ContributionSystem.API.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("api/Contribution")]
    public class ContributionController : ControllerBase
    {
        private readonly IContributionService postContributionService;

        public ContributionController()
        {
            postContributionService = new ContributionService();
        }

        [HttpPost("action")]
        [Route("api/contribution/calculate")]
        public IActionResult Calculate([FromBody] RequestCalculateContributionViewModel request)
        {
            ResponseCalculateContributionViewModel response = postContributionService.Calculate(request);

            return Ok(response);
        }
    }
}