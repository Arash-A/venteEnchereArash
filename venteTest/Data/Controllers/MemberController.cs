using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using venteTest.Data;
using venteTest.Models;
using venteTest.Models.MemberViewModels;

namespace venteTest.Controllers
{
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MemberController(ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager) {

            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        /// Montrer le profil public du membre, soit sa cote de vendeur et les commentaires reçus à son sujet, le nombre d'évaluations
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Route("Member/Index/{email}")]
        public async Task<IActionResult> Index(string email) 
        {
            if (email == null) {
                TempData["message"] = $"Email adress is invalid.";
                return RedirectToAction("Home", "Index");
            }
            var _user = await _userManager.FindByEmailAsync(email);
            if (_user == null) {
                throw new ApplicationException($"Unable to load user with email '{email}'.");
            }

            IList<ApplicationUser> lstMembers = await _userManager.GetUsersInRoleAsync("Member");
            IList<ApplicationUser> lstAdmin = await _userManager.GetUsersInRoleAsync("Admin");
            lstMembers.Add(lstAdmin.FirstOrDefault()); //ajouter l'admin à liste de membres
            var member = lstMembers.Where(p => p.Email.Equals(email)).FirstOrDefault();

            string query = "SELECT * FROM Objets WHERE Status = {0} AND VendeurId = {1} ";
            var objetsDuMembre = from o in _context.Objets.
                                         Include(o => o.Acheteur).
                                         Include(o => o.VenteEvaluation).
                                            ThenInclude(o => o.Acheteur).
                                         Include(o => o.AchatEvaluation).
                                            ThenInclude(o => o.Vendeur).
                                         FromSql(query, 1, member.Id)
                                 select o;

            ICollection<Objet> objetsDuMembreListe = objetsDuMembre.ToList();

            double ratingMoyenVente = 0d; int cmpt = 0;
            foreach (var objItem in objetsDuMembre) {

                if (objItem.AchatEvaluation != null) {
                    ratingMoyenVente += Convert.ToDouble(objItem.AchatEvaluation.Cote);
                    cmpt++;
                }

            }
            //ratingMoyenVente = (cmpt != 0) ? ratingMoyenVente / Convert.ToDouble(cmpt) : 0;

            var indexViewModel = new IndexViewModel() {
                Email = member.Email,
                Nom = member.Nom,
                Prenom = member.Prenom,
                Civilite = member.Civilite,
                Cote = ratingMoyenVente,
                NbEvaluation = cmpt,
                Objets = objetsDuMembreListe
            };
            

            return View(indexViewModel);

        }

    }
}