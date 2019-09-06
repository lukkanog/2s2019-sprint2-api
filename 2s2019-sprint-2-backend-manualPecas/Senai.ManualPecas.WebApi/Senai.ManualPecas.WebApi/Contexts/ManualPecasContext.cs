using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Senai.ManualPecas.WebApi.Domains
{
    public partial class ManualPecasContext : DbContext
    {
        public ManualPecasContext()
        {
        }

        public ManualPecasContext(DbContextOptions<ManualPecasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Fornecedores> Fornecedores { get; set; }
        public virtual DbSet<Pecas> Pecas { get; set; }
        
        public virtual DbSet<Vendas> Vendas { get; set; }

        // Unable to generate entity type for table 'dbo.Vendas'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\SqlExpress; Initial Catalog=M_ManualPecas;User Id=sa;Pwd=132");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendas>().HasKey(p => new { p.IdFornecedor, p.IdPeca });

            modelBuilder.Entity<Vendas>()
            .HasOne<Fornecedores>(x => x.Fornecedor)
            .WithMany(y => y.Vendas)
            .HasForeignKey(z => z.IdFornecedor);

            modelBuilder.Entity<Vendas>()
            .HasOne<Pecas>(x => x.Peca)
            .WithMany(y => y.Vendas)
            .HasForeignKey(z => z.IdPeca);


            modelBuilder.Entity<Vendas>()
                .Property(e => e.Preco)
                .IsRequired()
                .IsUnicode(false);


            modelBuilder.Entity<Fornecedores>(entity =>
            {
                entity.HasKey(e => e.IdFornecedor);

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Forneced__A9D10534E2A19F2F")
                    .IsUnique();

                entity.HasIndex(e => e.Nome)
                    .HasName("UQ__Forneced__7D8FE3B2300BE049")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pecas>(entity =>
            {
                entity.HasKey(e => e.IdPeca);

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
