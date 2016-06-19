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
    public class CalendarEntriesController
    {
        private readonly IServiceProvider _serviceProvider;
        private meeting_keeper.Controllers.CalendarEntriesController _calendarEntriesController;
        public CalendarEntriesController()
        {
            var services = new ServiceCollection();

            services.AddEntityFramework()
                    .AddInMemoryDatabase()
                    .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());

            _serviceProvider = services.BuildServiceProvider();

            var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            _calendarEntriesController = new meeting_keeper.Controllers.CalendarEntriesController(dbContext);
        }

    }
}
