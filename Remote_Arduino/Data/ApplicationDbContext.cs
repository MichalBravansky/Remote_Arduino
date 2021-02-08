using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Remote_Arduino.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Code> Code { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Code>(entity =>
            {
                entity.HasKey(e => e.Code_ID);
                entity.Property(e => e.Code_ID).HasColumnName("Code_ID");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.UploadDate).HasColumnType("datetime");

                entity.Property(e => e.User_ID).HasColumnName("User_ID");
            });
        }
    }
}
