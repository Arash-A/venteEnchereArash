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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Rewrite;

namespace venteTest.Controllers
{
    public class ObjetsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment he;
        private readonly IServiceProvider _serviceProvider;

        public ObjetsController(ApplicationDbContext context, IHostingEnvironment e, IServiceProvider serviceProvider)
        {
            _context = context;
            he = e;
            _serviceProvider = serviceProvider;
        }

        // GET: Objets
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Index(string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {


            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : ""; // name asc
            ViewData["PrixDepartSortParm"] = sortOrder == "prix_asc" ? "prix_desc" : "prix_asc";
            ViewData["DateInscritSortParm"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewData["DureeMiseVenteSortParm"] = sortOrder == "duree_asc" ? "duree_desc" : "duree_asc";
            ViewData["CategorieSortParm"] = sortOrder == "categ_asc" ? "categ_desc" : "categ_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var userName = await userManager.FindByNameAsync(User.Identity.Name);
            // --ajout Arash ---- Lister des objets qui appartient de l'utilisateur
            string query = "SELECT * FROM Objets WHERE VendeurId = {0} AND Status={1}";
            //var objets = from o in _context.Objets.Include(o => o.Categorie).FromSql(query, "5e22a045-25ca-4554-b08f-afe64c71e10c", 0) select o;
            // var objets = from o in _context.Objets.Include(o => o.Categorie).FromSql(query, userName.Id, 0) select o;
            var objets = from o in _context.Objets.
                         Include(o => o.Categorie).
                         Include(o => o.Vendeur).
                         Include(o => o.Acheteur).
                         Include(o => o.Fichiers).
                         Include(o => o.Encheres).
                         ThenInclude(o => o.Miseur).
                         FromSql(query, "5e22a045-25ca-4554-b08f-afe64c71e10c", 0)
                         select o;
            //var objets = from o in _context.Objets.
            //            Include(o => o.Categorie).
            //            Include(o => o.Vendeur).
            //            Include(o => o.Acheteur).
            //            Include(o => o.Fichiers).
            //            Include(o => o.Encheres).
            //            ThenInclude(o => o.Miseur).
            //            FromSql(query, userName.Id, 0)
            //             select o;

            ViewBag.Categories = _context.Categories.ToList(); //pour ComboBox

            if (!String.IsNullOrEmpty(searchString))
            {
                objets = objets.Where(s => s.Nom.Contains(searchString)
                || s.Description.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    objets = objets.OrderByDescending(s => s.Nom);
                    break;
                case "prix_asc":
                    objets = objets.OrderBy(s => s.PrixDepart);
                    break;
                case "prix_desc":
                    objets = objets.OrderByDescending(s => s.PrixDepart);
                    break;
                case "date_asc":
                    objets = objets.OrderBy(s => s.DateInscription);
                    break;
                case "date_desc":
                    objets = objets.OrderByDescending(s => s.DateInscription);
                    break;
                case "duree_asc":
                    objets = objets.OrderBy(s => s.DureeMiseVente);
                    break;
                case "duree_desc":
                    objets = objets.OrderByDescending(s => s.DureeMiseVente);
                    break;
                case "categ_asc":
                    objets = objets.OrderBy(s => s.Categorie.Nom);
                    break;
                case "categ_desc":
                    objets = objets.OrderByDescending(s => s.Categorie.Nom);
                    break;
                default: // name asc
                    objets = objets.OrderBy(s => s.Nom);
                    break;
            }

            int pageSize = 4;
            return View(await PaginatedList<Objet>.CreateAsync(objets.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Objets/Details/5
        [Authorize(Roles = "Member")]
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
            ViewBag.Categories = _context.Categories.ToList();
            return View(objet);
        }

        // GET: Objets/Create
        [Authorize(Roles = "Member")]
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
        [Authorize(Roles = "Member")]
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
        [Authorize(Roles = "Member")]
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
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> ListeAAcheter(string sortOrder,
           string currentFilter,
           string searchString,
           int? page)
        {


            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : ""; // name asc
            ViewData["PrixDepartSortParm"] = sortOrder == "prix_asc" ? "prix_desc" : "prix_asc";
            ViewData["DateInscritSortParm"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewData["DureeMiseVenteSortParm"] = sortOrder == "duree_asc" ? "duree_desc" : "duree_asc";
            ViewData["CategorieSortParm"] = sortOrder == "categ_asc" ? "categ_desc" : "categ_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var userName = await userManager.FindByNameAsync(User.Identity.Name);
            // --ajout Arash ---- Lister des objets qui appartient de l'utilisateur
            string query = "SELECT * FROM Objets WHERE VendeurId = {0} AND Status={1}";
            //var objets = from o in _context.Objets.Include(o => o.Categorie).FromSql(query, "5e22a045-25ca-4554-b08f-afe64c71e10c", 0) select o;
            // var objets = from o in _context.Objets.Include(o => o.Categorie).FromSql(query, userName.Id, 0) select o;
            var objets = from o in _context.Objets.
                         Include(o => o.Categorie).
                         Include(o => o.Vendeur).
                         Include(o => o.Acheteur).
                         Include(o => o.Fichiers).
                         Include(o => o.Encheres).
                         ThenInclude(o => o.Miseur).
                         FromSql(query, "5e22a045-25ca-4554-b08f-afe64c71e10c", 0)
                         select o;
            //var objets = from o in _context.Objets.
            //            Include(o => o.Categorie).
            //            Include(o => o.Vendeur).
            //            Include(o => o.Acheteur).
            //            Include(o => o.Fichiers).
            //            Include(o => o.Encheres).
            //            ThenInclude(o => o.Miseur).
            //            FromSql(query, userName.Id, 0)
            //             select o;


            ViewBag.Categories = _context.Categories.ToList(); //pour ComboBox

            if (!String.IsNullOrEmpty(searchString))
            {
                objets = objets.Where(s => s.Nom.Contains(searchString)
                || s.Description.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    objets = objets.OrderByDescending(s => s.Nom);
                    break;
                case "prix_asc":
                    objets = objets.OrderBy(s => s.PrixDepart);
                    break;
                case "prix_desc":
                    objets = objets.OrderByDescending(s => s.PrixDepart);
                    break;
                case "date_asc":
                    objets = objets.OrderBy(s => s.DateInscription);
                    break;
                case "date_desc":
                    objets = objets.OrderByDescending(s => s.DateInscription);
                    break;
                case "duree_asc":
                    objets = objets.OrderBy(s => s.DureeMiseVente);
                    break;
                case "duree_desc":
                    objets = objets.OrderByDescending(s => s.DureeMiseVente);
                    break;
                case "categ_asc":
                    objets = objets.OrderBy(s => s.Categorie.Nom);
                    break;
                case "categ_desc":
                    objets = objets.OrderByDescending(s => s.Categorie.Nom);
                    break;
                default: // name asc
                    objets = objets.OrderBy(s => s.Nom);
                    break;
            }

            int pageSize = 4;
            return View(await PaginatedList<Objet>.CreateAsync(objets.AsNoTracking(), page ?? 1, pageSize));
        }
    }

}
