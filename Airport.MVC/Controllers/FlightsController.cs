using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Airport.MVC.Data;
using Airport.MVC.Models;
using Airport.MVC.Services;

namespace Airport.MVC.Controllers
{
    public class FlightsController : Controller
    {
        private readonly FlightDbContext _context;
        private readonly IFlightRepository _iFlightRepository;

        public FlightsController(FlightDbContext context, IFlightRepository iFlightRepository)
        {
            _context = context;
            _iFlightRepository = iFlightRepository;
        }

        //https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/search?view=aspnetcore-2.2
        // GET: Flights
        public IActionResult Index(string fromlocation,string toLocation)
        {
            FlightViewModel flightViewModel = new FlightViewModel
            {
                FromLocation = fromlocation,
                ToLocation = toLocation
            };
            var flights = _iFlightRepository.SearchFlight(flightViewModel).Result;
            return View(flights);
        }


        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightViewModel = await _context.Flight
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flightViewModel == null)
            {
                return NotFound();
            }

            return View(flightViewModel);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("FlightId,AirCraftType,FromLocation,ToLocation,DepartureTime,ArrivalTime")] FlightViewModel flightViewModel)
        {
            if (ModelState.IsValid)
            {
                _iFlightRepository.AddFlight(flightViewModel);
                 _iFlightRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flightViewModel);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightViewModel = await _context.Flight.FindAsync(id);
            if (flightViewModel == null)
            {
                return NotFound();
            }
            return View(flightViewModel);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FlightId,AirCraftType,FromLocation,ToLocation,DepartureTime,ArrivalTime")] FlightViewModel flightViewModel)
        {
            if (id != flightViewModel.FlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _iFlightRepository.UpdateFlight(id);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightViewModelExists(flightViewModel.FlightId))
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
            return View(flightViewModel);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightViewModel = await _context.Flight
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flightViewModel == null)
            {
                return NotFound();
            }

            return View(flightViewModel);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flightViewModel = await _context.Flight.FindAsync(id);
            _context.Flight.Remove(flightViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightViewModelExists(int id)
        {
            return _context.Flight.Any(e => e.FlightId == id);
        }
    }
}
