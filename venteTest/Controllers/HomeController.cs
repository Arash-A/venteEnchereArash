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
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using venteTest.Models.HomeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using venteTest.Services;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using System.Net;
using System.Net.Mail;

namespace venteTest.Controllers
{
    public class HomeController : Controller

    {
        private readonly LibraryContext _context;

        public HomeController(LibraryContext context)
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

            ViewBag.Categories = _context.Categories.ToList(); //pour ComboBox

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

            int pageSize = 5;
            //return View(await objets.AsNoTracking().ToListAsync());
            return View(await PaginatedList<Objet>.CreateAsync(objets.AsNoTracking(), page ?? 1, pageSize));
        }

        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: ({0})</p><p>Message:</p><p>{1}</p>";
                //var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("VenteEnchereM9@gmail.com"));  // replace with valid value 
                message.From = new MailAddress("sender@outlook.com");  // replace with valid value
                message.Subject = "Demande de contact - Site web VenteEnchere";
                message.Body = string.Format(body, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "VenteEnchereM9@gmail.com",  // replace with valid value
                        Password = "!qwerty123"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
