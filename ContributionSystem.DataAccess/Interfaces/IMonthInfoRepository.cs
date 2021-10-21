using ContributionSystem.Entities.Entities;
using System.Collections.Generic;

namespace ContributionSystem.DataAccess.Interfaces
{
    public interface IMonthInfoRepository
    {
        void Create(IEnumerable<MonthInfo> details);
    }
}
