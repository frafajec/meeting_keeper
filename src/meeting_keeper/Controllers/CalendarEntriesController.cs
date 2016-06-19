using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using meeting_keeper.Models;

namespace meeting_keeper.Controllers
{
    public class CalendarEntriesController : Controller
    {
        private ApplicationDbContext _context;

        public CalendarEntriesController(ApplicationDbContext context)
        {
            _context = context;    
        }



        public JsonResult readEvents(int calendarID)
        {

            List<CalendarEntry> entries = _context.CalendarEntry.ToList();
            List<object> events = new List<object>();

            if (entries.Any(c => c.calendarID == calendarID))
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    if (entries[i].calendarID == calendarID)
                    {
                        events.Add(new {
                            id = entries[i].id,
                            title = entries[i].title,
                            color = entries[i].color,
                            start = entries[i].start,
                            end = entries[i].end,
                            allDay = entries[i].allDay
                        });
                    }
                }
            }

            return Json(events);
        }


        public CalendarEntry createEvent(CalendarEntry entry)
        {

            _context.CalendarEntry.Add(entry);
            _context.SaveChanges();

            return entry;

        }


        public CalendarEntry editEvent(CalendarEntry entry)
        {

            _context.CalendarEntry.Update(entry);
            _context.SaveChanges();

            return entry;
        }


        public void removeEvent(CalendarEntry entry)
        {
            _context.CalendarEntry.Remove(entry);
            _context.SaveChanges();
        }

    }
}
