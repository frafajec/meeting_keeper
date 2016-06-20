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
using Newtonsoft.Json;

namespace meeting_keeper.Tests.Controllers
{
    public class CalendarController
    {

        private readonly IServiceProvider _serviceProvider;
        private meeting_keeper.Controllers.CalendarController _calendarController;
        private meeting_keeper.Controllers.CalendarEntriesController _calendarEntriesController;
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
            _calendarEntriesController = new meeting_keeper.Controllers.CalendarEntriesController(_dbContext);
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
        public void createEvent()
        {
            //Arrange
            CalendarEntry entry = new CalendarEntry
            {
                id = 345,
                name = "NEW event",
                calendarID = 1,
                title = "NEW event",
                start = 123456789,
                end = 0,
                allDay = true,
                color = "FF00CC"
            };
            string eventJson = JsonConvert.SerializeObject(entry);
            List<CalendarEntry> beforeEntries = _dbContext.CalendarEntry.ToList();

            //Act
            var result = _calendarController.createEvent(eventJson);
            List<CalendarEntry> afterEntries = _dbContext.CalendarEntry.ToList();

            //Assert
            result.Should().BeOfType<IActionResult>();
            Assert.NotEqual(beforeEntries.Count, afterEntries.Count);
        }


        [Fact]
        public void editEvent()
        {
            //Arrange
            CalendarEntry entry = new CalendarEntry
            {
                id = 222,
                name = "Editable event",
                calendarID = 1,
                title = "Editable event",
                start = 123456789,
                end = 0,
                allDay = true,
                color = "FF00CC"
            };
            _dbContext.CalendarEntry.Add(entry);
            _dbContext.SaveChanges();

            List<CalendarEntry> beforeEntries = _dbContext.CalendarEntry.ToList();

            entry.allDay = false;
            entry.color = "AA00AA";
            string eventJson = JsonConvert.SerializeObject(entry);

            //Act
            var result = _calendarController.editEvent(eventJson);
            List<CalendarEntry> afterEntries = _dbContext.CalendarEntry.ToList();

            //Assert
            result.Should().BeOfType<IActionResult>();
            Assert.Equal(beforeEntries.Count, afterEntries.Count);
        }


        [Fact]
        public void removeEvent()
        {
            //Arrange
            CalendarEntry entry = new CalendarEntry
            {
                id = 222,
                name = "Editable event",
                calendarID = 1,
                title = "Editable event",
                start = 123456789,
                end = 0,
                allDay = true,
                color = "FF00CC"
            };
            _dbContext.CalendarEntry.Add(entry);
            _dbContext.SaveChanges();

            List<CalendarEntry> beforeEntries = _dbContext.CalendarEntry.ToList();
            string eventJson = JsonConvert.SerializeObject(entry);

            //Act
            var result = _calendarController.removeEvent(eventJson);
            List<CalendarEntry> afterEntries = _dbContext.CalendarEntry.ToList();

            //Assert
            result.Should().BeOfType<IActionResult>();
            Assert.NotEqual(beforeEntries.Count, afterEntries.Count);
        }


        [Fact]
        public void readSettings()
        {
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
        public void getSetings()
        {
            //Act
            var result = _calendarController.getSettings();

            //Assert
            result.Should().BeOfType<IActionResult>();
        }


        [Fact]
        public void saveSettings()
        {
            //Arrange
            Calendar calendar = new Calendar
            {
                userID = "1",
                id = 1,
                showSaturday = true,
                showSunday = false
            };
            _dbContext.Calendar.Add(calendar);
            _dbContext.SaveChanges();

            calendar.showSaturday = false;
            string eventJson = JsonConvert.SerializeObject(calendar);

            //Act
            var result = _calendarController.saveSettings(eventJson);

            //Assert
            result.Should().BeOfType<IActionResult>();
        }


        [Fact]
        public void createDefaultCalendar()
        {
            //Arrange
            List<Calendar> beforeEntries = _dbContext.Calendar.ToList();

            //Act
            var result = _calendarController.createDefaultCalendar();
            List<Calendar> afterEntries = _dbContext.Calendar.ToList();

            //Assert
            Assert.NotEqual(beforeEntries.Count, afterEntries.Count);
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
