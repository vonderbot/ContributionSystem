using ContributionSystem.Core.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContributionSystem.Infrastructure.NewFolder
{
    public class InicializationService
    {
        public Contribution Create(decimal newValue, int newTerm, int newPercent)
        {
            newValue = Math.Round(newValue, 2);
            Contribution newContribution = new()
            {
                StartValue = newValue,
                Term = newTerm,
                Percent = newPercent
            };
            return newContribution;
        }
    }
}
