using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using venteTest.Data;
using venteTest.Models;

namespace venteTest.Controllers
{
    public class ObjetsController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IHostingEnvironment he;

        public ObjetsController(LibraryContext context, IHostingEnvironment e)
        {
            _context = context;
            he = e;
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
            List<Categorie> CategoryList = new List<Categorie>();

            CategoryList = (from categorie in _context.Categories select categorie).ToList();
            CategoryList.Insert(0, new Categorie { CategorieId = 1000, Nom = "Select" });
            ViewBag.CategoryList = CategoryList;

            ViewData["CategorieID"] = new SelectList(_context.Categories, "Nom", "Nom");
            return View();
        }

        // POST: Objets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Objet objet, IFormFile pic)
        {
            if (!ModelState.IsValid)
            {
                if (objet.CategorieID == 1000)
                {
                    ModelState.AddModelError("", "Select Category");
                }



                if (pic != null)
                {

                    var file = Path.GetFileName(pic.FileName);
                    var fileName = System.IO.Path.Combine(he.WebRootPath, "Uploads") + $@"\{file}";
                    pic.CopyTo(new FileStream(fileName, FileMode.Create));

                    objet.DateInscription = DateTime.Now;

                    objet.imageUrl = "/Uploads/" + file;

                }

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
            Objet item = _context.Objets.Find(id);

            List<Categorie> CategoryList = new List<Categorie>();

            CategoryList = (from categorie in _context.Categories select categorie).ToList();
            CategoryList.Insert(0, new Categorie { CategorieId = item.CategorieID, Nom = item.Categorie.Nom });
            ViewBag.CategoryList = CategoryList;

            //ViewData["CategorieID"] = new SelectList(_context.Categories, "CategorieId", "CategorieId", objet.CategorieID);
            return View(objet);
        }

        // POST: Objets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Objet objet, IFormFile pic)
        {

            Objet objetDB = _context.Objets.Find(objet.ObjetID);


            if (ModelState.IsValid)
            {
                try
                {
                    objetDB.CategorieID = objet.CategorieID;
                    objetDB.Nom = objet.Nom;
                    objetDB.Description = objet.Description;
                    objetDB.Description = objet.Description;
                    objetDB.PrixDepart = objet.PrixDepart;


                    if (pic != null)
                    {
                        var file = Path.GetFileName(pic.FileName);
                        var fileName = System.IO.Path.Combine(he.WebRootPath, "Uploads") + $@"\{file}";
                        pic.CopyTo(new FileStream(fileName, FileMode.Create));

                        objetDB.imageUrl = "/Uploads/" + file;

                    }

                    _context.Update(objetDB);
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

            return Redirect("Home/Index"); ;
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
