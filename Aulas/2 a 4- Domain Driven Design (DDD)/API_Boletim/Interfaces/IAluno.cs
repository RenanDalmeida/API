using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Boletim.Domains;

namespace API_Boletim.Interfaces
{
    /*Interface do repositorie.*/
    interface IAluno
    {
        Aluno Cadastrar(Aluno aluno);
        List<Aluno> Ler();
        Aluno BuscarPorId(int id);
        Aluno Alterar(int id, Aluno aluno);
        void Excluir(int id);
    }
}
