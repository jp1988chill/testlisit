﻿using Prueba.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Prueba.Repository
{
    public class PruebaContext : DbContext
    {
        public PruebaContext(DbContextOptions<PruebaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //EF Core <-> Database mapper for entity: List<int> Idregion;
            modelBuilder.Entity<Pais>()
            .Property(p => p.Idregion)
            .HasConversion(v => JsonConvert.SerializeObject(v),
                     v => JsonConvert.DeserializeObject<List<int>>(v));
        }
		
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        // Entities mapped to EF Core will then be used along CQRS methods (RepositoryEntityFrameworkCQRS.cs)
        public DbSet<User> Users { get; set; }
        public DbSet<Pais> Paises { get; set; }
    }
}
