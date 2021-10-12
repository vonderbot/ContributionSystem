using ContributionSystem.API.Controllers;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;

namespace ContributionSystem.API.UnitTests
{
    public class ContributionControllerTests
    {
        private readonly ContributionController contributionController;
        private const int _�orrectStartValue = 1;
        private const int _�orrectTerm = 3;
        private const int _�orrectPercent = 100;

        public ContributionControllerTests()
        {
            contributionController = new ContributionController();
        }

        [Test]
        public void Calculate_ValidRequest_OkObjectResultWithResponseCalculateContributionViewModel()
        {
            var response = contributionController.Calculate(GetCalculationRequest(CalculationMethodEnumView.Simple, _�orrectStartValue, _�orrectTerm, _�orrectPercent));
            var okObjectResult = response as OkObjectResult;
            okObjectResult.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<ResponseCalculateContributionViewModel>();
        }

        [Test]
        public void Calculate_NullRequest_ThrowException()
        {
            RequestCalculateContributionViewModel request = null;
            Action act = () => contributionController.Calculate(request);
            act.Should().Throw<Exception>().WithMessage("Null request");
        }

        private static RequestCalculateContributionViewModel GetCalculationRequest(CalculationMethodEnumView calculationMethod, decimal startValue, int term, decimal percent)
        {
            var request = new RequestCalculateContributionViewModel
            {
                CalculationMethod = calculationMethod,
                StartValue = startValue,
                Term = term,
                Percent = percent
            };

            return request;
        }
    }
}