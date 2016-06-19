using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using meeting_keeper.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace meeting_keeper.Controllers
{
    public class CalendarController : Controller
    {
        private ApplicationDbContext _context;

        public CalendarController(ApplicationDbContext context)
        {
            _context = context;    
        }
        

        public IActionResult Index()
        {
            //return View(_context.Calendar.ToList());

            if (!User.IsSignedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        [HttpGet]
        public IActionResult initCalendar()
        {

            var userID = User.GetUserId();
            System.Diagnostics.Debug.WriteLine("USER ID: " + userID);

            Calendar calendar;
            List<Calendar> calendars = _context.Calendar.ToList();

            if (!calendars.Any(c => c.userID == userID))
            {
                //create initial calendar with default settings
                calendar = createDefaultCalendar();
            }
            else
            {
                //take first calendar from list that matches (should only be one)
                calendar = calendars.First(c => c.userID == userID);
            }
            
            JsonResult settings = readSettings(calendar);

            var cEntries = new CalendarEntriesController(_context);
            JsonResult events = cEntries.readEvents(calendar.id);

            return Json( new { events = events.Value, settings = settings.Value } );
        }


        [HttpGet]
        public IActionResult createEvent(string eventJson)
        {
            var cEntries = new CalendarEntriesController(_context);
            var userID = User.GetUserId();

            Calendar calendar = _context.Calendar.Single(c => c.userID == userID);
            CalendarEntry entry = JsonConvert.DeserializeObject<CalendarEntry>(eventJson);
            entry.calendarID = calendar.id;

            entry = cEntries.createEvent(entry);

            return Json(entry);
        }


        [HttpGet]
        public IActionResult editEvent(string eventJson) {

            var cEntries = new CalendarEntriesController(_context);
            var userID = User.GetUserId();

            Calendar calendar = _context.Calendar.Single(c => c.userID == userID);
            CalendarEntry entry = JsonConvert.DeserializeObject<CalendarEntry>(eventJson);
            entry.calendarID = calendar.id;

            cEntries.editEvent(entry);

            return Json(entry);
        }


        [HttpGet]
        public IActionResult removeEvent(string eventJson)
        {
            var cEntries = new CalendarEntriesController(_context);
            var userID = User.GetUserId();

            Calendar calendar = _context.Calendar.Single(c => c.userID == userID);
            CalendarEntry entry = JsonConvert.DeserializeObject<CalendarEntry>(eventJson);
            entry.calendarID = calendar.id;

            var entryID = entry.id;

            cEntries.removeEvent(entry);

            return Json(entryID);
        }


        public JsonResult readSettings(Calendar calendar)
        {
            List<int> hiddenDays = new List<int>();
            if (!calendar.showSunday) hiddenDays.Add(0);
            if (!calendar.showSaturday) hiddenDays.Add(6);

            int firstDay;
            firstDay = 1; //for now not in options

            return Json( new { hiddenDays = hiddenDays, firstDay = firstDay } );
        }


        [HttpGet]
        public IActionResult getSettings()
        {
            var userID = User.GetUserId();
            Calendar calendar = _context.Calendar.Single(c => c.userID == userID);
            JsonResult settings = readSettings(calendar);
            return Json(settings.Value);
        }


        [HttpGet]
        public IActionResult saveSettings(string settingsJSON)
        {

            Dictionary<string, string> settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(settingsJSON);

            var userID = User.GetUserId();
            Calendar calendar = _context.Calendar.Single(c => c.userID == userID);

            calendar.showSaturday = Boolean.Parse(settings["showSaturday"]);
            calendar.showSunday = Boolean.Parse(settings["showSunday"]);

            _context.Update(calendar);
            _context.SaveChanges();

            return Json(settingsJSON);
        }


        public Calendar createDefaultCalendar() {
            
            Calendar calendar = new Calendar {
                dateCreated = unixTimeNow(),
                dateModified = unixTimeNow(),
                name = User.Identity.Name + " Calendar",
                userID = User.GetUserId(),
                showSaturday = true,
                showSunday = true
            };

            _context.Calendar.Add(calendar);
            _context.SaveChanges();

            return calendar;
        }


        public long unixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

    }
}
