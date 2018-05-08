using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using venteTest.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using venteTest.Data;


namespace venteTest.Controllers
{
    public class HomeController : Controller

    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page) { 

            //var applicationDbContext = _context.Objets.Include(o => o.Categorie);

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : ""; // name asc
            ViewData["PrixDepartSortParm"] = sortOrder == "prix_asc" ? "prix_desc" : "prix_asc";
            ViewData["DateInscritSortParm"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewData["DureeMiseVenteSortParm"] = sortOrder == "duree_asc" ? "duree_desc" : "duree_asc";
            ViewData["CategorieSortParm"] = sortOrder == "categ_asc" ? "categ_desc" : "categ_asc";

            if (searchString != null) {
                page = 1;
            } else {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var objets = from o in _context.Objets.Include(o => o.Categorie)
                           select o;

            if (!String.IsNullOrEmpty(searchString)) {
                objets = objets.Where(s => s.Nom.Contains(searchString)
                || s.Description.Contains(searchString));
            }


            switch (sortOrder) {
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

            int pageSize = 3;
            //return View(await objets.AsNoTracking().ToListAsync());
            return View(await PaginatedList<Objet>.CreateAsync(objets.AsNoTracking(), page ?? 1, pageSize));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
