using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiscGolfRepository.Data;
using DiscGolfRepository.Models;

namespace DiscGolfRepository.Controllers
{
    public class FrisbeeDiscsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FrisbeeDiscsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FrisbeeDiscs
        public async Task<IActionResult> Index()
        {
            return View(await _context.FrisbeeDisc.ToListAsync());
        }

        // GET: FrisbeeDiscs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frisbeeDisc = await _context.FrisbeeDisc
                .FirstOrDefaultAsync(m => m.Id == id);
            if (frisbeeDisc == null)
            {
                return NotFound();
            }

            return View(frisbeeDisc);
        }

        // GET: FrisbeeDiscs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FrisbeeDiscs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,Color,Weight")] FrisbeeDisc frisbeeDisc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(frisbeeDisc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(frisbeeDisc);
        }

        // GET: FrisbeeDiscs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frisbeeDisc = await _context.FrisbeeDisc.FindAsync(id);
            if (frisbeeDisc == null)
            {
                return NotFound();
            }
            return View(frisbeeDisc);
        }

        // POST: FrisbeeDiscs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Color,Weight")] FrisbeeDisc frisbeeDisc)
        {
            if (id != frisbeeDisc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(frisbeeDisc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FrisbeeDiscExists(frisbeeDisc.Id))
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
            return View(frisbeeDisc);
        }

        // GET: FrisbeeDiscs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frisbeeDisc = await _context.FrisbeeDisc
                .FirstOrDefaultAsync(m => m.Id == id);
            if (frisbeeDisc == null)
            {
                return NotFound();
            }

            return View(frisbeeDisc);
        }

        // POST: FrisbeeDiscs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var frisbeeDisc = await _context.FrisbeeDisc.FindAsync(id);
            _context.FrisbeeDisc.Remove(frisbeeDisc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FrisbeeDiscExists(int id)
        {
            return _context.FrisbeeDisc.Any(e => e.Id == id);
        }
    }
}
