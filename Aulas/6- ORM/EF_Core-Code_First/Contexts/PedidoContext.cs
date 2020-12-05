using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF_Core_Code_First.Domains;
using Microsoft.EntityFrameworkCore;

namespace EF_Core_Code_First.Contexts
{
    public class PedidoContext : DbContext
    {
        //Aqui agora indicamos por meio de DbSets os domínios que farão parte desse contexto. Por exemplo, dizemos que o domínio ou a clase Pedido será a tabela Pedidos no banco de dados.
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<PedidoProduto> PedidosProdutos { get; set; }

        /// <summary>
        ///     Sobrescrita do método OnConfiguring para configurarmos o nosso contexto com o caminho do banco de dados a ser usado.
        /// </summary>
        /// <param name="optionBuilder">Construtor de opções do contexto. Nossa conexão com o banco.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            //Se não estiver configurado...
            if (!optionBuilder.IsConfigured)
            {
                //Caminho do banco a ser criado e informações usando o SQL SERVER.
                optionBuilder.UseSqlServer(@"Data Source=desktop-h1nfq8t\SQLEXPRESS;Initial Catalog=loja;User ID=sa;Password=sa132");

                //Usa o método original e passa como parâmetro nosso construtor do contexto, que já tem o caminho do banco e todas informações que ele necessita para ser criado.
                base.OnConfiguring(optionBuilder);
            }
        }
    }
}