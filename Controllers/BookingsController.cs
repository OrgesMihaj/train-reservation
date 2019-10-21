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
    [Authorize(Roles = "Admin")]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // POST: Journeys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JourneyID,Departure,Destination,DepartureTime,ArrivalTime,Price,AllowSeatReservation,TrainID")] Journey journey)
        {
            // if (ModelState.IsValid)
            // {
            //     _context.Add(journey);
            //     await _context.SaveChangesAsync();
            //     return RedirectToAction(nameof(Index));
            // }
            // ViewData["TrainID"] = new SelectList(_context.Trains, "TrainID", "TrainID", journey.TrainID);
            // return View(journey);
        }        


        // POST: Journeys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // var journey = await _context.Journeys.FindAsync(id);
            // _context.Journeys.Remove(journey);
            // await _context.SaveChangesAsync();
            // return RedirectToAction(nameof(Index));
        }
    }
}
