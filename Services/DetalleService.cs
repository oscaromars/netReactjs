// Services/DetalleService.cs
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Formaapp.Models; // Ajusta según tu espacio de nombres
using Formaapp.Data;   // Ajusta según tu espacio de nombres

namespace Formaapp.Services // Ajusta según la estructura de tu proyecto
{
    public class DetalleService
    {
        private readonly MiDbContext _context;

        public DetalleService(MiDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Detalle> GetAll()
        {
            return _context.Detalles.Include(d => d.Cabecera).ToList();
        }

        public Detalle? GetById(int id) // Cambiado a nullable
        {
            return _context.Detalles.Include(d => d.Cabecera).FirstOrDefault(d => d.Id == id);
        }

        public void Create(Detalle detalle)
        {
            _context.Detalles.Add(detalle);
            _context.SaveChanges();
        }

        public void Update(Detalle detalle)
        {
            _context.Detalles.Update(detalle);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var detalle = _context.Detalles.Find(id);
            if (detalle != null)
            {
                _context.Detalles.Remove(detalle);
                _context.SaveChanges();
            }
        }
    }
}
