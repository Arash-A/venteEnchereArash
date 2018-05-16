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

namespace venteTest.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public AdminController(ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager,
                                IEmailSender emailSender) {

            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
        }

        public AdminController()
        {
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
        public async Task<IActionResult> Membres() {

            IList<ApplicationUser> lstMembers =  await _userManager.GetUsersInRoleAsync("Member");
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

        public async Task<IActionResult> Objets() {

            // afficher la config des enchères et pouvoir en creer

            return View();
        }

    }
}