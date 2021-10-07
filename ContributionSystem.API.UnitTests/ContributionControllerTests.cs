using ContributionSystem.API.Controllers;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;

namespace ContributionSystem.API.UnitTests
{
    public class ContributionControllerTests
    {
        private ContributionController ñontributionController;

        public ContributionControllerTests()
        {
            ñontributionController = new ContributionController();
        }

        [Test]
        public void Calculate_ValidRequest_OkObjectResultWithResponseCalculateContributionViewModel()
        {
            //arrange
            var request = new RequestCalculateContributionViewModel
            {
                CalculationMethod = CalculationMethodEnumView.Simple,

                StartValue = 1,

                Term = 3,

                Percent = 100
            };

            //act
            var response = ñontributionController.Calculate(request);
            var okObjectResult = response as OkObjectResult;

            //assert
            okObjectResult.Should().NotBeNull();
            okObjectResult.Value.Should().BeOfType<ResponseCalculateContributionViewModel>();
        }

        [Test]
        public void Calculate_NullRequest_ThrowException()
        {
            //arrange
            RequestCalculateContributionViewModel request = null;

            //act
            Action act = () => ñontributionController.Calculate(request);

            //assert
            act.Should().Throw<Exception>()
                .WithMessage("Null request");
        }
    }
}