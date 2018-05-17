using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Data;
using venteTest.Models;

namespace venteTest.Services
{
    public class FiniVenteObjet
    {
        private readonly ApplicationDbContext _context;
       

        public FiniVenteObjet(ApplicationDbContext db) {
            _context = db;
          
        }

        public void ChangeStatus(int objetId) {
            var objet = _context.Objets.First(a => a.ObjetID == objetId);
            objet.Status = Status.Vendu;
            _context.SaveChangesAsync();
        }
    }
}
