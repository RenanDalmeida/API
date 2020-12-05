using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Jogame__Code_First_Fluent_API_.Context;
using API_Jogame__Code_First_Fluent_API_.Domains;
using API_Jogame__Code_First_Fluent_API_.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Jogame__Code_First_Fluent_API_.Repositories
{
    public class JogadorRepository : IJogadorRepository
    {
        private readonly JogameContext _ctx;

        /// <summary>
        ///     Construtor que certifica que assim que o repositório for instanciado, ele crie o contexto do banco de dados.
        /// </summary>
        public JogadorRepository()
        {
            _ctx = new JogameContext();
        }

        public ICollection<Jogador> Ler()
        {
            try
            {
                return _ctx.Jogadores.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Jogador Buscar(Guid id)
        {
            try
            {
                return _ctx.Jogadores.FirstOrDefault(j => j.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ICollection<Jogador> Buscar(string nome)
        {
            try
            {
                return _ctx.Jogadores.Where(j => j.Nome.Contains(nome)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Jogador Cadastrar(Jogador jogador)
        {
            try
            {
                _ctx.Jogadores.Add(jogador);
                _ctx.SaveChanges();

                return jogador;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Jogador Alterar(Jogador jogador)
        {
            try
            {
                Jogador jogadorAlterado = Buscar(jogador.Id);

                if (jogadorAlterado == null)
                    throw new Exception("Impossível alterar o jogador pois ele não existe no banco de dados.");

                jogadorAlterado.Nome = jogador.Nome;
                jogadorAlterado.Email = jogador.Email;
                jogadorAlterado.Senha = jogador.Senha;
                jogadorAlterado.DataNascimento = jogador.DataNascimento;

                _ctx.Jogadores.Update(jogadorAlterado);
                _ctx.SaveChanges();

                return jogador;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Jogador Excluir(Guid id)
        {
            try
            {
                Jogador jogadorExcluir = Buscar(id);

                if (jogadorExcluir == null)
                    throw new Exception("Impossível excluir o jogador pois ele não existe no banco de dados.");

                _ctx.Jogadores.Remove(jogadorExcluir);
                _ctx.SaveChanges();

                return jogadorExcluir;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}