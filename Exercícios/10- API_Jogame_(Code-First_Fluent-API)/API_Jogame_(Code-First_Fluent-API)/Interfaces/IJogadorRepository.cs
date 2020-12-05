using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Jogame__Code_First_Fluent_API_.Domains;

namespace API_Jogame__Code_First_Fluent_API_.Interfaces
{
    interface IJogadorRepository
    {
        /// <summary>
        ///     Mostra TODOS os jogadores cadastrados no banco de dados.
        /// </summary>
        /// <returns>TODOS os objetos do tipo Jogador cadastrados no banco de dados.</returns>
        ICollection<Jogador> Ler();

        /// <summary>
        ///     Mostra o jogador que tem o ID especificado.
        /// </summary>
        /// <param name="id">ID do jogador procurado.</param>
        /// <returns>Objeto do tipo Jogador cadastrado no banco de dados que tem o ID especificado no parâmetro.</returns>
        Jogador Buscar(Guid id);

        /// <summary>
        ///     Sobrecarga do método Buscar que mostra TODOS os jogadores cadastrados no banco de dados que contenham o nome especificado.
        /// </summary>
        /// <param name="nome">NOME do jogador procurado.</param>
        /// <returns>Objetos do tipo Jogador cadastrados no banco de dados que contenham o nome especificado por parâmetro.</returns>
        ICollection<Jogador> Buscar(string nome);

        /// <summary>
        ///     Cadastra no banco de dados o jogador passado por parâmetro.
        /// </summary>
        /// <param name="jogador">Objeto do tipo Jogador a ser cadastrado.</param>
        /// <returns>Objeto do tipo Jogador cadastrado.</returns>
        Jogador Cadastrar(Jogador jogador);

        /// <summary>
        ///     Altera o jogador passado por parâmetro. Ao invés de especificar um id, coloque o id no objeto que for passado.
        /// </summary>
        /// <param name="jogador">Objeto do tipo Jogador, já alterado.</param>
        /// <returns>Objeto do tipo Jogador alterado.</returns>
        Jogador Alterar(Jogador jogador);

        /// <summary>
        ///     Exclui o jogador que tem o id especificado por parâmetro.
        /// </summary>
        /// <param name="id">Id do jogador a ser excluído.</param>
        /// <returns>Objeto do tipo Jogador excluído.</returns>
        Jogador Excluir(Guid id);
    }
}