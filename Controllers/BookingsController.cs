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
    /**
     * BookingsController is responsible for booking journeys, retriving all journeys,
     * and cancel bookings. 
     * 
     * 1. Secured from Cross Site Request Forgery (CSRF).
     * 2. To protect from overposting attacks.
     * 3. ModelState.IsValid tells you if any model errors have been added to ModelState .
     *    The default model binder will add some errors for basic type conversion issues 
     *    (for example, passing a non-number for something which is an "int").
     * 4. Asynchronously saves all changes made in this context to the underlying database.
     * 
     */

    [Authorize]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BookingsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.Bookings = _context.Bookings.Where(b => b.UserID == user.Id).Include(b => b.Journey);

            return View();  
        }

        // POST: Bookings/BookJourney
        [HttpPost]
        [ValidateAntiForgeryToken] /* [1] */
        public async Task<ActionResult> BookJourney([Bind("JourneyID,UserID,Passengers")] Booking booking) { /* [2] */
            
            if (ModelState.IsValid) /* [3] */
            {
                _context.Add(booking);
                await _context.SaveChangesAsync(); /* [4] */
            }
            
            return Redirect("/Bookings");
        }

        
        [ValidateAntiForgeryToken]
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
