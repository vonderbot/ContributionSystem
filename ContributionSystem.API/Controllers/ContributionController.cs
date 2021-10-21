using Microsoft.AspNetCore.Mvc;
using ContributionSystem.ViewModels.Models.Contribution;
using ContributionSystem.BusinessLogic.Interfaces;
using System;

namespace ContributionSystem.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class ContributionController : ControllerBase
    {
        private readonly IContributionService _contributionService;
        private readonly IContributionRepositoryService _contributionRepositoryService;

        public ContributionController(IContributionService сontributionService, IContributionRepositoryService contributionRepositoryService)
        {
            _contributionService = сontributionService;
            _contributionRepositoryService = contributionRepositoryService;
        }

        //[HttpGet]
        //public IActionResult GetContributions()
        //{
        //    try
        //    {
        //        var list = _contributionRepositoryService.GetContributionList();
        //        return Ok();
        //    }
        //    catch
        //    {
        //        return BadRequest("BadRequest");
        //    }
        //}

        [HttpPost]
        public IActionResult Calculate(RequestCalculateContributionViewModel request)
        {
            try
            {
                var response = _contributionService.Calculate(request);
                _contributionRepositoryService.AddContribution(request, response.Items);
                return Ok(response);
            }
            catch(Exception exception)
            {
                return BadRequest("BadRequest");
            }
        }
    }
}