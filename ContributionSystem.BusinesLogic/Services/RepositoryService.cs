using ContributionSystem.BusinessLogic.Interfaces;
using ContributionSystem.DataAccess.Interfaces;
using ContributionSystem.Entities.Entities;
using ContributionSystem.ViewModels.Models.Contribution;
using System;
using System.Collections.Generic;
using ContributionSystem.Entities.Enums;
using ContributionSystem.ViewModels.Enums;

namespace ContributionSystem.BusinessLogic.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly IContributionRepository _contributionRepository;
        private readonly IMonthInfoRepository _monthInfoRepository;

        public RepositoryService(IContributionRepository contributionRepository, IMonthInfoRepository monthInfoRepository)
        {
            _contributionRepository = contributionRepository;
            _monthInfoRepository = monthInfoRepository;
        }

        public IEnumerable<RequestCalculateContributionViewModel> GetRequestsHistory(RequestGetRequestsHistoryContrbutionViewModel request)
        {
            var contributions = _contributionRepository.GetContributions(request.NumberOfContrbutionsForLoad, request.NumberOfContrbutionsForSkip);
            var Requests = new List<RequestCalculateContributionViewModel>();

            foreach (var element in contributions)
            {
                Requests.Add(CreateRequestFromContribution(element));
            }

            return Requests;
        }

        public void AddContribution(RequestCalculateContributionViewModel request, IEnumerable<ResponseCalculateContributionViewModelItem> responseItems)
        {
            if (!Enum.IsDefined(typeof(CalculationMethodEnum), (int)request.CalculationMethod))
            {
                throw new Exception("Incorrect calculation method");
            }
            var contribution = CreateContributionFromRequest(request);
            var monthsInfo = new List<MonthInfo>();

            foreach (var element in responseItems)
            {
                monthsInfo.Add(CreateMonthInfoFromResponseItem(element, contribution));
            }
            _contributionRepository.Create(contribution);
            _monthInfoRepository.Create(monthsInfo);
        }

        private static MonthInfo CreateMonthInfoFromResponseItem(ResponseCalculateContributionViewModelItem responseItems, Contribution contribution)
        {
            return new MonthInfo()
            {
                MonthNumber = responseItems.MonthNumber,
                Income = responseItems.Income,
                Sum = responseItems.Sum,
                Contribution = contribution
            };
        }

        private static Contribution CreateContributionFromRequest(RequestCalculateContributionViewModel request) 
        {
            return new Contribution()
            {
                StartValue = request.StartValue,
                Term = request.Term,
                Percent = request.Percent,
                Date = request.Date,
                CalculationMethod = (CalculationMethodEnum)(int)request.CalculationMethod
            };
        }

        private static RequestCalculateContributionViewModel CreateRequestFromContribution(Contribution contribution)
        {
            return new RequestCalculateContributionViewModel()
            {
                StartValue = contribution.StartValue,
                Term = contribution.Term,
                Percent = contribution.Percent,
                Date = contribution.Date,
                CalculationMethod = (CalculationMethodEnumView)(int)contribution.CalculationMethod
            };
        }
    }
}
