using ContributionSystem.API.Controllers;
using ContributionSystem.BusinessLogic.Services;
using ContributionSystem.DataAccess.Contexts;
using ContributionSystem.DataAccess.Repositories;
using ContributionSystem.Entities.Entities;
using ContributionSystem.ViewModels.Enums;
using ContributionSystem.ViewModels.Models.Contribution;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace ContributionSystem.API.UnitTests.Contollers
{
    public class ContributionControllerTests
    {
        //private readonly ContributionController contributionController;
        //private const int CorrectStartValue = 1;
        //private const int CorrectTerm = 3;
        //private const int CorrectPercent = 100;

        //public ContributionControllerTests()
        //{
        //    contributionController = new ContributionController(new ContributionService(new ContributionRepository(new TestDbContext())));
        //    var options = new DbContextOptionsBuilder<ContributionDbContext>().UseInMemoryDatabase(databaseName: "Products Test").Options;
        //}

        //[Test]
        //public async void Calculate_ValidRequest_OkObjectResultWithResponseCalculateContributionViewModel()
        //{
        //    var response = await contributionController.Calculate(GetCalculationRequest(CalculationMethodEnumView.Simple, CorrectStartValue, CorrectTerm, CorrectPercent));
        //    var okObjectResult = response as OkObjectResult;
        //    okObjectResult.Should().NotBeNull();
        //    okObjectResult.Value.Should().BeOfType<ResponseCalculateContributionViewModel>();
        //}

        //[Test]
        //public async void Calculate_NullRequest_ThrowException()
        //{
        //    var response = await contributionController.Calculate(null);
        //    var BadRequestObjectResult = response as BadRequestObjectResult;
        //    BadRequestObjectResult.Should().NotBeNull();
        //    BadRequestObjectResult.StatusCode.ToString().Should().BeEquivalentTo("400");

        //}

        //private RequestCalculateContributionViewModel GetCalculationRequest(CalculationMethodEnumView calculationMethod, decimal startValue, int term, decimal percent)
        //{
        //    var request = new RequestCalculateContributionViewModel
        //    {
        //        CalculationMethod = calculationMethod,
        //        StartValue = startValue,
        //        Term = term,
        //        Percent = percent
        //    };

        //    return request;
        //}
    }

    public class TestDbContext : DbContext
    {
        public DbSet<Contribution> Contribution { get; set; }

        public DbSet<MonthInfo> MonthInfo { get; set; }
    }
}