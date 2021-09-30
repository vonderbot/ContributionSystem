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
using ContributionSystem.ViewModels.Models.Contribution;

namespace ContributionSystem.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ContributionController : ControllerBase
    {
        private readonly IContributionService postContributionService;

        public ContributionController()
        {
            postContributionService = new ContributionService();
        }

        [HttpGet]
        [Route("/api/[controller]/get")]
        public IActionResult Get()
        {

            return Ok("conected");
        }

        [HttpPost]
        [Route("/api/[controller]/SimplCalculate")]
        public IActionResult SimplCalculate(RequestCalculateContributionViewModel request)
        {
            ResponseCalculateContributionViewModel response = postContributionService.SimplCalculate(request);

            return Ok(response);
        }

        [HttpPost]
        [Route("/api/[controller]/ComplexCalculate")]
        public IActionResult ComplexCalculate(RequestCalculateContributionViewModel request)
        {
            ResponseCalculateContributionViewModel response = postContributionService.ComplexCalculate(request);

            return Ok(response);
        }
    }
}