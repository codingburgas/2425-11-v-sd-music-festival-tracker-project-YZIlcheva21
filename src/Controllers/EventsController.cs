using Microsoft.AspNetCore.Mvc;
using MusicFestivalManagementSystem.Models;
using System.Linq;
using MusicFestivalManagementSystem.Data;

namespace MusicFestivalManagementSystem.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // List all events with search, sorting, and filtering
        public IActionResult Index(string searchQuery, string sortOrder, string category)
        {
            var events = _context.Events.AsQueryable();

            // Search
            if (!string.IsNullOrEmpty(searchQuery))
            {
                events = events.Where(e => e.Name.Contains(searchQuery) || e.Description.Contains(searchQuery));
            }

            // Filtering by category (if the Event model has a Category property)
            if (!string.IsNullOrEmpty(category))
            {
                events = events.Where(e => e.Category == category);
            }

            // Sorting
            events = sortOrder switch
            {
                "name_desc" => events.OrderByDescending(e => e.Name),
                "date_asc" => events.OrderBy(e => e.Date),
                "date_desc" => events.OrderByDescending(e => e.Date),
                _ => events.OrderBy(e => e.Name), // Default sorting by name ascending
            };

            ViewBag.SearchQuery = searchQuery;
            ViewBag.SortOrder = sortOrder;
            ViewBag.Category = category;

            return View(events.ToList());
        }

        // View details of a specific event
        public IActionResult Details(int id)
        {
            var eventItem = _context.Events.FirstOrDefault(e => e.Id == id);
            if (eventItem == null)
            {
                return NotFound();
            }
            return View(eventItem);
        }

        // Show the form to create a new event
        public IActionResult Create()
        {
            return View();
        }

        // Handle form submission to create a new event
        [HttpPost]
        public IActionResult Create(Event eventItem)
        {
            if (ModelState.IsValid)
            {
                _context.Events.Add(eventItem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventItem);
        }

        // Show the form to edit an existing event
        public IActionResult Edit(int id)
        {
            var eventItem = _context.Events.FirstOrDefault(e => e.Id == id);
            if (eventItem == null)
            {
                return NotFound();
            }
            return View(eventItem);
        }

        // Handle form submission to update an existing event
        [HttpPost]
        public IActionResult Edit(Event eventItem)
        {
            if (ModelState.IsValid)
            {
                _context.Events.Update(eventItem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventItem);
        }

        // Confirm deletion of an event
        public IActionResult Delete(int id)
        {
            var eventItem = _context.Events.FirstOrDefault(e => e.Id == id);
            if (eventItem == null)
            {
                return NotFound();
            }
            return View(eventItem);
        }

        // Handle deletion of an event
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var eventItem = _context.Events.FirstOrDefault(e => e.Id == id);
            if (eventItem != null)
            {
                _context.Events.Remove(eventItem);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
