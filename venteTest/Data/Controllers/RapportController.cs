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
using venteTest.Models.ObjetViewModel;
using venteTest.Services;
using AutoMapper;
using Rotativa.AspNetCore;
using System.Collections;
using System.Net.Mail;
using Hangfire;
using System.Net;
using System.Text;
using venteTest.Models.Rapports;
using venteTest.Models;
using venteTest.Models.AdminViewModels;

namespace venteTest.Data.Controllers
{
    public class RapportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment he;
        private readonly IServiceProvider _serviceProvider;
        private static int numero = 0;
        public RapportController(ApplicationDbContext context, IHostingEnvironment e, IServiceProvider serviceProvider) {
            _context = context;
            he = e;
            _serviceProvider = serviceProvider;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> rapSettings(RapViewModel rap) {
            RapportsClass rapportsClass = new RapportsClass(_context, this.ControllerContext, he);
            int an = DateTime.Now.Year;
            DateTime s = DateTime.Now.AddMinutes(1);
            string emilAdmin = "amiri2281@gmail.com";
            if (rap.rap1 == true) {
                RecurringJob.AddOrUpdate("rap1Job", () => this.rapport1(), "0 0 1 1-12 *");
            } else {

            }

            if (rap.rap2 == true) {
                RecurringJob.AddOrUpdate("rap2Job", () => this.rapport2(), "0 0 1 1-12 *");
            } else {

            }

            if (rap.rap3 == true) {
                RecurringJob.AddOrUpdate("rap3Job", () => this.rapport3(), "0 0 1 1-12 *");
            } else {

            }

            if (rap.rap4 == true) {
                RecurringJob.AddOrUpdate("rap4Job", () => this.rapport4(), "0 0 1 1-12 *");
            } else {

            }

            if (rap.rap5 == true) {
                RecurringJob.AddOrUpdate("rap5Job", () => this.rapport5(), "59 23 31 12 *");
            } else {
                RecurringJob.RemoveIfExists("rap5Job");
            }
            TempData["message"] = "votre configuration est sauvegardée";
            return RedirectToAction("Index", "Admin");

        }
      /// <summary>
      /// // Méthode vides utiliser pour les messages automatique
      /// </summary>
        public void rapport1() {
            RapportsClass rapportsClass = new RapportsClass(_context, this.ControllerContext, he);
            rapportsClass.rapport1();
        }

        public void rapport2() {
            RapportsClass rapportsClass = new RapportsClass(_context, this.ControllerContext, he);
            rapportsClass.rapport2();
        }

        public void rapport3() {
            RapportsClass rapportsClass = new RapportsClass(_context, this.ControllerContext, he);
           // rapportsClass.rapport3();
        }

        public void rapport4() {
            RapportsClass rapportsClass = new RapportsClass(_context, this.ControllerContext, he);
           // rapportsClass.rapport4("sasha.bouchard@gmail.com", SendCotesReportViewModel sendCotesReportViewModel);
        }

        public void rapport5() {
            RapportsClass rapportsClass = new RapportsClass(_context, this.ControllerContext, he);
            rapportsClass.rapport5(2018, "sasha.bouchard@gmail.com");
        }

        /// <summary>
        /// Methodes de l'action pour les boutons
        /// </summary>
        public async Task<IActionResult> rap1Mt() {
            RapportsClass rapportsClass = new RapportsClass(_context, this.ControllerContext, he);
            // envoyer rapport
            rapportsClass.rapport1();
            TempData["message"] = "Rapport est envoyé";
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> rap2Mt() {
            RapportsClass rapportsClass = new RapportsClass(_context, this.ControllerContext, he);
            // envoyer rapport
            rapportsClass.rapport2();
            TempData["message"] = "Rapport est envoyé";
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> rap3Mt() {
            RapportsClass rapportsClass = new RapportsClass(_context, this.ControllerContext, he);
            // envoyer rapport
            //rapportsClass.rapport3();
            TempData["message"] = "Rapport est envoyé";
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> rap4Mt() {
            RapportsClass rapportsClass = new RapportsClass(_context, this.ControllerContext, he);
            // envoyer rapport
           // rapportsClass.rapport4( "sasha.bouchard@gmail.com",);
            TempData["message"] = "Rapport est envoyé";
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> rap5Mt() {
            RapportsClass rapportsClass = new RapportsClass(_context, this.ControllerContext, he);
            // envoyer rapport
            rapportsClass.rapport5(DateTime.Now.Year,"sasha.bouchard@gmail.com");
            TempData["message"] = "Rapport est envoyé";
            return RedirectToAction("Index", "Admin");
        }

    }
}