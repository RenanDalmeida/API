using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF_Core_Code_First.Domains;

namespace EF_Core_Code_First.Interfaces
{
    interface IProdutoRepository
    {
        void Adicionar(Produto produto);
        List<Produto> Ler();
        Produto BuscarPorId(Guid id);
        List<Produto> BuscarPorNome(string nome);
        void Alterar(Produto produto);
        void Excluir(Guid id);
    }
}