using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EF_Core_Database_First.Domains;

namespace EF_Core_Database_First.Contexts
{
    public partial class LojaContext : DbContext
    {
        public LojaContext()
        {
        }

        public LojaContext(DbContextOptions<LojaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pedidos> Pedidos { get; set; }
        public virtual DbSet<PedidosProdutos> PedidosProdutos { get; set; }
        public virtual DbSet<Produtos> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\SqlExpress;Initial Catalog=loja;User ID=sa;Password=sa132");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedidos>(entity =>
            {
                entity.HasKey(e => e.IdPedido);

                entity.Property(e => e.IdPedido).ValueGeneratedNever();
            });

            modelBuilder.Entity<PedidosProdutos>(entity =>
            {
                entity.HasKey(e => e.IdPedidoProduto);

                entity.HasIndex(e => e.IdPedido);

                entity.HasIndex(e => e.IdProduto);

                entity.Property(e => e.IdPedidoProduto).ValueGeneratedNever();

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.PedidosProdutos)
                    .HasForeignKey(d => d.IdPedido);

                entity.HasOne(d => d.IdProdutoNavigation)
                    .WithMany(p => p.PedidosProdutos)
                    .HasForeignKey(d => d.IdProduto);
            });

            modelBuilder.Entity<Produtos>(entity =>
            {
                entity.HasKey(e => e.IdProduto);

                entity.Property(e => e.IdProduto).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
