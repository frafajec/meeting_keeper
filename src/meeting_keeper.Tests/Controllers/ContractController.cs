using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using meeting_keeper.Models;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;

namespace meeting_keeper.Tests.Controllers
{
    public class ContractController
    {

        private readonly IServiceProvider _serviceProvider;
        private meeting_keeper.Controllers.ContractController _contractController;
        private ApplicationDbContext _dbContext;

        public ContractController()
        {
            var services = new ServiceCollection();

            services.AddEntityFramework()
                    .AddInMemoryDatabase()
                    .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());

            _serviceProvider = services.BuildServiceProvider();

            _dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            CreateTestData(_dbContext);
            _contractController = new meeting_keeper.Controllers.ContractController(_dbContext);
        }

        private void CreateTestData(ApplicationDbContext dbContext)
        {
            var id = 1;
            GenFu.GenFu.Configure<Contract>()
                .Fill(p => p.id, () => id++)
                .Fill(p => p.dateCreated, () =>
                {
                    return 1;
                });

            var contracts = GenFu.GenFu.ListOf<Contract>(20);

            dbContext.Contract.AddRange(contracts);
            dbContext.SaveChanges();
        }


        [Fact]
        public void ReturnsView()
        {
            // Act
            var result = _contractController.Index();

            // Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void DetailsNullIdGiven()
        {
            // Act
            var result = _contractController.Details(null);

            // Assert
            result.Should().BeOfType<HttpNotFoundResult>();
        }


        [Fact]
        public void DetailsNonexistentIdGiven()
        {
            // Act
            var result = _contractController.Details(21);

            // Assert
            result.Should().BeOfType<HttpNotFoundResult>();
        }


        [Fact]
        public void DetailsCorrectIdGiven()
        {
            // Act
            var contractID = _dbContext.Contract.First().id;
            var actionResult = _contractController.Details(contractID);

            // Assert
            actionResult.Should().BeOfType<ViewResult>()
                                 .Which.ViewData.Model.Should().BeOfType<Contract>()
                                 .Which.id.Should().Be(contractID);
        }

    }

}
