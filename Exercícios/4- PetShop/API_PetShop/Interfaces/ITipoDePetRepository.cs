using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_PetShop.Domains;

namespace API_PetShop.Interfaces
{
    interface ITipoDePetRepository
    {
        TipoDePet Cadastrar(TipoDePet tipoDePet);
        List<TipoDePet> Ler();
        TipoDePet BuscarPorId(int id);
        TipoDePet BuscarPorDescricao(string descricao);
        TipoDePet Alterar(int id, TipoDePet tipoDePetAlterado);
        TipoDePet Excluir(int id);
    }
}