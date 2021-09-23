using ContributionSystem.Entities.Entities;
using ContributionSystem.ViewModels.Models;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContributionSystem.BusinesLogic.Interfaces
{
    public interface IContributionService
    {
        public ArrayList MonthsInfo(RequestPostContributionViewModel request);
    }
}
