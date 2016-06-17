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

    public class ClientController
    {

        private readonly IServiceProvider _serviceProvider;
        private meeting_keeper.Controllers.ClientController _clientController;
        public ClientController()
        {
            var services = new ServiceCollection();

            services.AddEntityFramework()
                    .AddInMemoryDatabase()
                    .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());

            _serviceProvider = services.BuildServiceProvider();

            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            CreateTestData(dbContext);
            _clientController = new meeting_keeper.Controllers.ClientController(dbContext);
        }

        private void CreateTestData(ApplicationDbContext dbContext)
        {
            var id = 1;
            GenFu.GenFu.Configure<Client>()
                .Fill(p => p.id, () => id++)
                .Fill(p => p.dateCreated, () =>
                {
                    return 1;
                });

            var clients = GenFu.GenFu.ListOf<Client>(20);

            dbContext.Client.AddRange(clients);
            dbContext.SaveChanges();
        }


        [Fact]
        public void ReturnsView()
        {
            // Act
            var result = _clientController.Index();

            // Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void DetailsNullIdGiven()
        {
            // Act
            var result = _clientController.Details(null);

            // Assert
            result.Should().BeOfType<HttpNotFoundResult>();
        }


        [Fact]
        public void DetailsNonexistentIdGiven()
        {
            // Act
            var result = _clientController.Details(21);

            // Assert
            result.Should().BeOfType<HttpNotFoundResult>();
        }


        [Fact]
        public void DetailsCorrectIdGiven()
        {
            // Act
            var actionResult = _clientController.Details(1);

            // Assert
            actionResult.Should().BeOfType<ViewResult>()
                                 .Which.ViewData.Model.Should().BeOfType<Client>()
                                 .Which.id.Should().Be(1);
        }

    }

}