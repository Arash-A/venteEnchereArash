using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Data;
using System.Net.Mail;
using Hangfire;
using System.Net;
using Rotativa.AspNetCore;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using venteTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;

namespace venteTest.Models.Rapports
{
    public class RapportsClass
    {
        private readonly IHostingEnvironment he;
        private readonly ApplicationDbContext _context;
        private readonly ActionContext monContext;
        private static int numero = 0;

        public RapportsClass(ApplicationDbContext db, ControllerContext controllerContext, IHostingEnvironment e) {
            _context = db;
            monContext = controllerContext;
            he = e;
        }

        public void rapport1() {
            //String q1 = "SELECT * FROM Objets WHERE Status={0}";
            //var objets = from o in _context.Objets.
            //            Include(o => o.Categorie).
            //            Include(o => o.Vendeur).
            //            Include(o => o.Acheteur).
            //            Include(o => o.Fichiers).
            //            Include(o => o.Encheres).
            //                ThenInclude(m => m.Miseur).
            //                FromSql(q1, 0)
            //             select o;
            //List<Objet> visListe = objets.ToList();
            //string path=creerPdf(visListe, "");
            //sendRapport(membre,path,sujet,content);
        }

        public void rapport2() {

        }

        public void rapport3() {

        }

        public void rapport4() {

        }

        public void rapport5(int annee, string emailAdmin) {
            // Sélection des objets vendus
            String q1 = "SELECT * FROM Objets WHERE Status={0}"; // AND DateLimite >= {1} AND DateLimite < {2}  
            var objets = from o in _context.Objets.
                        Include(o => o.Categorie).
                        Include(o => o.Vendeur).
                        Include(o => o.Acheteur).
                            FromSql(q1, 1, annee, annee + 1)
                         select o;

            objets = objets.Where(p => p.DateLimite.Year >= annee && p.DateLimite.Year < (annee + 1));

            List <Objet> lstObjets = objets.ToList();

            // Création du PDF
            string path = CreerPdf(lstObjets, "../Rapports/Rapport5", "5-VENTES-ANNUEL");
            // Titre du courriel
            string sujet = "VentesEnchères - Synthèse des ventes réalisées et des commissions année " + annee;
            // Contenu du courriel
            string content = "Voici votre rapport annuel des ventes réalisées et des commissions perçues pour l'annéee " + annee + ".";

            sendRapport(emailAdmin, path, sujet, content);
            /////////////////////////////////
        }

        private string CreerPdf(List<Objet>liste, string vueUtilisee, string typeRapport) {

            //string webRootPath = "wwww/Attachments/Rapports/"; //enleve SB
            string webRootPath = he.WebRootPath + "/Attachments/Rapports/";
            webRootPath = webRootPath.Trim();
            string pthCombine = "rapport#" + typeRapport + "---" + numero + "---" + DateTime.Now.ToString("yyyy-MM-dd hh-mm") + ".pdf";
            var path = Path.Combine(webRootPath, pthCombine);

            var report = new ViewAsPdf(vueUtilisee, liste) {
                FileName = "",
                PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
                SaveOnServerPath = path,
                CustomSwitches =
            "--footer-center \"  Date: " +
                DateTime.Now.ToString("MMM ddd d HH:mm yyyy") + "      Page: [page]/[toPage]\"" +
            " --footer-line --footer-font-size \"12\" --footer-spacing 1 --footer-font-name \"Segoe UI\""
            };
            var binary = report.BuildFile(this.monContext); 
            numero++;
            return path.ToString();
        }

        private async void sendRapport(string recipient, string fichier, string sujet,string content) {
                try {
                    await Task.Run(() => {
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("VenteEnchereM9@gmail.com");
                        mail.To.Add(recipient);
                        mail.Subject = sujet;
                        mail.Body = content;
                        Attachment attachment = new Attachment(fichier);
                        mail.Attachments.Add(attachment);

                        SmtpClient smtpClient = new SmtpClient();
                        var credential = new NetworkCredential {
                            UserName = "VenteEnchereM9@gmail.com",  // replace with valid value
                            Password = "!qwerty123"  // replace with valid value
                        };
                        smtpClient.Credentials = credential;
                        smtpClient.Host = "smtp.gmail.com";
                        smtpClient.Port = 587;

                        smtpClient.EnableSsl = true;
                        smtpClient.Send(mail);
                    });
                } catch (Exception ex) {
                    throw ex;
                } 
            
        }
    }
}
