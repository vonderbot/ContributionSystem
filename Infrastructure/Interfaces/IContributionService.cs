using ContributionSystem.Entities.Entities;
using ContributionSystem.ViewModels.Interfaces;
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
        //public decimal GetStartValue(Contribution contribution);
        //public int GetTerm(Contribution contribution);
        //public int GetPercent(Contribution contribution);
    }
}
