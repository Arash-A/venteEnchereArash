﻿using System;
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
using venteTest.Models.ObjetViewModel;
using venteTest.Services;
using AutoMapper;

using System.Collections;
using System.Net.Mail;


using Hangfire;
using System.Net;
using System.Text;

namespace venteTest.Controllers
{
    public class ObjetsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment he;
        private readonly IServiceProvider _serviceProvider;
        private int index = 0;
        private  static decimal bidMove = 0;
      

        public ObjetsController(ApplicationDbContext context, IHostingEnvironment e, IServiceProvider serviceProvider)
        {
            _context = context;
            he = e;
            _serviceProvider = serviceProvider;
        }

        // GET: Objets
        [Authorize(Roles = "Member, Admin, Manager")]
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
                         FromSql(query, userName.Id, 0)
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
        [Authorize(Roles = "Member, Admin, Manager")]
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
        [Authorize(Roles = "Member, Admin, Manager")]
        public IActionResult Create()
        {
            List<Categorie> CategoryList = new List<Categorie>();

            CategoryList = (from categorie in _context.Categories select categorie).ToList();
            CategoryList.Insert(0, new Categorie { CategorieId = 1000, Nom = "Select" });
            ViewBag.CategoryList = CategoryList;

            ViewData["CategorieID"] = new SelectList(_context.Categories, "Nom", "Nom");

            AnnoncerObjectViewModel objVM = new AnnoncerObjectViewModel() { DateInscription = DateTime.Now, PrixDepart = 50 };

            return View(objVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnnoncerObjectViewModel objetVM, IFormFile pic, IList<IFormFile> attachments) {

             if (ModelState.IsValid) {

                //validation supplémentaire 
                if (objetVM.CategorieID == 1000) {
                    List<Categorie> CategoryList = new List<Categorie>();

                    CategoryList = (from categorie in _context.Categories select categorie).ToList();
                    CategoryList.Insert(0, new Categorie { CategorieId = 1000, Nom = "Select" });
                    ViewBag.CategoryList = CategoryList;

                    ViewData["CategorieID"] = new SelectList(_context.Categories, "Nom", "Nom");

                    ModelState.AddModelError("", "Select Category");
                    return View(objetVM);
                } //fin validation sup

                Objet objet = Mapper.Map<AnnoncerObjectViewModel, Objet>(objetVM);

                // Affectation des autres propriétés requise... (Vendeur, Miseur, DateInscription, imageUrl, ConfigurationAdmin, Categorie, List d'enchere et Liste de fichiers)
                var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var userName = await userManager.FindByNameAsync(User.Identity.Name);

                objet.Vendeur = (Vendeur)userName;
                objet.DateInscription = DateTime.Now;
         

                if (pic != null)
                {
                    var file = Path.GetFileName(pic.FileName);
                    var fileName = System.IO.Path.Combine(he.WebRootPath, "Uploads") + $@"\{file}";
                    pic.CopyTo(new FileStream(fileName, FileMode.Create));
                    objet.imageUrl = "Uploads/" + file;
                }

                //carles 
                foreach (IFormFile item in attachments)
                {

                    if (item != null)
                    {
                        var attachmentfile = Path.GetFileName(item.FileName);
                        var attachmentFileName = System.IO.Path.Combine(he.WebRootPath, "Attachments") + $@"\{attachmentfile}";
                        item.CopyTo(new FileStream(attachmentFileName, FileMode.Create));
                        objet.Fichiers.Add(new Fichier() {NomOriginal= attachmentfile,NomLocale= attachmentFileName ,verseLe=DateTime.Now});
                    }
                }

                objet.Categorie = _context.Categories.FirstOrDefault(p => p.CategorieId == objetVM.CategorieID);
                objet.ConfigurationAdmin = _context.ConfigurationAdmins.Last();
                objet.Encheres = new List<Enchere>();
                objet.Fichiers = new List<Fichier>();

                //Affectation d'une 1e mise.. (Par défaut celui qui crée un objet place une première mise sur son objet, comme ça il est facturé si jamais personne mise et il "remporte" son objet)
                Miseur miseur = (Miseur)userName;
                objet.Encheres.Add(new Enchere() { Objet = objet, Niveau = objet.PrixDepart, MiseMax = objet.PrixDepart, Miseur = miseur });
                // Fin affection 1e mise 

              
                // Fin affectation des autres propriétés requises

                //Ajout à la BD de l'Objet à vendre ainsi qu'une première mise par défaut
                _context.Add(objet);
                await _context.SaveChangesAsync();
                //Fin ajout BD

                //Ajout Arash pour mettre objet en status Vendu apres certain temp !!!!!!!!!!!!!! ces ligne du code doit être exactement ici !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                BackgroundJob.Schedule(() => ChangeStatus(objet.ObjetID), objet.DateLimite);

                TempData["message"] = $"Objet '{objet.Nom}' has been created for bidding starting now and ending at '{objet.DateLimite}'.";
                return RedirectToAction(nameof(Index));
            }
            return View(objetVM);
        }


        public void ChangeStatus(int objId) {
            // var objetNew = _context.Objets.Find(objId);
            // objetNew.Status = Status.Vendu;

            string query = "SELECT * FROM Objets WHERE ObjetId = {0}";

            var objetNewQry = from o in _context.Objets.
                             Include(o => o.ConfigurationAdmin).
                             Include(o => o.Categorie).
                             Include(o => o.Vendeur).
                             Include(o => o.Acheteur).
                             Include(o => o.Fichiers).
                             Include(o => o.Encheres).
                                ThenInclude(o => o.Miseur).
                             FromSql(query, objId)
                            select o;

            Objet objetNew = objetNewQry.FirstOrDefault();

            objetNew.Status = Status.Vendu;

            // déterminer le gagnant et l'enregistrer:
            var enchereMenante = objetNew.Encheres.OrderByDescending(t => t.Niveau).FirstOrDefault();
            var miseurMenantEnchere = enchereMenante.Miseur;
            objetNew.Acheteur = miseurMenantEnchere;
            // fin déterminer gagnant

            // enregistrer le prix de vente burte
            objetNew.PrixVenteBrute = enchereMenante.Niveau;

            // enregistrer la commission sur cette vente
            objetNew.Commission = enchereMenante.Niveau * (double)objetNew.ConfigurationAdmin.TauxGlobalComissionAuVendeur;

            _context.SaveChanges();
        }

        // POST: Objets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        /* 
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
 */
        // GET: Objets/Delete/5
        [Authorize(Roles = "Member, Admin, Manager")]
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
    [Authorize(Roles = "Member, Admin, Manager")]
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
    [Authorize(Roles = "Member, Admin, Manager")]
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
        [Authorize(Roles = "Member, Admin, Manager")]
        public async Task<IActionResult> Miser(int id)
        {

            var objet = await _context.Objets
                            .Include(o => o.Encheres)
                            .ThenInclude(o => o.Miseur)
                            .Include(o => o.ConfigurationAdmin)
                            .Include(o => o.Acheteur)
                            .Include(o => o.Vendeur)
                            .SingleOrDefaultAsync(m => m.ObjetID == id);

            var encheres = objet.Encheres.ToList();
            bidMove = objet.ConfigurationAdmin.PasGlobalEnchere;
            Double[] enchereTableau = new Double[encheres.Count];
           
            var i = 0;
            foreach (Enchere item in encheres)
            {
                enchereTableau[i] = item.Niveau;
                i++;
            }

            var bestBid = Max(enchereTableau);

            Enchere newBid = new Enchere { Objet = objet, Niveau = bestBid,ObjetId=id};

            return View(newBid);
        }
        
        [HttpPost]
        public async Task<IActionResult> Miser(Enchere newEnchere)
        {

            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var userName = await userManager.FindByNameAsync(User.Identity.Name);
            Miseur miseur = (Miseur)userName;

            if (newEnchere.MiseMax< newEnchere.Niveau)
            {
                var nextMinBid = newEnchere.Niveau + (double)bidMove;
                ModelState.AddModelError(string.Empty, "Prochain montant d'enchère valide est:$"+ nextMinBid);
                return View(newEnchere);
            }
                

            var objet = await _context.Objets
                .Include(o => o.Encheres)
                .ThenInclude(o => o.Miseur)
                .Include(o => o.ConfigurationAdmin)
                .Include(o => o.Acheteur)
                .Include(o => o.Vendeur)
                .SingleOrDefaultAsync(m => m.ObjetID == newEnchere.ObjetId);

            var newMaxBid = newEnchere.MiseMax;
            //objet.ConfigurationAdmin = _context.ConfigurationAdmins.Last();

            var pas = objet.ConfigurationAdmin.PasGlobalEnchere;
            var encheres = objet.Encheres.ToList();
           
            Double[] enchereTableau = new Double[encheres.Count];
            Miseur[] lesMiseurs = new Miseur[encheres.Count];

            var  i = 0;
            foreach (Enchere item in encheres)
            {
                enchereTableau[i] = item.MiseMax;
                lesMiseurs[i] = item.Miseur;
                i++;
            }

            var bestBid = Max(enchereTableau);
            var newBid = 0.00;

            var msgFr = "Un autre membre a surenchérit sur l'objet.Si vous n'augmentez pas votre enchère maximum vous perdrez l'objet";
            var msgEn = "Another member outbid the object .If you do not increase your maximum bid you will lose the item";
            var msg = msgFr;

            if (newMaxBid > bestBid)
            {
                for(int j=0;j<encheres.Count; j++)
                {
                    //Le système surenchérit automatiquement pour tous à l'enchère maximum.
                    encheres[j].Niveau = encheres[j].MiseMax;
                    //envoyerun message aux concernés les avisant q un autre mebre a surencherit sur l'objet 
                    
                    if (encheres[j].Miseur.Langue == "en")
                    {
                        msg = msgEn;
                    }
                    notifyBiders(miseur.Email, msg);
                }

                newBid = bestBid + (double)pas;
            }
            else
            {
                for (int j = 0; j < encheres.Count; j++)
                {
                    if(newMaxBid > encheres[j].MiseMax)
                    {
                        //Le système surenchérit automatiquement pour tous à l'enchère maximum.
                        encheres[j].Niveau = encheres[j].MiseMax;
                        //envoyerun message aux concernés les avisant q un autre mebre a surencherit sur l objet 

                        if (encheres[j].Miseur.Langue == "en")
                        {
                            msg = msgEn;
                        }
                        notifyBiders(miseur.Email, msg);
                    }

                    if (newMaxBid <= encheres[j].MiseMax)
                    {
                        //Le système surenchérit automatiquement pour tous à l'enchère maximum.
                        encheres[j].Niveau = ((newMaxBid + (double)pas)< encheres[j].MiseMax)?(newMaxBid + (double)pas):encheres[j].MiseMax;

                        //on avise le miseur actuel selon sa langue parlée
                        if (encheres[j].Miseur.Langue=="en")
                        {
                            msg = msgEn;
                        }
                        notifyBiders(miseur.Email, msg);
                    }
                }
                newBid = newMaxBid;
            }

        Miseur losingBidder = lesMiseurs[index];

        objet.Fichiers = new List<Fichier>();

        newEnchere.Niveau = newBid;
        newEnchere.MiseMax = newMaxBid;
        newEnchere.Miseur = miseur;

        objet.Encheres.Add(newEnchere);
        _context.Update(objet);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Home");
        }
        
    
        private double Max( double[] tab)
        {
           double  max = tab[0];

            for(int i=1;i < tab.Length; i++)
            {
                if (tab[i]>max)
                {
                    max = tab[i];
                    index = i;
                }
            }
            return max;
        }

       
        private MailMessage BuildMailMessage(string recipient,string content)
        {
            var message = new StringBuilder();
            //message.AppendFormat("Date: {0:yyyy-MM-dd hh:mm}\n", DateTime.Now);
           // message.AppendFormat("Subject:Notification \n");
            message.AppendFormat(content);

            return new MailMessage(
            "VenteEnchereM9@gmail.com", // From
             recipient, // To
            content, // Subject
            message.ToString() // Body
            );
        }



       private void notifyBiders(string recipient,string content) 
        {
            using (var smtpClient = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "VenteEnchereM9@gmail.com",  // replace with valid value
                    Password = "!qwerty123"  // replace with valid value
                };

                smtpClient.Credentials = credential;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;

                using (var mailMessage = BuildMailMessage(recipient, content))
                {
                    smtpClient.Send(mailMessage);
                }
            } 
           
        }

    }

}
