using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using meeting_keeper.Models;

namespace meeting_keeper.Controllers
{
    public class ClientController : Controller
    {
        private ApplicationDbContext _context;

        public ClientController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Client
        public IActionResult Index()
        {
            return View(_context.Client.ToList());
        }

        // GET: Client/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Client client = _context.Client.FirstOrDefault(m => m.id == id);
            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Client.Add(client);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Client/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Client client = _context.Client.Single(m => m.id == id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Client/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Update(client);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Client/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Client client = _context.Client.Single(m => m.id == id);
            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Client client = _context.Client.Single(m => m.id == id);
            _context.Client.Remove(client);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
