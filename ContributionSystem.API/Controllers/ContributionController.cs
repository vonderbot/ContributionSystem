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
        IPostContributionService PostContributionService = new PostContributionService();

        [HttpPost]
        public IActionResult Calculate(RequestPostContributionViewModel request)
        {
            try
            {
                ResponsePostContributionViewModel response = PostContributionService.Calculate(request);

                return Ok(response);
            }
            catch(Exception ex)
            {
                return ValidationProblem(ex.Message);
            };
        }
    }
}
