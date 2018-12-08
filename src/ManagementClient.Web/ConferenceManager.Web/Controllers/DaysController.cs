using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConferenceManager.Database;
using ConferenceManager.Database.Model;

namespace ConferenceManager.Web.Controllers
{
    public class DaysController : Controller
    {
        private readonly CMContext _context;

        public DaysController(CMContext context)
        {
            _context = context;
        }

        // GET: Days/1
        public async Task<IActionResult> Index(int? conferenceId)
        {
            var days = _context.Days.Where(x => x.ConferenceId == conferenceId);
            return View(await days.ToListAsync());
        }

        // GET: Days/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var day = await _context.Days
                .Include(d => d.Conference)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (day == null)
            {
                return NotFound();
            }

            return View(day);
        }
		
        private bool DayExists(int id)
        {
            return _context.Days.Any(e => e.Id == id);
        }
    }
}
