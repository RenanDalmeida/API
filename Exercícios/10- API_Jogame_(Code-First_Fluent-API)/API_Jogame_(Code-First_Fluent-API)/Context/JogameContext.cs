using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Jogame__Code_First_Fluent_API_.Domains;
using Microsoft.EntityFrameworkCore;

namespace API_Jogame__Code_First_Fluent_API_.Context
{
    public class JogameContext : DbContext
    {
        //Transformando os domínios em tabelas para o banco de dados.
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<JogosJogadores> JogosJogadores { get; set; }

        /// <summary>
        ///     Configura o banco de dados através de sobrescrita e recursividade.
        /// </summary>
        /// <param name="optionBuilder">Contexto que contém as configurações do banco de dados.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if(!optionBuilder.IsConfigured)
            {
                optionBuilder.UseSqlServer(@"Data Source=desktop-h1nfq8t\SQLEXPRESS;Initial Catalog=jogame;Persist Security Info=True;User ID=sa;Password=sa132");
                base.OnConfiguring(optionBuilder);
            }
        }

        /// <summary>
        ///     Utiliza-se de FluentAPI para fazer o mapeamento de entidades e define constraints dos atributos para gerar o banco de dados automaticamente através do ORM Entity Framework Core.
        /// </summary>
        /// <param name="modelBuilder">O construtor do banco de dados, o mapeador de entidades.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //OBS: O método HasKey() é usado para definir a chave primária de uma entidade. Automaticamente, o EF Core vai gerar as chaves primárias de suas entidades, quando encontrar algum atribudo denominado (Id) ou (NomeId). Ainda assim, o método HasKey() é útil para gerar chaves primárias compostas (o que é impossível fazer com DataAnnotations e não tem geração automática).

            //Na entidade JogosJogadores..
            modelBuilder.Entity<JogosJogadores>()
                .HasOne<Jogo>(jj => jj.Jogo) //Tem um jogo, e indica o atributo, na tabela JogosJogadores, que guardará esse Jogo.
                .WithMany(j => j.JogosJogadores) //E, na tabela Jogo, tem uma coleção de JogosJogadores.
                .HasForeignKey(jj => jj.IdJogo); //Tem a chave estrangeira desse Jogo, e indica o atributo, na tabela JogosJogadores, que guardará essa chave.

            //A mesma coisa para a tabela Jogador..
            modelBuilder.Entity<JogosJogadores>()
                .HasOne<Jogador>(jj => jj.Jogador) 
                .WithMany(j => j.JogosJogadores) 
                .HasForeignKey(jj => jj.IdJogador);

            //Na entidade Jogador, o atributo Nome é NotNull.
            modelBuilder.Entity<Jogador>()
                .Property(j => j.Nome)
                .IsRequired();
        }
    }
}