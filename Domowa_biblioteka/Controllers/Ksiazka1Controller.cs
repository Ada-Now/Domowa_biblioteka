using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domowa_biblioteka.Data;
using Domowa_biblioteka.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Domowa_biblioteka.Controllers
{
    
    public class Ksiazka1Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public Ksiazka1Controller(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Ksiazka1
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            var applicationDbContext = _context.Ksiazka1.Include(t => t.User).Where(u => u.User == user);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ksiazka1/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ksiazka1 == null)
            {
                return NotFound();
            }

            var ksiazka1 = await _context.Ksiazka1
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ksiazka1 == null)
            {
                return NotFound();
            }

            return View(ksiazka1);
        }
        // GET: Ksiazka/ShowSearchForms
        public async Task<IActionResult> ShowSearchForm()
        {
            return _context.Ksiazka1 != null ?
                        View() :
                        Problem("Entity set 'ApplicationDbContext.Ksiazka'  is null.");
        }
        // POST: Ksiazka/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return _context.Ksiazka1 != null ?
                        View("Index", await _context.Ksiazka1.Where(j => j.Tytul.Contains(SearchPhrase)).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Ksiazka'  is null.");
        }
        // GET: Ksiazka1/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Ksiazka1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tytul,Autor,Gatunek,Opis,Data_wydania,UserId")] Ksiazka1DTO ksiazka1DTO)

        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var ksiazka1 = new Ksiazka1
            {
            Id = ksiazka1DTO.Id,
            Tytul = ksiazka1DTO.Tytul,
            Autor = ksiazka1DTO.Autor,
            Gatunek = ksiazka1DTO.Gatunek,
            Opis = ksiazka1DTO.Opis,
            Data_wydania = ksiazka1DTO.Data_wydania,
            UserId = user.Id,
             };
            if (ModelState.IsValid)
            {
                _context.Add(ksiazka1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ksiazka1.UserId);
            return View(ksiazka1);
        }


        // GET: Ksiazka1/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ksiazka1 == null)
            {
                return NotFound();
            }

            var ksiazka1 = await _context.Ksiazka1.FindAsync(id);
            if (ksiazka1 == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ksiazka1.UserId);
            return View(ksiazka1);
        }

        // POST: Ksiazka1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tytul,Autor,Gatunek,Opis,Data_wydania,UserId")] Ksiazka1DTO ksiazka1DTO)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var ksiazka1 = new Ksiazka1
            {
                Id = ksiazka1DTO.Id,
                Tytul = ksiazka1DTO.Tytul,
                Autor = ksiazka1DTO.Autor,
                Gatunek = ksiazka1DTO.Gatunek,
                Opis = ksiazka1DTO.Opis,
                Data_wydania = ksiazka1DTO.Data_wydania,
                UserId = user.Id,
            };
            if (id != ksiazka1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ksiazka1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Ksiazka1Exists(ksiazka1.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ksiazka1.UserId);
            return View(ksiazka1);
        }

        // GET: Ksiazka1/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ksiazka1 == null)
            {
                return NotFound();
            }

            var ksiazka1 = await _context.Ksiazka1
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ksiazka1 == null)
            {
                return NotFound();
            }

            return View(ksiazka1);
        }

        // POST: Ksiazka1/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ksiazka1 == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Ksiazka1'  is null.");
            }
            var ksiazka1 = await _context.Ksiazka1.FindAsync(id);
            if (ksiazka1 != null)
            {
                _context.Ksiazka1.Remove(ksiazka1);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Ksiazka1Exists(int id)
        {
          return (_context.Ksiazka1?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
