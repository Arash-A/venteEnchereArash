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

namespace venteTest.Models.Rapports
{
    public class RapportsClass
    {
        private readonly ApplicationDbContext _context;
        private readonly ActionContext monContext;
        private static int numero = 0;




        public RapportsClass(ApplicationDbContext db) {
            _context = db;
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
            //creerPdf(visListe, "");
            //sendRapport()
        }

        public void rapport2() {

        }

        public void rapport3() {

        }

        public void rapport4() {

        }

        public void rapport5() {

        }



        private string CreerPdf(List<Objet> liste,string vueUtilisee,string typeRapport) {

            string webRootPath = "wwww/Attachments/Rapports/";
            var path = Path.Combine(webRootPath, "rapport#" +typeRapport+"---"+ numero+"---" + DateTime.Now.ToString("yyyy-mm-dd")+ ".pdf");

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
