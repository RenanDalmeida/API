using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using API_PetShop.Context;
using API_PetShop.Domains;
using API_PetShop.Interfaces;

namespace API_PetShop.Repositories
{
    public class RacaRepository : IRacaRepository
    {
        PetShopContext conexao = new PetShopContext();

        SqlCommand comando = new SqlCommand();

        /// <summary>
        ///     Cadastra no banco de dados a raça passada no parâmetro.
        /// </summary>
        /// <param name="raca">Raça a ser cadastrada no banco de dados.</param>
        /// <returns>Retorna a raça que foi cadastrada no banco de dados.</returns>
        public Raca Cadastrar(Raca raca)
        {
            comando.Connection = conexao.Conectar();

            comando.CommandText = "INSERT INTO Raca (Descricao, IdTipoDePet) " +
                                  "VALUES " +
                                  "(@descricao, @idTipoDePet)";
            comando.Parameters.AddWithValue("@descricao", raca.Descricao);
            comando.Parameters.AddWithValue("@idTipoDePet", raca.IdTipoDePet);
            comando.ExecuteNonQuery();

            conexao.Desconectar();

            return raca;
        }

        /// <summary>
        ///     Faz a leitura de todas as raças cadastradas no banco de dados e retorna.
        /// </summary>
        /// <returns>Retorna todos as raças cadastradas no banco de dados.</returns>
        public List<Raca> Ler()
        {
            comando.Connection = conexao.Conectar();

            comando.CommandText = "SELECT * FROM Raca";

            SqlDataReader dados = comando.ExecuteReader();

            List<Raca> racas = new List<Raca>();

            while(dados.Read())
            {
                racas.Add(
                    new Raca()
                    {
                        IdRaca = Convert.ToInt32(dados.GetValue(0)),
                        Descricao = dados.GetValue(1).ToString(),
                        IdTipoDePet = Convert.ToInt32(dados.GetValue(2))
                    }
                );
            }

            conexao.Desconectar();

            return racas;
        }

        /// <summary>
        ///     Faz a leitura da raça especificada por id cadastrada no banco de dados e retorna.
        /// </summary>
        /// <param name="id">Id para filtrar a raça.</param>
        /// <returns>Retorna a raça que tem o id especificado.</returns>
        public Raca BuscarPorId(int id)
        {
            comando.Connection = conexao.Conectar();

            comando.CommandText = "SELECT * FROM Raca " + 
                                  "WHERE IdRaca = @id";
            comando.Parameters.AddWithValue("@id", id);

            SqlDataReader dados = comando.ExecuteReader();

            comando.Parameters.Clear();

            Raca raca = new Raca();
            while (dados.Read())
            {
                raca.IdRaca = Convert.ToInt32(dados.GetValue(0));
                raca.Descricao = dados.GetValue(1).ToString();
                raca.IdTipoDePet = Convert.ToInt32(dados.GetValue(2));
            }
            
            conexao.Desconectar();

            return raca;
        }

        /// <summary>
        ///     Altera uma raça cadastrada no banco de dados.
        /// </summary>
        /// <param name="id">Id da raça a ser alterada.</param>
        /// <param name="racaAlterada">Raça alterada, que irá substituir a raça a ser alterada.</param>
        /// <returns>Retorna a raça, já alterada e cadastrada no banco.</returns>
        public Raca Alterar(int id, Raca racaAlterada)
        {
            comando.Connection = conexao.Conectar();

            comando.CommandText = "UPDATE Raca SET " +
                "Descricao = @descricao, IdTipoDePet = @idTipoDePet " + 
                "WHERE IdRaca = @id";
            comando.Parameters.AddWithValue("@descricao", racaAlterada.Descricao);
            comando.Parameters.AddWithValue("@idTipoDePet", racaAlterada.IdTipoDePet);
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();

            return racaAlterada;
        }

        /// <summary>
        ///     Exclui a raça dos banco de dados que contém o id especificado.
        /// </summary>
        /// <param name="id">Id da raça a ser deletada.</param>
        /// <returns>Retorna a raça deletada para uma melhor compreensão.</returns>
        public Raca Excluir(int id)
        {
            //Instância da raça que irá ser apagada, para ser retornada e mostrar a raça que foi apagada.
            Raca raca = new Raca();
            raca.IdRaca = BuscarPorId(id).IdRaca;
            raca.Descricao = BuscarPorId(id).Descricao;
            raca.IdTipoDePet = BuscarPorId(id).IdTipoDePet;

            comando.Connection = conexao.Conectar();

            comando.CommandText = "DELETE FROM Raca " +
                                  "WHERE IdRaca = @idRaca";
            comando.Parameters.AddWithValue("@idRaca", id);

            comando.ExecuteNonQuery();

            conexao.Desconectar();

            return raca;
        }
    }
}