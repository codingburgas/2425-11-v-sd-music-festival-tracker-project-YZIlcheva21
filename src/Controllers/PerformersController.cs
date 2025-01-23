using MusicFestivalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MusicFestivalManagementSystem.Data;


namespace MusicFestivalManagementSystem.Controllers
{
    public class PerformancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerformancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // List all performances
        public IActionResult Index()
        {
            var performances = _context.Performances.ToList();
            return View(performances);
        }

        // View details of a specific performance
        public IActionResult Details(int id)
        {
            var performance = _context.Performances.FirstOrDefault(p => p.Id == id);
            if (performance == null)
            {
                return NotFound();
            }
            return View(performance);
        }

        // Show the form to create a new performance
        public IActionResult Create()
        {
            return View();
        }

        // Handle form submission to create a new performance
        [HttpPost]
        public IActionResult Create(Performance performance)
        {
            if (ModelState.IsValid)
            {
                _context.Performances.Add(performance);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(performance);
        }

        // Show the form to edit an existing performance
        public IActionResult Edit(int id)
        {
            var performance = _context.Performances.FirstOrDefault(p => p.Id == id);
            if (performance == null)
            {
                return NotFound();
            }
            return View(performance);
        }

        // Handle form submission to update an existing performance
        [HttpPost]
        public IActionResult Edit(Performance performance)
        {
            if (ModelState.IsValid)
            {
                _context.Performances.Update(performance);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(performance);
        }

        // Confirm deletion of a performance
        public IActionResult Delete(int id)
        {
            var performance = _context.Performances.FirstOrDefault(p => p.Id == id);
            if (performance == null)
            {
                return NotFound();
            }
            return View(performance);
        }

        // Handle deletion of a performance
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var performance = _context.Performances.FirstOrDefault(p => p.Id == id);
            if (performance != null)
            {
                _context.Performances.Remove(performance);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
