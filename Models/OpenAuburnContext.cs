using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace open_auburn_api.Models
{
    public partial class OpenAuburnContext : DbContext
    {
        public OpenAuburnContext()
        {
        }

        public OpenAuburnContext(DbContextOptions<OpenAuburnContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Crime> Crimes { get; set; } = null!;
        public virtual DbSet<Fire> Fires { get; set; } = null!;
        public virtual DbSet<College> Colleges { get; set; } = null!;
        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crime>(entity =>
            {
                entity.Property(e => e.Oaid).HasComputedColumnSql("(right('OACRI'+CONVERT([varchar](50),[ID]),(50)))", true);
            });

            modelBuilder.Entity<Fire>(entity =>
            {
                entity.Property(e => e.Oaid).HasComputedColumnSql("(right('OAFIR'+CONVERT([varchar](50),[ID]),(50)))", true);
            });

            modelBuilder.Entity<College>(entity =>
            {
                entity.Property(e => e.Oaid).HasComputedColumnSql("(right('OACOL'+CONVERT([varchar](50),[ID]),(50)))", true);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.Property(e => e.Oaid).HasComputedColumnSql("(right('OAENR'+CONVERT([varchar](50),[ID]),(50)))", true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
