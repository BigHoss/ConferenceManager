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
			var conference = new Conference
			{
				DateRange = new DateTime[]
				{
					DateTime.Now,
					DateTime.Now.AddDays(1)
				}
			};
			return View(conference);
		}

		// POST: Conferences/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,DateRange")] Conference conference)
		{
			if (ModelState.IsValid)
			{
				var totalDays = (conference.DateRange[1] - conference.DateRange[0]).TotalDays;
				for (int i = 0; i < totalDays; i++)
				{
					_context.Days.Add(new Day
					{
						Conference = conference,
						Date = conference.DateRange[0].AddDays(i)
					});
				}
				_context.Add(conference);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Details), new { id = conference.Id });
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

			ViewBag.StartTime = conference.StartTime;
			ViewBag.EndTime = conference.EndTime;

			return View(conference);
		}

		// POST: Conferences/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,DateRange,StartTime,EndTime")] Conference conference)
		{
			if (id != conference.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					var orderedDays = await _context.Days.Where(x => x.ConferenceId == conference.Id).OrderBy(x => x.Date).ToListAsync();
					int currentDaysAmount = orderedDays.Count;
					int neededDaysAmount = Convert.ToInt32((conference.EndTime - conference.StartTime).TotalDays);

						for (int i = 0; i < neededDaysAmount; i++)
						{
							Day day;
							if (i + 1 > currentDaysAmount)
							{
								if (conference.StartTime != null)
								{
									day = new Day
									{
										Date = conference.StartTime.AddDays(i)
									};
									conference.Days?.Add(day);
								}

							}
							else
							{
								day = orderedDays[i];
								if (conference.StartTime != null)
								{
									day.Date = conference.StartTime.AddDays(i);
								}

								_context.Days.Update(day);
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
				return RedirectToAction(nameof(Details), new { id = conference.Id });
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
