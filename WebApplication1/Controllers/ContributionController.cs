using ContributionSystem.Infrastructure.NewFolder;
using ContributionSystem.Infrastructure.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContributionSystem.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContributionController : ControllerBase
    {
        [HttpGet]
        public Dictionary<string, string> Get()
        {
            InicializationService inicializationService = new();
            ContributionService contributionService = new(inicializationService.Create(0, 0, 0));
            Dictionary<string, string>  obj = contributionService.GetInfo();
            return obj;
        }
        [HttpPost]
        public IEnumerable<Dictionary<string, string>> Post(decimal newValue, int newTerm, int newPercent)
        {
            InicializationService inicializationService = new();
            ContributionService contributionService = new(inicializationService.Create(newValue, newTerm, newPercent));
            //Dictionary<string, string> obj = contributionService.GetInfo();
            Dictionary<string, string>[] AllMonthsInfo = new Dictionary<string, string>[contributionService.GetTerm()];
            for (int i = 0; i < contributionService.GetTerm();i++)
            {
                AllMonthsInfo[i] = contributionService.MonthInfo(i+1);
            }
            return AllMonthsInfo;
        }
    }
}
