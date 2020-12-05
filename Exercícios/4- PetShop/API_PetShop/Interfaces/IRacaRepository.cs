using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_PetShop.Domains;

namespace API_PetShop.Interfaces
{
    interface IRacaRepository
    {
        Raca Cadastrar(Raca raca);
        List<Raca> Ler();
        Raca BuscarPorId(int id);
        Raca Alterar(int id, Raca racaAlterada);
        Raca Excluir(int id);
    }
}