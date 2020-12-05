using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_PetShop.Context
{
    public class PetShopContext
    {
        SqlConnection conexao = new SqlConnection();

        /// <summary>
        ///     Método construtor, que garante que quando uma nova instância de PetShopContext for gerada, a cadeia de conexão do banco de dados desejado seja atribuída à variável que irá manipular a conexão com o referido banco de dados.
        /// </summary>
        public PetShopContext()
        {
            conexao.ConnectionString = @"Data Source=desktop-h1nfq8t\SQLEXPRESS;Initial Catalog=PetShop;User ID=sa;Password=sa132";
        }

        /// <summary>
        ///     Abre a conexão com o banco de dados por meio da variável responsável por manipular a conexão (se esta estiver fechada). 
        /// </summary>
        /// <returns>A variável, já com a conexão aberta.</returns>
        public SqlConnection Conectar()
        {
            if(conexao.State == System.Data.ConnectionState.Closed)
                conexao.Open();

            return conexao;
        }

        /// <summary>
        ///     Fecha a conexão com o banco de dados por meio da variável responsável por manipular a conexão (se esta estiver aberta). 
        /// </summary>
        public void Desconectar()
        {
            if (conexao.State == System.Data.ConnectionState.Open)
                conexao.Close();
        }
    }
}