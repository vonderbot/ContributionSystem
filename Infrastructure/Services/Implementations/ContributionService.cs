using ContributionSystem.Core.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContributionSystem.Infrastructure.Services.Implementations
{
    public class ContributionService
    {
        private readonly Contribution curentContribution;
        public ContributionService(Contribution newContribution)
        {
            curentContribution = newContribution;
        }
        public Dictionary<string, string> MonthInfo(int monthNumber)
        {
            Dictionary<string, string> info = new Dictionary<string, string>(3);
            info.Add("Month number", monthNumber.ToString());
            decimal income = Math.Round((GetStartValue() / 100 * ((decimal)GetPercent()/12)), 2);
            info.Add("Income", income.ToString());
            info.Add("Sum", (GetStartValue() + income * monthNumber).ToString());
            return info;
        }
        public Dictionary<string, string> GetInfo()
        {
            Dictionary<string, string> info = new Dictionary<string, string>(3);
            info.Add("Start value", GetStartValue().ToString());
            info.Add("Term", GetTerm().ToString());
            info.Add("Percent", GetPercent().ToString());
            return info;
        }
        public decimal GetStartValue()
        {
            return curentContribution.StartValue;
        }
        public int GetTerm()
        {
            return curentContribution.Term;
        }
        public int GetPercent()
        {
            return curentContribution.Percent;
        }
    }
}
