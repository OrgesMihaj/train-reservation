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
    public class TrainsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trains
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trains.ToListAsync());
        }

        // GET: Trains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trains = await _context.Trains
                .FirstOrDefaultAsync(m => m.TrainID == id);
            if (trains == null)
            {
                return NotFound();
            }

            return View(trains);
        }

        // GET: Trains/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trains/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainID,Name,Capacity,Express")] Train train)
        {
            if (ModelState.IsValid)
            {
                _context.Add(train);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(train);
        }

        // GET: Trains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trains = await _context.Trains.FindAsync(id);
            if (trains == null)
            {
                return NotFound();
            }
            return View(trains);
        }

        // POST: Trains/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainID,Name,Capacity,Express")] Train train)
        {
            if (id != train.TrainID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(train);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainExists(train.TrainID))
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
            return View(train);
        }

        // GET: Trains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trains = await _context.Trains
                .FirstOrDefaultAsync(m => m.TrainID == id);
            if (trains == null)
            {
                return NotFound();
            }

            return View(trains);
        }

        // POST: Trains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var train = await _context.Trains.FindAsync(id);
            _context.Trains.Remove(train);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainExists(int id)
        {
            return _context.Trains.Any(e => e.TrainID == id);
        }
    }
}
