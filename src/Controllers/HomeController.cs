﻿using Microsoft.AspNetCore.Mvc;
using MusicFestivalManagementSystem.Data;
using MusicFestivalManagementSystem.Models;
using System.Linq;

namespace MusicFestivalManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Default Index Action
        public IActionResult Index()
        {
            return View();
        }

        // Statistics Action
        public IActionResult Statistics()
        {
            var viewModel = new StatisticsViewModel
            {
                TotalPerformers = _context.Performers.Count(),
                TotalPerformances = _context.Performances.Count(),
                TotalEvents = _context.Events.Count(),
                MostPopularGenre = _context.Performers
                    .GroupBy(p => p.Genre)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefault() ?? "No Data"
            };

            return View(viewModel);
        }
    }
}
