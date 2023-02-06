using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_MythicTask.Data;
using Proiect_MythicTask.Models;

namespace Proiect_MythicTask.Controllers
{
    public class TODOListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TODOListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TODOLists
        public async Task<IActionResult> Index()
        {
              return View(await _context.TODOList.ToListAsync());
        }

        // GET: TODOLists // CautareCriteriu
        public async Task<IActionResult> CautareCriteriu()
        {
            return View("CautareCriteriu");
        }

        // POST: TODOLists // RezultateCautareCriteriu
        public async Task<IActionResult> RezultateCautareCriteriu(string CautareDupaLocatie)
        {
            return View("Index", await _context.TODOList.Where(j => j.Locație.Contains(CautareDupaLocatie)).ToListAsync());
        }


        // GET: TODOLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TODOList == null)
            {
                return NotFound();
            }

            var tODOList = await _context.TODOList
                .FirstOrDefaultAsync(m => m.NrCrit == id);
            if (tODOList == null)
            {
                return NotFound();
            }

            return View(tODOList);
        }

        // GET: TODOLists/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TODOLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NrCrit,Obiectiv,Descriere,Locație,Deadline")] TODOList tODOList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tODOList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tODOList);
        }

        // GET: TODOLists/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TODOList == null)
            {
                return NotFound();
            }

            var tODOList = await _context.TODOList.FindAsync(id);
            if (tODOList == null)
            {
                return NotFound();
            }
            return View(tODOList);
        }

        // POST: TODOLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NrCrit,Obiectiv,Descriere,Locație,Deadline")] TODOList tODOList)
        {
            if (id != tODOList.NrCrit)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tODOList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TODOListExists(tODOList.NrCrit))
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
            return View(tODOList);
        }

        // GET: TODOLists/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TODOList == null)
            {
                return NotFound();
            }

            var tODOList = await _context.TODOList
                .FirstOrDefaultAsync(m => m.NrCrit == id);
            if (tODOList == null)
            {
                return NotFound();
            }

            return View(tODOList);
        }

        // POST: TODOLists/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TODOList == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TODOList'  is null.");
            }
            var tODOList = await _context.TODOList.FindAsync(id);
            if (tODOList != null)
            {
                _context.TODOList.Remove(tODOList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TODOListExists(int id)
        {
          return _context.TODOList.Any(e => e.NrCrit == id);
        }
    }
}
