using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Jogame__Code_First_Fluent_API_.Domains;
using API_Jogame__Code_First_Fluent_API_.Interfaces;

namespace API_Jogame__Code_First_Fluent_API_.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        public ICollection<Jogo> Ler()
        {
            throw new NotImplementedException();
        }

        public Jogo Buscar(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Jogo> Buscar(string nome)
        {
            throw new NotImplementedException();
        }

        public Jogo Cadastrar(Jogo jogo)
        {
            throw new NotImplementedException();
        }

        public Jogo Alterar(Jogo jogo)
        {
            throw new NotImplementedException();
        }

        public Jogo Excluir(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}