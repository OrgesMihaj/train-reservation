using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainReservation.Data;
using TrainReservation.Models;
using Microsoft.AspNetCore.Authorization;

namespace TrainReservation.Controllers
{
    public class BookingsController : Controller
    {
        
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Bookings = _context.Bookings.Include(b => b.Journey);
            return View();
        }

        public ActionResult BookJourney(int JourneyID, string UserID) {
            Booking booking = new Booking();

            booking.UserID = UserID;
            booking.JourneyID = JourneyID;

            _context.Bookings.Add(booking);
            _context.SaveChanges();
            
            return Redirect("/Bookings");
        }

    }
}
