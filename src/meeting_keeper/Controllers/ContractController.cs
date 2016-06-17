using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using meeting_keeper.Models;

namespace meeting_keeper.Controllers
{
    public class ContractController : Controller
    {
        private ApplicationDbContext _context;

        public ContractController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Contract
        public IActionResult Index()
        {
            return View(_context.Contract.ToList());
        }

        // GET: Contract/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Contract contract = _context.Contract.FirstOrDefault(m => m.id == id);
            if (contract == null)
            {
                return HttpNotFound();
            }

            return View(contract);
        }

        // GET: Contract/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contract/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Contract.Add(contract);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contract);
        }

        // GET: Contract/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Contract contract = _context.Contract.Single(m => m.id == id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contract/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Update(contract);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contract);
        }

        // GET: Contract/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Contract contract = _context.Contract.Single(m => m.id == id);
            if (contract == null)
            {
                return HttpNotFound();
            }

            return View(contract);
        }

        // POST: Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Contract contract = _context.Contract.Single(m => m.id == id);
            _context.Contract.Remove(contract);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
