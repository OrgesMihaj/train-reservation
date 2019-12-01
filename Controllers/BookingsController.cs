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
     * 5. Retrieve the current authenticated user.
     */

    [Authorize]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        private readonly Payment payment = new Payment();

        private readonly Seat seat = new Seat();
        private readonly Coupon coupon = new Coupon();

        public BookingsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            ViewBag.Bookings = 
                _context.Bookings
                    .Where(b => b.UserID == user.Id)
                    .OrderByDescending(b => b.BookingID)
                    .Include(b => b.Journey)
                    .Include(b => b.Seats);

            return View();  
        }

        // POST: Bookings/BookJourney
        [HttpPost]
        [ValidateAntiForgeryToken] /* [1] */
        public async Task<ActionResult> BookJourney(
            [Bind("JourneyID,UserID,Passengers")] Booking booking, string SeatsReceived, string CouponCode, string ReturnJourneyInput) 
        { /* [2] */

            var user = await _userManager.GetUserAsync(HttpContext.User); /* [5] */
            
            // the journey that is being booked
            Journey journey = _context.Journeys.Include(j => j.Train).Single(j => j.JourneyID == booking.JourneyID);
            
            Booking[] bookings = _context.Bookings.ToArray();
            
            // Making sure that the journey is reserved before the departure time 
            if (journey.DepartureTime > DateTime.Now) {

                int TotalPassengers = 0;

                foreach (Booking b in bookings) {
                    TotalPassengers = TotalPassengers + b.Passengers;
                }

                // initital booking cost; 
                booking.Cost = booking.Passengers * journey.Price;

                // Check if the user is using a valid coupon and, if so, 
                // apply it to the price.
                if (!String.IsNullOrEmpty(CouponCode)) {
                    booking.Cost = coupon.ApplyCoupon(_context, booking.Cost, CouponCode);
                }

                int SeatsTaken = TotalPassengers + booking.Passengers;
                int TrainCapacity = journey.Train.Capacity;

                if (ModelState.IsValid && SeatsTaken <= TrainCapacity && journey.IsCanceled == false) {  /* [3] */

                    // check if user wants to book a return ticket
                    if (!String.IsNullOrEmpty(ReturnJourneyInput)) {
                        
                        // making sure that user has entered a number 
                        if (int.TryParse(ReturnJourneyInput, out int JourneyID)) {

                            Booking newBooking = new Booking();
                            Journey newJourney = _context.Journeys.Single(j => j.JourneyID == JourneyID);
                            
                            newBooking.UserID = user.Id;
                            newBooking.JourneyID = JourneyID;
                            newBooking.Cost = booking.Passengers * newJourney.Price;
                            newBooking.Passengers = booking.Passengers;
                            
                            _context.Add(newBooking);

                            TempData["BookReturnTicket"] = "You have successfully booked the trip from " + journey.Destination + " to " + journey.Departure;
                        }
                    }

                    _context.Add(booking);
                    

                    // check if seat(s) reservations are allowed, and if so,
                    // check if a seats reservation request is provided by the user 
                    if (journey.AllowSeatReservation) {
                        
                        // Default state of `SeatsReceived` input in the client-side is 0 
                        if (SeatsReceived == "0") SeatsReceived = null;

                        if (!string.IsNullOrEmpty(SeatsReceived)) {

                            // @param SeatsReceived: string of seats requested seperated by comma: Ex. "5,2,6"
                            seat.reserveSeats(_context, user.Id, booking.BookingID, journey.JourneyID, SeatsReceived);

                            // default price for a reserved seat
                            // hard-coded, should be provided by the system administrator dynamically
                            booking.Cost += 5; 
                        }
                    }


                    // if the journey is paid 
                    if (payment.pay()) {
                        await _context.SaveChangesAsync();
                    }

                } else {

                    if (journey == null)
                    {
                        return NotFound();
                    }

                    TempData["Error"] = "Something went wrong";

                    return View("Views/Journeys/Details", journey);
                }
            }

            TempData["Success"] = "You have successfully booked the trip from " + journey.Departure + " to " + journey.Destination;
            
            return RedirectToAction("Index");
        }

        private bool BookReturnTicket(int Booking) {
            return true;
        }
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelBooking(int BookingID)
        {
            var Booking = await _context.Bookings.FindAsync(BookingID);

            // the journey that is being booked
            Journey Journey = _context.Journeys.Include(j => j.Train).Single(j => j.JourneyID == Booking.JourneyID);

            var UserID = _userManager.GetUserId(HttpContext.User);

            // Make sure that the booking is canceled only before train departure
            if (Booking.UserID == UserID && Journey.DepartureTime > DateTime.Now) {
                
                _context.Bookings.Remove(Booking);
                await _context.SaveChangesAsync();
            } 

            return Redirect("/Bookings");
        }

    }
}
