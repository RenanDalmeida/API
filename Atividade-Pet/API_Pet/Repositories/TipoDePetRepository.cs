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
    public class TipoDePetRepository : ITipoDePetRepository
    {
        PetShopContext conexao = new PetShopContext();

        SqlCommand comando = new SqlCommand();

        /// <summary>
        ///     Cadastra no banco de dados o tipo de pet passado no parâmetro.
        /// </summary>
        /// <param name="tipoDePet">Tipo de pet a ser cadastrado no banco de dados.</param>
        /// <returns>Retorna o tipo de pet que foi cadastrado no banco de dados.</returns>
        public TipoDePet Cadastrar(TipoDePet tipoDePet)
        {
            comando.Connection = conexao.Conectar();

            comando.CommandText = "INSERT INTO TipoDePet (Descricao) " +
                                  "VALUES " +
                                  "(@descricao)";
            comando.Parameters.AddWithValue("@descricao", tipoDePet.Descricao);
            comando.ExecuteNonQuery();

            conexao.Desconectar();

            return tipoDePet;
        }

        /// <summary>
        ///     Faz a leitura de todos os tipos de pet cadastrados no banco de dados e retorna.
        /// </summary>
        /// <returns>Retorna todos os tipos de pet cadastrados no banco de dados.</returns>
        public List<TipoDePet> Ler()
        {
            comando.Connection = conexao.Conectar();

            comando.CommandText = "SELECT * FROM TipoDePet";

            SqlDataReader dados = comando.ExecuteReader();

            List<TipoDePet> tiposDePet = new List<TipoDePet>();

            while (dados.Read())
            {
                tiposDePet.Add(
                    new TipoDePet()
                    {
                        IdTipoDePet = Convert.ToInt32(dados.GetValue(0)),
                        Descricao = dados.GetValue(1).ToString(),
                    }
                );
            }

            conexao.Desconectar();

            return tiposDePet;
        }

        /// <summary>
        ///     Faz a leitura do tipo de pet especificado por id cadastrado no banco de dados e retorna.
        /// </summary>
        /// <param name="id">Id para filtrar o tipo de pet.</param>
        /// <returns>Retorna o tipo de pet que tem o id especificado.</returns>
        public TipoDePet BuscarPorId(int id)
        {
            comando.Connection = conexao.Conectar();

            comando.CommandText = "SELECT * FROM TipoDePet " +
                                  "WHERE IdTipoDePet = @id";
            comando.Parameters.AddWithValue("@id", id);

            SqlDataReader dados = comando.ExecuteReader();

            comando.Parameters.Clear();

            TipoDePet tipoDePet = new TipoDePet();
            while (dados.Read())
            {
                tipoDePet.IdTipoDePet = Convert.ToInt32(dados.GetValue(0));
                tipoDePet.Descricao = dados.GetValue(1).ToString();
            }

            conexao.Desconectar();

            return tipoDePet;
        }

        /// <summary>
        ///     Faz a leitura do tipo de pet que tenha a descrição especificada no parâmetro cadastrado no banco de dados e retorna. Busca simples.
        /// </summary>
        /// <param name="descricao">Descrição para filtrar o tipo de pet.</param>
        /// <returns>Retorna o de pet que contém a descrição especificada.</returns>
        public TipoDePet BuscarPorDescricao(string descricao)
        {
            comando.Connection = conexao.Conectar();

            comando.CommandText = "SELECT * FROM TipoDePet " +
                                  "WHERE Descricao = @descricao";
            comando.Parameters.AddWithValue("@descricao", descricao);

            SqlDataReader dados = comando.ExecuteReader();

            TipoDePet tipoDePet = new TipoDePet();
            while (dados.Read())
            {
                tipoDePet.IdTipoDePet = Convert.ToInt32(dados.GetValue(0));
                tipoDePet.Descricao = dados.GetValue(1).ToString();
            }

            conexao.Desconectar();

            return tipoDePet;
        }

        /// <summary>
        ///     Altera um tipo de pet cadastrado no banco de dados.
        /// </summary>
        /// <param name="id">Id do tipod e pet a ser alterado.</param>
        /// <param name="tipoDePetAlterado">Tipo de pet alterado, que irá substituir o tipo de pet a ser alterado.</param>
        /// <returns>Retorna o tipo de pet, já alterado e cadastrado no banco.</returns>
        public TipoDePet Alterar(int id, TipoDePet tipoDePetAlterado)
        {
            comando.Connection = conexao.Conectar();

            comando.CommandText = "UPDATE TipoDePet SET " +
                                  "Descricao = @descricao " +
                                  "WHERE IdTipoDePet = @id";
            comando.Parameters.AddWithValue("@descricao", tipoDePetAlterado.Descricao);
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();

            return tipoDePetAlterado;
        }

        /// <summary>
        ///     Exclui o tipo de pet do banco de dados que contém o id especificado.
        /// </summary>
        /// <param name="id">Id do tipo de pet a ser deletado.</param>
        /// <returns>Retorna o tipo de pet deletado para uma melhor compreensão.</returns>
        public TipoDePet Excluir(int id)
        {
            //Instância do tipo de pet que irá ser apagado, para ser retornado e mostrar o tipo de pet que foi apagado.
            TipoDePet tipoDePet = new TipoDePet();
            tipoDePet.IdTipoDePet = BuscarPorId(id).IdTipoDePet;
            tipoDePet.Descricao = BuscarPorId(id).Descricao;

            comando.Connection = conexao.Conectar();

            comando.CommandText = "DELETE FROM TipoDePet " +
                                  "WHERE IdTipoDePet = @idTipoDePet";
            comando.Parameters.AddWithValue("@idTipoDePet", id);

            comando.ExecuteNonQuery();

            conexao.Desconectar();

            return tipoDePet;
        }
    }
}