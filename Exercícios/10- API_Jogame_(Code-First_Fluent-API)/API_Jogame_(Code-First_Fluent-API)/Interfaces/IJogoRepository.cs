using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Jogame__Code_First_Fluent_API_.Domains;

namespace API_Jogame__Code_First_Fluent_API_.Interfaces
{
    interface IJogoRepository
    {
        /// <summary>
        ///     Mostra TODOS os jogas cadastrados no banco de dados.
        /// </summary>
        /// <returns>TODOS os objetos do tipo Jogo cadastrados no banco de dados.</returns>
        ICollection<Jogo> Ler();

        /// <summary>
        ///     Mostra o jogo que tem o ID especificado.
        /// </summary>
        /// <param name="id">ID do jogo procurado.</param>
        /// <returns>Objeto do tipo Jogo cadastrado no banco de dados que tem o ID especificado no parâmetro.</returns>
        Jogo Buscar(Guid id);

        /// <summary>
        ///     Sobrecarga do método Buscar que mostra TODOS os jogos cadastrados no banco de dados que contenham o nome especificado.
        /// </summary>
        /// <param name="nome">NOME do jogo procurado.</param>
        /// <returns>Objetos do tipo Jogo cadastrados no banco de dados que contenham o nome especificado por parâmetro.</returns>
        ICollection<Jogo> Buscar(string nome);

        /// <summary>
        ///     Cadastra no banco de dados o jogo passado por parâmetro.
        /// </summary>
        /// <param name="jogo">Objeto do tipo Jogo a ser cadastrado.</param>
        /// <returns>Objeto do tipo Jogo cadastrado.</returns>
        Jogo Cadastrar(Jogo jogo);

        /// <summary>
        ///     Altera o jogo passado por parâmetro. Ao invés de especificar um id, coloque o id no objeto que for passado.
        /// </summary>
        /// <param name="jogo">Objeto do tipo Jogo, já alterado.</param>
        /// <returns>Objeto do tipo Jogo alterado.</returns>
        Jogo Alterar(Jogo jogo);

        /// <summary>
        ///     Exclui o jogo que tem o id especificado por parâmetro.
        /// </summary>
        /// <param name="id">Id do jogo a ser excluído.</param>
        /// <returns>Objeto do tipo Jogo excluído.</returns>
        Jogo Excluir(Guid id);
    }
}