using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BadgerysCreekHotel.Data;
using BadgerysCreekHotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.Sqlite;

namespace BadgerysCreekHotel.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Room.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .FirstOrDefaultAsync(m => m.ID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SearchRooms searchingRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(searchingRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(searchingRoom);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Level,BedCount,Price")] Room room)
        {
            if (id != room.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.ID))
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
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .FirstOrDefaultAsync(m => m.ID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Room.FindAsync(id);
            _context.Room.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.ID == id);
        }

        // GET: MovieGoers/PeopleDiff
        [AllowAnonymous]
        public IActionResult Search()
        {
            // Get the options for the MovieGoer select list from the database
            // and save them in ViewBag for passing to View
            ViewBag.Search = new SelectList(_context.Room, "Email", "Name");
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        [Authorize(Roles = "Customers")]
        public async Task<IActionResult> Search(SearchRooms basicSearch)
        {
            var bed = new SqliteParameter("bed", basicSearch.roomBedCount);
            var cin = new SqliteParameter("in", basicSearch.BookingCheckIn);
            var cout = new SqliteParameter("out", basicSearch.BookingCheckOut);
            var searchRooms = _context.Room.FromSql("select * from [Room] "
                               + "where [Room].BedCount = @bed and [Room].ID not in "
                               + "(select [Room].ID from [Room] inner join [Booking] on [Room].ID = [Booking].RoomID where "
                               + "[Booking].CheckIn < @out AND @in < [Booking].CheckOut)", bed, cin, cout)
                           .Select(r => new Room { ID = r.ID, Level = r.Level, BedCount = r.BedCount, Price = r.Price });
            ViewBag.result = await searchRooms.ToListAsync();
            return View(basicSearch);

        }
    }
}
