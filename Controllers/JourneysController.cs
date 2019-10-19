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
    public class JourneysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JourneysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Journeys
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Journeys.Include(j => j.Train);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Journeys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journey = await _context.Journeys
                .Include(j => j.Train)
                .FirstOrDefaultAsync(m => m.JourneyID == id);
            if (journey == null)
            {
                return NotFound();
            }

            return View(journey);
        }

        // GET: Journeys/Create
        public IActionResult Create()
        {
            ViewData["Trains"] = new SelectList(_context.Trains, "TrainID", "Name");
            return View();
        }

        // POST: Journeys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JourneyID,DepartsFrom,Destination,DepartsAt,ArrivesAt,TrainID")] Journey journey)
        {
            if (ModelState.IsValid)
            {
                _context.Add(journey);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainID"] = new SelectList(_context.Trains, "TrainID", "TrainID", journey.TrainID);
            return View(journey);
        }

        // GET: Journeys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journey = await _context.Journeys.FindAsync(id);
            if (journey == null)
            {
                return NotFound();
            }
            ViewData["TrainID"] = new SelectList(_context.Trains, "TrainID", "TrainID", journey.TrainID);
            return View(journey);
        }

        // POST: Journeys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JourneyID,DepartsFrom,Destination,DepartsAt,ArrivesAt,TrainID")] Journey journey)
        {
            if (id != journey.JourneyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(journey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JourneyExists(journey.JourneyID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainID"] = new SelectList(_context.Trains, "TrainID", "TrainID", journey.TrainID);
            return View(journey);
        }

        // GET: Journeys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journey = await _context.Journeys
                .Include(j => j.Train)
                .FirstOrDefaultAsync(m => m.JourneyID == id);
            if (journey == null)
            {
                return NotFound();
            }

            return View(journey);
        }

        // POST: Journeys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var journey = await _context.Journeys.FindAsync(id);
            _context.Journeys.Remove(journey);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JourneyExists(int id)
        {
            return _context.Journeys.Any(e => e.JourneyID == id);
        }
    }
}