using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Datos
{
    public class LugarRepositorio : ILugarRepositorio
    {
        private readonly ApplicationDbContext _db;
        public LugarRepositorio(ApplicationDbContext db)
        {
            _db = db;
            
        }
        public async Task<Lugar> GetLugarAsync(int id)
        {
            return await _db.Lugar                        
                        .Include(p => p.Pais)
                        .Include(c => c.Categoria)
                        .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IReadOnlyList<Lugar>> GetLugaresAsync()
        {
         /*   var paisId = 1;
            var lugares = _db.Lugar.Where(p=>p.PaisId == paisId)
                        .Include(p => p.Pais)
                        .Include(c => c.Categoria)
                        .ToListAsync(); */

            return await _db.Lugar
                        .Include(p => p.Pais)
                        .Include(c => c.Categoria)
                        .ToListAsync();
        }
    }
}