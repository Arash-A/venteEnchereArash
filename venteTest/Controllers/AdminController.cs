using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using venteTest.Data;
using Microsoft.AspNetCore.Identity;
using venteTest.Models;
using venteTest.Models.AdminViewModels;
using AutoMapper;

namespace venteTest.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager) {
            _context = context;
        }
        // CATEGORIES
        public async Task<IActionResult> Index()
        {
            IList<Categorie> lstCateg = await _context.Categories.ToListAsync();
            IList<CategorieViewModel> lstCategViewModel = Mapper.Map<IList<Categorie>, IList<CategorieViewModel>>(lstCateg);
            return View(lstCategViewModel);
        }

        public IActionResult CreateCategorie()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategorie([Bind("CategorieId,Nom,Description")] CategorieViewModel categViewModel) {

            // Valider l'unicité du nom de la catégorie..
            if (_context.Categories.FirstOrDefault(p => p.Nom.Equals(categViewModel.Nom)) != null)
                ModelState.AddModelError("CustomValid", "This categorie name already exists. Try again.");

            // Fin validation

            if (ModelState.IsValid) {

                Categorie categ = Mapper.Map<CategorieViewModel, Categorie>(categViewModel);
                // Mettre en majuscule 1e caractère de la description
                categ.Description = categ.Description.First().ToString().ToUpper() + categ.Description.Substring(1);
                _context.Add(categ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categViewModel);
        }

        // GET: Admin/EditCategorie/5
        public async Task<IActionResult> EditCategorie(int? id) {
            if (id == null) {
                return NotFound();
            }

            var categorie = await _context.Categories.SingleOrDefaultAsync(m => m.CategorieId == id);
            if (categorie == null) {
                return NotFound();
            }
            var categViewModel = Mapper.Map<Categorie, CategorieViewModel>(categorie);
            return View(categViewModel);
        }

        // POST: Admin/EditCategorie/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategorie(int id, [Bind("CategorieId,Nom,Description")] CategorieViewModel categViewModel) {
            if (id != categViewModel.CategorieId) {
                return NotFound();
            }

            // Valider l'unicité du nom de la catégorie..
            if (_context.Categories.FirstOrDefault(p => p.Nom.Equals(categViewModel.Nom)) != null)
                ModelState.AddModelError("CustomValid", "This categorie name already exists. Try again.");
            // Fin validation

            Categorie categorie = Mapper.Map<CategorieViewModel, Categorie>(categViewModel);
            if (ModelState.IsValid && categorie != null) {
                try {
                    _context.Update(categorie);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!CategorieExists(categorie.CategorieId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categViewModel);
        }

        // GET: Admin/DeleteCategorie/5
        public async Task<IActionResult> DeleteCategorie(int? id) {
            if (id == null) {
                return NotFound();
            }

            var categorie = await _context.Categories
                .SingleOrDefaultAsync(m => m.CategorieId == id);
            if (categorie == null) {
                return NotFound();
            }
            var categViewModel = Mapper.Map<Categorie, CategorieViewModel>(categorie);
            return View(categViewModel);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategorieConfirmed([Bind("CategorieId")] CategorieViewModel categViewModel) {
            int id = categViewModel.CategorieId;
            var categorie = await _context.Categories.SingleOrDefaultAsync(m => m.CategorieId == id);
            _context.Categories.Remove(categorie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategorieExists(int id) {
            return _context.Categories.Any(e => e.CategorieId == id);
        }


        // ****************************************************************** ///
        // MEMBRES
        public IActionResult Membres() {
            return View();
        }
    }
}