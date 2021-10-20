using ContributionSystem.BusinessLogic.Interfaces;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;
using ContributionSystem.ViewModels.Models.Contribution;
using System;

namespace ContributionSystem.BusinessLogic.Services
{
    public class ContributionRepositoryService : IContributionRepositoryService
    {
        private readonly IContributionRepository _contributionRepository;

        public ContributionRepositoryService(IContributionRepository newContributionRepository)
        {
            _contributionRepository = newContributionRepository;
        }

        public void AddContribution(RequestCalculateContributionViewModel request)
        {
            var contribution = new Contribution()
            {
                StartValue = request.StartValue,
                Term = request.Term,
                Percent = request.Percent,
                Date = DateTime.Now.Date
            };
            _contributionRepository.Create(contribution);
        }
    }
}
