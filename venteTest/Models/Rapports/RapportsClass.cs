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
using venteTest.Models.AdminViewModels;
using AutoMapper;
using venteTest.Models.MemberViewModels;
using venteTest.Resources.Models;

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

        public void rapport3(string emailAdmin) {

        }

        public void rapport4(string emailAdmin, SendCotesReportViewModel sendCotesReportViewModel) {
            
            // Liste des membres avec leurs cotes selon la requête du client (Annee/mois Debut Et Anne/mois Fin)
            String q1 = "SELECT * FROM Objets WHERE Status={0}"; // AND DateLimite >= {1} AND DateLimite < {2}  
            var objets = from o in _context.Objets.
                        Include(o => o.Categorie).
                        Include(o => o.Vendeur).
                        Include(o => o.Acheteur).
                        Include(o => o.VenteEvaluation).
                        Include(o => o.AchatEvaluation).
                            FromSql(q1, 1)
                         select o;

            // Trouver les objets qui ont des évaluations
            objets = objets.Where(p => p.DateLimite.Year >= sendCotesReportViewModel.SelectedYearStart &&
                                       p.DateLimite.Month >= sendCotesReportViewModel.SelectedMonthStart &&
                                       p.DateLimite.Year <= sendCotesReportViewModel.SelectedYearEnd &&
                                       p.DateLimite.Month <= sendCotesReportViewModel.SelectedMonthEnd);
            List<Objet> lstObjets = objets.ToList();

            // Création du PDF
            string path = CreerPdf(lstObjets, "../Rapports/Rapport4", "4-COTES-" + sendCotesReportViewModel.SelectedYearStart + sendCotesReportViewModel.SelectedMonthStart + "-" + sendCotesReportViewModel.SelectedYearEnd + sendCotesReportViewModel.SelectedMonthEnd);
            // Titre du courriel
            string sujet = "VentesEnchères - " +@StringsRapports.RapportCotesDescription + " " +
                                sendCotesReportViewModel.SelectedMonthStart + "/" + sendCotesReportViewModel.SelectedYearStart + " - " +
                                sendCotesReportViewModel.SelectedMonthEnd + "/" + sendCotesReportViewModel.SelectedYearEnd +
                                ".";
            // Contenu du courriel
            string content = @StringsRapports.RapportAnnuelDescription + " " +
                                sendCotesReportViewModel.SelectedMonthStart +"/"+ sendCotesReportViewModel.SelectedYearStart + " - " +
                                sendCotesReportViewModel.SelectedMonthEnd + "/" + sendCotesReportViewModel.SelectedYearEnd +
                                ".";

            sendRapport(emailAdmin, path, sujet, content);
            /////////////////////////////////
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
            string sujet = "VentesEnchères -  " +@StringsRapports.RapportVentesDescription + " " + annee;
            // Contenu du courriel
            string content = @StringsRapports.RapportVentesDescription +" " + annee + ".";

            sendRapport(emailAdmin, path, sujet, content);
            /////////////////////////////////
        }

        private string CreerPdf(List<Objet>liste, string vueUtilisee, string typeRapport) {

            //string webRootPath = "wwww/Attachments/Rapports/"; //enleve SB
            string webRootPath = he.WebRootPath + "/Attachments/Rapports/";
            webRootPath = webRootPath.Trim();
            string pthCombine = @StringsRapports.RapportNo + typeRapport + "---" + numero + "---" + DateTime.Now.ToString("yyyy-MM-dd hh-mm") + ".pdf";
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

        //// Méthode surchargé pour rapport no.4 (Cote de popularité des membres incluant le niveau de la cote, le nombre d’évaluations.) 
        //private string CreerPdf(CotesViewModel model, string vueUtilisee, string typeRapport) {

        //    //string webRootPath = "wwww/Attachments/Rapports/"; //enleve SB
        //    string webRootPath = he.WebRootPath + "/Attachments/Rapports/";
        //    webRootPath = webRootPath.Trim();
        //    string pthCombine = "rapport#" + typeRapport + "---" + numero + "---" + DateTime.Now.ToString("yyyy-mm-dd") + ".pdf";
        //    var path = Path.Combine(webRootPath, pthCombine);

        //    var report = new ViewAsPdf(vueUtilisee, model) {
        //        FileName = "",
        //        PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
        //        SaveOnServerPath = path,
        //        CustomSwitches =
        //    "--footer-center \"  Date: " +
        //        DateTime.Now.ToString("MMM ddd d HH:mm yyyy") + "      Page: [page]/[toPage]\"" +
        //    " --footer-line --footer-font-size \"12\" --footer-spacing 1 --footer-font-name \"Segoe UI\""
        //    };
        //    var binary = report.BuildFile(this.monContext);
        //    numero++;
        //    return path.ToString();
        //}

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
