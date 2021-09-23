using ContributionSystem.BusinesLogic.Services;
using ContributionSystem.BusinesLogic.Interfaces;
using ContributionSystem.ViewModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContributionSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContributionController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(RequestPostContributionViewModel request)
        {
            IContributionService contributionService = new ContributionService();
            ResponsePostContributionViewModel response = new ResponsePostContributionViewModel() { items = contributionService.MonthsInfo(request) };

            return Ok(response);
        }
    }
}
