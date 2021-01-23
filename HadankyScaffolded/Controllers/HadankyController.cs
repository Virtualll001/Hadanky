using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HadankyScaffolded.Data;
using HadankyScaffolded.Models;
using Microsoft.AspNetCore.Authorization;

namespace HadankyScaffolded.Controllers
{
    public class HadankyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HadankyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hadanky
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hadanky.ToListAsync());
        }

        // GET: Hadanky/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Hadanky/ShowSearchResult
        public async Task<IActionResult> ShowSearchResult(String SearchPhrase)
        {
            return View("Index", await _context.Hadanky.Where(h => h.Otazka.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Hadanky/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hadanky = await _context.Hadanky
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hadanky == null)
            {
                return NotFound();
            }

            return View(hadanky);
        }

        // GET: Hadanky/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hadanky/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Otazka,Odpoved")] Hadanky hadanky)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hadanky);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hadanky);
        }

        // GET: Hadanky/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hadanky = await _context.Hadanky.FindAsync(id);
            if (hadanky == null)
            {
                return NotFound();
            }
            return View(hadanky);
        }

        // POST: Hadanky/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Otazka,Odpoved")] Hadanky hadanky)
        {
            if (id != hadanky.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hadanky);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HadankyExists(hadanky.Id))
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
            return View(hadanky);
        }

        // GET: Hadanky/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hadanky = await _context.Hadanky
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hadanky == null)
            {
                return NotFound();
            }

            return View(hadanky);
        }

        // POST: Hadanky/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hadanky = await _context.Hadanky.FindAsync(id);
            _context.Hadanky.Remove(hadanky);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HadankyExists(int id)
        {
            return _context.Hadanky.Any(e => e.Id == id);
        }
    }
}
