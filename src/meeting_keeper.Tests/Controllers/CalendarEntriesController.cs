using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using meeting_keeper.Models;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Newtonsoft.Json;

namespace meeting_keeper.Tests.Controllers
{
    public class CalendarEntriesController
    {
        private readonly IServiceProvider _serviceProvider;
        private meeting_keeper.Controllers.CalendarEntriesController _calendarEntriesController;
        private ApplicationDbContext _dbContext;
        private readonly ITestOutputHelper _output;
        public CalendarEntriesController(ITestOutputHelper output)
        {
            var services = new ServiceCollection();

            services.AddEntityFramework()
                    .AddInMemoryDatabase()
                    .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase());

            _serviceProvider = services.BuildServiceProvider();

            _dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            _calendarEntriesController = new meeting_keeper.Controllers.CalendarEntriesController(_dbContext);

            _output = output;

            arrangeEntries(20);
        }

        public List<CalendarEntry> arrangeEntries(int nbr)
        {
            List<CalendarEntry> list = new List<CalendarEntry>();

            for (int i = 1; i < nbr + 1; i++)
            {
                list.Add(new CalendarEntry {
                    id = i,
                    calendarID = i < 11 ? 1 : 2,
                    start = 123456789,
                    end = 0,
                    title = "Event no." + i,
                    allDay = i % 3 == 0 ? true : false,
                    color = "FF00AA"
                });
            }

            _dbContext.CalendarEntry.AddRange(list);
            _dbContext.SaveChanges();

            return list;
        }


        [Fact]
        public void readEvents()
        {
            //Arrange
            //Constructor arranged


            //Act
            JsonResult result =  _calendarEntriesController.readEvents(1);
            List<object> events = new List<object>();
            events = (List<object>)result.Value;
            

            //Assert
            Assert.Equal(10, events.Count);
        }


        [Fact]
        public void readEventsWrongID()
        {
            //Arrange
            //contructor arranged


            //Act
            JsonResult result = _calendarEntriesController.readEvents(999);
            List<object> events = new List<object>();
            events = (List<object>)result.Value;


            //Assert
            Assert.Equal(0, events.Count);
        }


        [Fact]
        public void createEvent()
        {
            //Arrange
            List<CalendarEntry> beforeCreate = _dbContext.CalendarEntry.ToList();
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


            //Act
            CalendarEntry created = _calendarEntriesController.createEvent(entry);


            //Assert
            List<CalendarEntry> afterCreate = _dbContext.CalendarEntry.ToList();
            Assert.NotNull(created);
            Assert.NotEqual(afterCreate.Count, beforeCreate.Count);
        }


        [Fact]
        public void createEventExistingID()
        {
            //Arrange
            List<CalendarEntry> beforeCreate = _dbContext.CalendarEntry.ToList();
            CalendarEntry entry = new CalendarEntry
            {
                id = 1,
                name = "NEW exising id event",
                calendarID = 1,
                title = "NEW exising id event",
                start = 123456789,
                end = 0,
                allDay = true,
                color = "FF00CC"
            };


            //Act
            CalendarEntry created = _calendarEntriesController.createEvent(entry);


            //Assert
            List<CalendarEntry> afterCreate = _dbContext.CalendarEntry.ToList();
            Assert.Null(created);
            Assert.Equal(beforeCreate.Count, afterCreate.Count);
        }


        [Fact]
        public void editEvent()
        {
            //Arrange
            CalendarEntry entry = _dbContext.CalendarEntry.First();


            //Act
            entry.title = "Changed event";
            entry.allDay = true;
            CalendarEntry edited = _calendarEntriesController.editEvent(entry);


            //Assert
            Assert.NotNull(edited);
            Assert.Equal("Changed event", edited.title);
            Assert.Equal(true, edited.allDay);
        }


        [Fact]
        public void editEventNonexistent()
        {
            //Arrange
            List<CalendarEntry> beforeEdit = _dbContext.CalendarEntry.ToList();
            CalendarEntry entry = new CalendarEntry
            {
                id = 888,
                name = "Nonexistent event",
                calendarID = 1,
                title = "Nonexistent event",
                start = 123456789,
                end = 0,
                allDay = false,
                color = "FF00AA"
            };


            //Act
            CalendarEntry edited = _calendarEntriesController.editEvent(entry);
            List<CalendarEntry> afterEdit = _dbContext.CalendarEntry.ToList();


            //Assert
            Assert.NotNull(edited);
            Assert.NotEqual(beforeEdit.Count, afterEdit.Count);
        }


        [Fact]
        public void removeEvent()
        {
            //Arrange
            List<CalendarEntry> beforeEntries = _dbContext.CalendarEntry.ToList();
            CalendarEntry entry = _dbContext.CalendarEntry.First();


            //Act
            _calendarEntriesController.removeEvent(entry);


            //Assert
            List<CalendarEntry> afterEntries = _dbContext.CalendarEntry.ToList();
            Assert.NotEqual(beforeEntries.Count, afterEntries.Count);
        }


        [Fact]
        public void removeEventNonexistent()
        {
            //Arrange
            List<CalendarEntry> beforeEdit = _dbContext.CalendarEntry.ToList();
            CalendarEntry entry = new CalendarEntry
            {
                id = 22,
                name = "Nonexistent event",
                calendarID = 1,
                title = "Nonexistent event",
                start = 123456789,
                end = 0,
                allDay = false,
                color = "FF00AA"
            };


            //Act
            _calendarEntriesController.removeEvent(entry);


            //Assert
            List<CalendarEntry> afterEdit = _dbContext.CalendarEntry.ToList();
            Assert.Equal(beforeEdit.Count, afterEdit.Count);
        }


    }
}
