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

namespace meeting_keeper.Tests.Controllers
{
    public class DashboardsController
    {

        private meeting_keeper.Controllers.DashboardsController _dashboardController;
        private readonly IServiceProvider _serviceProvider;
        private ApplicationDbContext _dbContext;

        public DashboardsController()
        {
            var services = new ServiceCollection();

            services.AddEntityFramework()
                    .AddInMemoryDatabase()
                    .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());

            _serviceProvider = services.BuildServiceProvider();

            _dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            _dashboardController = new meeting_keeper.Controllers.DashboardsController(_dbContext);
        }

        [Fact]
        public void ReturnsIndex()
        {
            // Act
            var result = _dashboardController.Index();

            // Assert
            Assert.NotNull(result);
        }

    }
}
