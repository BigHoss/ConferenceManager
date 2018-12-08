using ConferenceManager.Database;
using ConferenceManager.Database.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceManager.Web.Controllers
{
	public class ConferencesController : Controller
	{
		private readonly CMContext _context;

		public ConferencesController(CMContext context)
		{
			_context = context;
		}

		// GET: Conferences
		public async Task<IActionResult> Index()
		{
			return View(await _context.Conferences.ToListAsync());
		}

		// GET: Conferences/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var conference = await _context.Conferences
				.FirstOrDefaultAsync(m => m.Id == id);
			if (conference == null)
			{
				return NotFound();
			}

			return View(conference);
		}

		// GET: Conferences/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Conferences/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,CreateDateTime,CreateUser,UpdateDateTime,UpdateUserName,Updates,DeleteDateTime,DeleteUserName,Name,StartTime,EndTime")] Conference conference)
		{
			if (ModelState.IsValid)
			{
				for (int i = 0; i < (conference.EndTime - conference.StartTime).TotalDays; i++)
				{
					_context.Days.Add(new Day
					{
						Conference = conference,
						Date = conference.StartTime.AddDays(i)
					});
				}
				_context.Add(conference);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(conference);
		}

		// GET: Conferences/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var conference = await _context.Conferences.FindAsync(id);
			if (conference == null)
			{
				return NotFound();
			}
			return View(conference);
		}

		// POST: Conferences/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,CreateDateTime,CreateUser,UpdateDateTime,UpdateUserName,Updates,DeleteDateTime,DeleteUserName,Name,StartTime,EndTime")] Conference conference)
		{
			if (id != conference.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					var orderedDays = conference.Days?.OrderBy(x => x.Date).ToList();
					int currentDaysAmount = orderedDays.Count;
					int neededDaysAmount = Convert.ToInt32((conference.EndTime - conference.StartTime).TotalDays);

					for (int i = 0; i < neededDaysAmount; i++)
					{
						Day day;
						if (i + 1 > currentDaysAmount)
						{
							day = new Day
							{
								Date = conference.StartTime.AddDays(i)
							};
							conference.Days?.Add(day);
						}
						else
						{
							day = orderedDays[i];
							day.Date = conference.StartTime.AddDays(i);
						}
					}

					_context.Update(conference);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ConferenceExists(conference.Id))
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
			return View(conference);
		}

		// GET: Conferences/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var conference = await _context.Conferences
				.FirstOrDefaultAsync(m => m.Id == id);
			if (conference == null)
			{
				return NotFound();
			}

			return View(conference);
		}

		// POST: Conferences/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var conference = await _context.Conferences.FindAsync(id);
			_context.Conferences.Remove(conference);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ConferenceExists(int id)
		{
			return _context.Conferences.Any(e => e.Id == id);
		}
	}
}
