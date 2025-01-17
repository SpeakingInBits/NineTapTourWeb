using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NineTapTourWeb.Data;
using NineTapTourWeb.Models;

namespace NineTapTourWeb.Controllers;

[Authorize(Roles = IdentityHelper.AdminRole)]
public class NineTapRegionsController : Controller
{
    private readonly ApplicationDbContext _context;

    public NineTapRegionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: NineTapRegions
    public async Task<IActionResult> Index()
    {
        return View(await _context.Regions.ToListAsync());
    }

    // GET: NineTapRegions/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var nineTapRegion = await _context.Regions
            .FirstOrDefaultAsync(m => m.Id == id);
        if (nineTapRegion == null)
        {
            return NotFound();
        }

        return View(nineTapRegion);
    }

    // GET: NineTapRegions/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: NineTapRegions/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] NineTapRegion nineTapRegion)
    {
        if (ModelState.IsValid)
        {
            _context.Add(nineTapRegion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(nineTapRegion);
    }

    // GET: NineTapRegions/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var nineTapRegion = await _context.Regions.FindAsync(id);
        if (nineTapRegion == null)
        {
            return NotFound();
        }
        return View(nineTapRegion);
    }

    // POST: NineTapRegions/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] NineTapRegion nineTapRegion)
    {
        if (id != nineTapRegion.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(nineTapRegion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NineTapRegionExists(nineTapRegion.Id))
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
        return View(nineTapRegion);
    }

    // GET: NineTapRegions/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var nineTapRegion = await _context.Regions
            .FirstOrDefaultAsync(m => m.Id == id);
        if (nineTapRegion == null)
        {
            return NotFound();
        }

        return View(nineTapRegion);
    }

    // POST: NineTapRegions/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var nineTapRegion = await _context.Regions.FindAsync(id);
        if (nineTapRegion != null)
        {
            _context.Regions.Remove(nineTapRegion);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool NineTapRegionExists(int id)
    {
        return _context.Regions.Any(e => e.Id == id);
    }
}