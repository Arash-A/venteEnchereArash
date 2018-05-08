using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using venteTest.Data;
using venteTest.Models;

namespace venteTest.Controllers
{
    public class ObjetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ObjetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Objets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Objets.Include(o => o.Categorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Objets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objet = await _context.Objets
                .Include(o => o.Categorie)
                .SingleOrDefaultAsync(m => m.ObjetID == id);
            if (objet == null)
            {
                return NotFound();
            }

            return View(objet);
        }

        // GET: Objets/Create
        public IActionResult Create()
        {
            ViewData["CategorieNom"] = new SelectList(_context.Categories, "Nom", "Nom");
            return View();
        }

        // POST: Objets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObjetID,Nom,Description,PrixDepart,DateInscription,DureeMiseVente,imageUrl,CategorieID")] Objet objet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieID"] = new SelectList(_context.Categories, "CategorieId", "CategorieId", objet.CategorieID);
            return View(objet);
        }

        // GET: Objets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objet = await _context.Objets.SingleOrDefaultAsync(m => m.ObjetID == id);
            if (objet == null)
            {
                return NotFound();
            }
            ViewData["CategorieID"] = new SelectList(_context.Categories, "CategorieId", "CategorieId", objet.CategorieID);
            return View(objet);
        }

        // POST: Objets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObjetID,Nom,Description,PrixDepart,DateInscription,DureeMiseVente,imageUrl,CategorieID")] Objet objet)
        {
            if (id != objet.ObjetID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(objet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObjetExists(objet.ObjetID))
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
            ViewData["CategorieID"] = new SelectList(_context.Categories, "CategorieId", "CategorieId", objet.CategorieID);
            return View(objet);
        }

        // GET: Objets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objet = await _context.Objets
                .Include(o => o.Categorie)
                .SingleOrDefaultAsync(m => m.ObjetID == id);
            if (objet == null)
            {
                return NotFound();
            }

            return View(objet);
        }

        // POST: Objets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var objet = await _context.Objets.SingleOrDefaultAsync(m => m.ObjetID == id);
            _context.Objets.Remove(objet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObjetExists(int id)
        {
            return _context.Objets.Any(e => e.ObjetID == id);
        }
    }
}
