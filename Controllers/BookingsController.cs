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
using Microsoft.AspNetCore.Identity;

namespace TrainReservation.Controllers
{
    public class BookingsController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public BookingsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        public async Task<IActionResult> CancelBooking(int BookingID)
        {
            var Booking = await _context.Bookings.FindAsync(BookingID);
            var UserID = _userManager.GetUserId(HttpContext.User);

            if (Booking.UserID == UserID) {
                 _context.Bookings.Remove(Booking);
                await _context.SaveChangesAsync();
            }

            return Redirect("/Bookings");
        }

    }
}
