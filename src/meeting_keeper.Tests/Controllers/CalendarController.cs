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
using System.Security.Claims;

namespace meeting_keeper.Tests.Controllers
{
    public class CalendarController
    {

        private readonly IServiceProvider _serviceProvider;
        private meeting_keeper.Controllers.CalendarController _calendarController;
        private ApplicationDbContext _dbContext;
        public CalendarController()
        {
            var services = new ServiceCollection();

            services.AddEntityFramework()
                    .AddInMemoryDatabase()
                    .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());

            _serviceProvider = services.BuildServiceProvider();

            _dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            _calendarController = new meeting_keeper.Controllers.CalendarController(_dbContext);
        }


        [Fact]
        public void RedirectsView()
        {
            //Arrange
            //TODO get user NOT logged in


            // Act
            var result = (ViewResult)_calendarController.Index();

            // Assert
            Assert.Equal("Index", result.ViewName);
            
            //Assert.NotNull(result);

        }

        [Fact]
        public void ReturnsView()
        {
            // Act
            //TODO get user logged in
            var result = _calendarController.Index();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void initCalendarCreateCalendar()
        {
            //Arrange
            //TODO get user logged in

            //Act
            var result = _calendarController.initCalendar();


            //Assert
            result.Should().BeOfType<JsonResult>();
        }

        
        [Fact]
        public void initCalendarReadCalendar()
        {
            //Arrange
            //TODO get user logged in
            
            GenFu.GenFu.Configure<Calendar>()
                .Fill(p => p.id, () => 1)
                .Fill(p => p.dateCreated, () => 1 )
                .Fill(p => p.dateModified, () => 1 )
                .Fill(p => p.name, () => "Test calendar" )
                .Fill(p => p.userID, () => "MISSING USER ID" )
                .Fill(p => p.showSaturday, () => true)
                .Fill(p => p.showSunday, () => false);

            var calendars = GenFu.GenFu.ListOf<Calendar>(1);
            _dbContext.Calendar.AddRange(calendars);
            _dbContext.SaveChanges();


            //Act
            var result = _calendarController.initCalendar();

            //Assert
            result.Should().BeOfType<JsonResult>();
        }


        [Fact]
        public void readSettings()
        {
            //Arrange
            //TODO get user logged in

            Calendar calendar = new Calendar {
                id = 1,
                dateCreated = 0,
                dateModified = 0,
                name = "Test calendar",
                userID = "1",
                showSaturday = true,
                showSunday = false
            };

            //Act
            var result = _calendarController.readSettings(calendar);

            //Assert
            result.Should().BeOfType<JsonResult>()
                  .Which.Value.Should().NotBeNull();
        }


        [Fact]
        public void isUnixTime()
        {
            //Act
            var result = _calendarController.unixTimeNow();

            //Assert
            Assert.NotNull(result);
        }


    }

}
