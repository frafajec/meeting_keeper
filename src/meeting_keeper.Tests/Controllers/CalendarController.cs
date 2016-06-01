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
    public class CalendarController
    {

        private readonly IServiceProvider _serviceProvider;
        private meeting_keeper.Controllers.CalendarController _calendarController;
        public CalendarController()
        {
            var services = new ServiceCollection();

            services.AddEntityFramework()
                    .AddInMemoryDatabase()
                    .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());

            _serviceProvider = services.BuildServiceProvider();

            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            _calendarController = new meeting_keeper.Controllers.CalendarController(dbContext);
        }
    

        [Fact]
        public void ReturnsView()
        {
            // Act
            var result = _calendarController.Index();

            // Assert
            Assert.NotNull(result);
        }

    }

}
