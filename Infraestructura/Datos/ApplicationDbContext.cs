using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Pais> Pais { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Lugar> Lugar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //ENCARGADO DE CREAR LAS MIGRACIONES
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}