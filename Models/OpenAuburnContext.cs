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
        public virtual DbSet<Dataset> Datasets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:sqlserver-openauburn-e1-prod-001.database.windows.net,1433;Initial Catalog=db-openauburn-e1-prod-001;Persist Security Info=False;User ID=oa_reader;Password=w@r3@g!3d@t@s3t5;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
