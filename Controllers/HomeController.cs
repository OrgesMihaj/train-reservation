using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainReservation.Data;
using TrainReservation.Models;

namespace TrainReservation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string RequestedDeparture = null, string RequestedDestination = null)
        {
            var Journeys = from m in _context.Journeys select m;

            if (!String.IsNullOrEmpty(RequestedDeparture))
            {
                Journeys = Journeys.Where(s => s.Departure.ToLower().Contains(RequestedDeparture.ToLower()));
            }

            if (!String.IsNullOrEmpty(RequestedDestination))
            {
                Journeys = Journeys.Where(s => s.Destination.ToLower().Contains(RequestedDestination.ToLower()));
            }

            ViewBag.Journeys = Journeys.Include(j => j.Train);
            return View();
        }
      

        public IActionResult Privacy()
        {
            return View();
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }
    }
}
