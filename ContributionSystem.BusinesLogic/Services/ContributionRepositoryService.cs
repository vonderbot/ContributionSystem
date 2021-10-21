using ContributionSystem.BusinessLogic.Interfaces;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;
using ContributionSystem.ViewModels.Models.Contribution;
using System;
using System.Collections.Generic;
using ContributionSystem.Entities.Enums;

namespace ContributionSystem.BusinessLogic.Services
{
    public class ContributionRepositoryService : IContributionRepositoryService
    {
        private readonly IContributionRepository _contributionRepository;

        public ContributionRepositoryService(IContributionRepository newContributionRepository)
        {
            _contributionRepository = newContributionRepository;
        }

        public void AddContribution(RequestCalculateContributionViewModel request, IEnumerable<ResponseCalculateContributionViewModelItem> ResponseItems)
        {
            if (!Enum.IsDefined(typeof(CalculationMethodEnum), (int)request.CalculationMethod))
            {
                throw new Exception("Incorrect calculation method");
            }
            var contribution = CreateContributionFromRequest(request);
            var details = new List<MonthInfo>();
            foreach (var element in ResponseItems)
            {
                details.Add(CreateMonthInfoFromResponseItem(element, contribution));
            }
            _contributionRepository.Create(contribution, details);
        }

        private MonthInfo CreateMonthInfoFromResponseItem(ResponseCalculateContributionViewModelItem ResponseItems, Contribution contribution)
        {
            return new MonthInfo()
            {
                MonthNumber = ResponseItems.MonthNumber,
                Income = ResponseItems.Income,
                Sum = ResponseItems.Sum,
                Contribution = contribution
            };
        }

        private Contribution CreateContributionFromRequest(RequestCalculateContributionViewModel request) 
        {
            return new Contribution()
            {
                StartValue = request.StartValue,
                Term = request.Term,
                Percent = request.Percent,
                Date = DateTime.Now.Date,
                CalculationMethod = (CalculationMethodEnum)(int)request.CalculationMethod
            };
        }

        //public IEnumerable<Contribution> GetContributionList()
        //{
        //    return _contributionRepository.GetContributionList();
        //}
    }
}
