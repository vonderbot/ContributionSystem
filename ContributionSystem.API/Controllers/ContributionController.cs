using ContributionSystem.BusinesLogic.Services;
using ContributionSystem.BusinesLogic.Interfaces;
using ContributionSystem.ViewModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ContributionSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContributionController : ControllerBase
    {
        private readonly IContributionService postContributionService;

        public ContributionController()
        {
            postContributionService = new ContributionService();
        }

        [HttpPost]
        public IActionResult Calculate(RequestCalculateContributionViewModel request)
        {
            ResponseCalculateContributionViewModel response = postContributionService.Calculate(request);

            return Ok(response);
        }
    }
}