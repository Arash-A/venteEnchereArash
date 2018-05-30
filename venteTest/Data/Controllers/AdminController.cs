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
using venteTest.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using venteTest.Models.Rapports;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;

namespace venteTest.Controllers {
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostingEnvironment he;

        public AdminController(ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager,
                                IEmailSender emailSender,
                                IServiceProvider serviceProvider,
                                IHostingEnvironment e) {

            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
            _serviceProvider = serviceProvider;
            he = e;
        }

        // CATEGORIES
        public async Task<IActionResult> Index() {
            IList<Categorie> lstCateg = await _context.Categories.ToListAsync();
            IList<CategorieViewModel> lstCategViewModel = Mapper.Map<IList<Categorie>, IList<CategorieViewModel>>(lstCateg);
            return View(lstCategViewModel);
        }

        public IActionResult CreateCategorie() {
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
                TempData["message"] = $"Categorie '{categ.Nom}' has been created.";
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
            if (_context.Categories.FirstOrDefault(p => p.Nom.Equals(categViewModel.Nom) && !p.CategorieId.Equals(categViewModel.CategorieId)) != null)
                ModelState.AddModelError("CustomValid", "This categorie name already exists. Try again.");
            // Fin validation

            Categorie categorie = Mapper.Map<CategorieViewModel, Categorie>(categViewModel);
            if (ModelState.IsValid && categorie != null) {
                try {
                    _context.Update(categorie);
                    await _context.SaveChangesAsync();
                    TempData["message"] = $"Categorie '{categorie.Nom}' has been updated.";
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

            // Valider que la catégorie n'est pas un objet en vente ou vendu...
            if (_context.Objets.FirstOrDefault(p => p.CategorieID == id) != null) {
                //ModelState.AddModelError("CustomValid", $"Error. You cannot delete the categorie '{categorie.Nom}'. An object for sale with this categorie name already exists.");
                if (_context.Objets.FirstOrDefault(p => p.CategorieID == id).Acheteur != null)
                    TempData["message"] = $"Error. You cannot delete the categorie '{categorie.Nom}'. An object already sold with this categorie name already exists.";
                else
                    TempData["message"] = $"Error. You cannot delete the categorie '{categorie.Nom}'. An object currently for sale with this categorie name already exists.";

                IList<Categorie> lstCateg = await _context.Categories.ToListAsync();
                IList<CategorieViewModel> lstCategViewModel = Mapper.Map<IList<Categorie>, IList<CategorieViewModel>>(lstCateg);
                return View("Index", lstCategViewModel);
            }
            // Fin validation

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
            TempData["message"] = $"Categorie '{categorie.Nom}' has been deleted.";
            return RedirectToAction(nameof(Index));
        }

        private bool CategorieExists(int id) {
            return _context.Categories.Any(e => e.CategorieId == id);
        }
        // FIN CATEGORIES
        // ****************************************************************** ///


        // ****************************************************************** ///
        // MEMBRES
        public async Task<IActionResult> Membres(string members = "") {

            IList<ApplicationUser> lstMembers = await _userManager.GetUsersInRoleAsync("Member");

            if (!(members == "" || members == "AllMembers")) {
                // Afficher les nouveaux membres inscrits depuis 24 heures
                lstMembers = lstMembers.Where(p => p.DateInscription > DateTime.Now.Subtract(TimeSpan.FromDays(1))).ToList();
                ViewBag.AllMembers = "all";
            }
            IList<MemberViewModel> model = Mapper.Map<IList<ApplicationUser>, IList<MemberViewModel>>(lstMembers);

            return View(model);

        }


        public async Task<IActionResult> ToggleMember(string email) {
            //
            if (email == null) {
                return RedirectToAction(nameof(Index));
            }
            var _user = await _userManager.FindByEmailAsync(email);
            if (_user == null) {
                throw new ApplicationException($"Unable to load user with email '{email}'.");
            }
            //Toggle la propriété pour désactivé membre
            if (_user.EmailConfirmed) {
                _user.EmailConfirmed = false;
                TempData["message"] = $"Member '{email}' has been deactivated.";
            } else {
                _user.EmailConfirmed = true;
                TempData["message"] = $"Member '{email}' has been activated.";
            }

            var result = await _userManager.UpdateAsync(_user); // MAJ
            if (result.Succeeded) {
                //Montre un message de succès...

            } else {
                TempData["message"] = $"Failed to update activation status of '{email}'.";
            }

            return RedirectToAction(nameof(Membres));
        }
        public async Task<IActionResult> EmailMember(string email) {
            if (email == null) {
                TempData["message"] = $"Enter an email adress.";
                return RedirectToAction(nameof(Membres));
            }
            var _user = await _userManager.FindByEmailAsync(email);
            if (_user == null) {
                throw new ApplicationException($"Unable to load user with email '{email}'.");
            }
            SendEmailViewModel model = new SendEmailViewModel { ToEmail = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmailMember(SendEmailViewModel emailVM) {

            // Valider existence du membre
            var _user = await _userManager.FindByEmailAsync(emailVM.ToEmail);
            if (_user == null) {
                ModelState.AddModelError("CustomValid", $"Error. No member with email '{emailVM.ToEmail}' exists in the system.");
                return View(emailVM);
            }
            // Fin valider existence du membre

            // Envoyer courriel à membre
            await _emailSender.SendEmailAsync(emailVM.ToEmail, emailVM.Titre, emailVM.Message);
            TempData["message"] = $"Message sent to member with email '{emailVM.ToEmail}'.";
            return RedirectToAction(nameof(Membres));
        }
        // FIN MEMBRES
        // ****************************************************************** ///

        // ****************************************************************** ///
        // BidConfiguration
        public async Task<IActionResult> BidConfiguration() {

            // afficher la config des enchères et pouvoir en creer
            IList<ConfigurationAdmin> lstConfigs = await _context.ConfigurationAdmins.ToListAsync();
            IList<ConfigurationAdminViewModel> lstConfigsViewModel = Mapper.Map<IList<ConfigurationAdmin>, IList<ConfigurationAdminViewModel>>(lstConfigs);
            return View(lstConfigsViewModel);
        }

        public IActionResult CreateBidConfiguration() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBidConfiguration([Bind("TauxGlobalComissionAuVendeur,PasGlobalEnchere")]  ConfigurationAdminViewModel configurationVM) {

            // validation manuelle:
            string errMsg = "Please correct the following errors : \n";
            bool error = false;
            decimal pasEnchere;
            decimal tauxGlobalComissionAuVendeur;
            if (!decimal.TryParse(configurationVM.PasGlobalEnchere.ToString(), out pasEnchere) || pasEnchere <= 0 || pasEnchere >= 50) {
                errMsg += " - The bid step must be a value between 0 and 50 \n";
                error = true;
            }
            if (!decimal.TryParse(configurationVM.TauxGlobalComissionAuVendeur.ToString(), out tauxGlobalComissionAuVendeur) || tauxGlobalComissionAuVendeur <= 0 || (tauxGlobalComissionAuVendeur) >= 50) {
                errMsg += " - The bid commision cost value between 0 and 50 \n";
                error = true;
            }

            if (error) {
                ModelState.AddModelError("CustomValid", errMsg);
                return View(configurationVM);
            }

            // AUCUNE ERREUR: 
            configurationVM.ConfigurationAdminId = 0;
            configurationVM.PasGlobalEnchere = pasEnchere;
            configurationVM.TauxGlobalComissionAuVendeur = tauxGlobalComissionAuVendeur / 100m;
            //if (ModelState.IsValid) {

            var configuration = new ConfigurationAdmin {
                ConfigurationAdminId = 0,
                PasGlobalEnchere = configurationVM.PasGlobalEnchere,
                TauxGlobalComissionAuVendeur = configurationVM.TauxGlobalComissionAuVendeur
            };

            _context.Add(configuration);
            await _context.SaveChangesAsync();
            TempData["message"] = $"Created sucessfully new configuration # '{configuration.ConfigurationAdminId}'.";
            return RedirectToAction(nameof(BidConfiguration));
            //}
        }

        // GET:
        public async Task<IActionResult> DeleteBidConfiguration(int? id) {
            if (id == null) {
                return NotFound();
            }

            var configuration = await _context.ConfigurationAdmins
                .SingleOrDefaultAsync(m => m.ConfigurationAdminId == id);
            if (configuration == null) {
                return NotFound();
            }

            // Valider que la configuration n'est pas un objet en vente ou vendu...
            if (_context.Objets.FirstOrDefault(p => p.ConfigurationAdmin.ConfigurationAdminId == id) != null) {
                TempData["message"] = $"Error. You cannot delete this bid configuration. An object already sold or for sale with this bid configuration already exists.";

                IList<ConfigurationAdmin> lstConfigs = await _context.ConfigurationAdmins.ToListAsync();
                IList<ConfigurationAdminViewModel> lstConfigsViewModel = Mapper.Map<IList<ConfigurationAdmin>, IList<ConfigurationAdminViewModel>>(lstConfigs);
                return View("BidConfiguration", lstConfigsViewModel);
            }
            // Fin validation

            var model = Mapper.Map<ConfigurationAdmin, ConfigurationAdminViewModel>(configuration);
            return View(model);
        }

        // POST:
        [HttpPost, ActionName("DeleteBidConfiguration")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBidConfigurationConfirmed(ConfigurationAdminViewModel configVM) {

            int id = configVM.ConfigurationAdminId;
            var configuration = await _context.ConfigurationAdmins.SingleOrDefaultAsync(m => m.ConfigurationAdminId == id);
            _context.ConfigurationAdmins.Remove(configuration);
            await _context.SaveChangesAsync();
            TempData["message"] = $"Bid configuration '{configuration.ConfigurationAdminId}' has been deleted.";
            return RedirectToAction(nameof(BidConfiguration));
        }

        private bool ConfigurationExists(int id) {
            return _context.ConfigurationAdmins.Any(e => e.ConfigurationAdminId == id);
        }


        // FIN BidConfiguration
        // ****************************************************************** ///

        // ****************************************************************** ///

        // List objets en vente, en cours, vendu, etc.

        public async Task<IActionResult> Objets(string objects = "", string state = "", string email = "") {

            IList<ApplicationUser> lstMembers = await _userManager.GetUsersInRoleAsync("Member");
            ViewBag.lstMembers = lstMembers;

            string query = "SELECT * FROM Objets"; //

            //on load bcp de paramètres...
            var objets = from o in _context.Objets.
                         Include(o => o.Categorie).
                         Include(o => o.Vendeur).
                         Include(o => o.Acheteur).
                         Include(o => o.Fichiers).
                         Include(o => o.Encheres).
                            ThenInclude(o => o.Miseur).
                         FromSql(query, 0)
                         select o;

            if (!(objects == "" || objects == "AllObjects")) {
                // Afficher nouveaux
                ViewBag.AllObjects = "all";
                objets = objets.Where(p => p.DateInscription > DateTime.Now.Subtract(TimeSpan.FromDays(1)));
            }

            if (!(state == "")) {
                // Afficher nouveaux
                if (state == "SoldObjects") {
                    ViewBag.StateObjects = "Sold";
                    objets = objets.Where(p => p.Status == Status.Vendu);
                } else {
                    ViewBag.StateObjects = "OnSale";
                    objets = objets.Where(p => p.Status == Status.EnVente);
                }
            }

            if (email != "") {
                objets = objets.Where(p => p.Vendeur.Email.Equals(email));
                ViewBag.EmailSel = email;
            }

            IList<Objet> obs = objets.ToList();

            IList<ObjetsViewModel> model = Mapper.Map<IList<Objet>, IList<ObjetsViewModel>>(obs);
            return View(model);
        }

        //Rapport #4 - PDF - Cote de popularité des membres incluant le niveau de la cote, le nombre d’évaluations.
        [HttpGet]
        public async Task<IActionResult> MembresCote() {
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x =>
                                               new SelectListItem() {
                                                   Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1] + " (" + x + ")",
                                                   Value = x.ToString()
                                               }), "Value", "Text");

            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year - 5, 6).Select(x =>
                                               new SelectListItem() {
                                                   Text = x.ToString(),
                                                   Value = x.ToString()
                                               }), "Value", "Text");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MembresCote(SendCotesReportViewModel sendCotesReportViewModel) {

            if (ModelState.IsValid) {
                // Créer et envoyer rapport PDF:
                RapportsClass rapportsClass = new RapportsClass(_context, this.ControllerContext, he);
                // Chercher l'admin courriel
                var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var userName = await userManager.FindByNameAsync(User.Identity.Name);
                // envoyer rapport
                rapportsClass.rapport4(userName.Email, sendCotesReportViewModel);

                return RedirectToAction(nameof(MembresCote));
            }

            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x =>
                                   new SelectListItem() {
                                       Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1] + " (" + x + ")",
                                       Value = x.ToString()
                                   }), "Value", "Text");

            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year - 5, 6).Select(x =>
                                               new SelectListItem() {
                                                   Text = x.ToString(),
                                                   Value = x.ToString()
                                               }), "Value", "Text");
            return View(sendCotesReportViewModel);

        }
        // Fin rapport #4

        //Rapport #5 - PDF - Synthèse des ventes réalisées et des commissions perçues durant l’année
        public async Task<IActionResult> Ventes() {

            ViewBag.Years = new List<int> { 2018 };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Ventes(SendVentesReportViewModel sendVentesReportViewModel) {

            if (ModelState.IsValid) { 
                // Créer et envoyer rapport PDF:
                RapportsClass rapportsClass = new RapportsClass(_context, this.ControllerContext, he);
                // Chercher l'admin courriel
                var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var userName = await userManager.FindByNameAsync(User.Identity.Name);
                // envoyer rapport
                rapportsClass.rapport5(sendVentesReportViewModel.Year,
                                        userName.Email);

                return RedirectToAction(nameof(Ventes));
            }

            ViewBag.Years = new List<int> { 2018 };
            return View(sendVentesReportViewModel);

        }
        // Fin rapport #5

        public async Task<IActionResult> rapSettings() {

            return View();
        }

    }
}