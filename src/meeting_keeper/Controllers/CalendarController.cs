using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using meeting_keeper.Models;

namespace meeting_keeper.Controllers
{
    public class CalendarController : Controller
    {
        private ApplicationDbContext _context;

        public CalendarController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Calendar
        public IActionResult Index()
        {
            return View(_context.Calendar.ToList());
        }

        // GET: Calendar/Details/5
        //public IActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    Calendar calendar = _context.Calendar.Single(m => m.id == id);
        //    if (calendar == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(calendar);
        //}

        // GET: Calendar/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Calendar/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Calendar calendar)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Calendar.Add(calendar);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(calendar);
        //}

        // GET: Calendar/Edit/5
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    Calendar calendar = _context.Calendar.Single(m => m.id == id);
        //    if (calendar == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(calendar);
        //}

        // POST: Calendar/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Calendar calendar)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Update(calendar);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(calendar);
        //}

        //// GET: Calendar/Delete/5
        //[ActionName("Delete")]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    Calendar calendar = _context.Calendar.Single(m => m.id == id);
        //    if (calendar == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(calendar);
        //}

        //// POST: Calendar/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    Calendar calendar = _context.Calendar.Single(m => m.id == id);
        //    _context.Calendar.Remove(calendar);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
