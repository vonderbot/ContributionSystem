using ContributionSystem.Entities.Entities;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContributionSystem.ViewModels.Models.Contribution;

namespace ContributionSystem.BusinesLogic.Interfaces
{
    public interface IContributionService
    {
        public ResponseCalculateContributionViewModel SimpleCalculate(RequestCalculateContributionViewModel request);

        public ResponseCalculateContributionViewModel ComplexCalculate(RequestCalculateContributionViewModel request);
    }
}
