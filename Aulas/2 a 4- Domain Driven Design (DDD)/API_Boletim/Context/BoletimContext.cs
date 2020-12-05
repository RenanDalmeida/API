using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_Boletim.Context
{
    public class BoletimContext
    {
        //Instância da classe SqlConnection, que terá todos os recursos necessários para conectar a nossa api ao banco.
        SqlConnection conexao = new SqlConnection();

        //Método construtor, para quando um BoletimContext for instanciado, pegar o caminho do banco de dados.
        public BoletimContext()
        {
            //Para achar o caminho do banco, aqui no VS é muito fácil. Vá em Gerenciador de Servidores > botão direito no banco desejado > Propriedades > copie o campo "Cadeia de Conexão".
            conexao.ConnectionString = @"Data Source=desktop-h1nfq8t\SQLEXPRESS;Initial Catalog=boletim;User ID=sa;Password=sa132";
        }

        //Conecta ao banco de dados.
        public SqlConnection Conectar()
        {
            //Se não estiver conectado.. (isso é necessário pois só executará se não estiver conectado, poupando "esforço")
            if(conexao.State == System.Data.ConnectionState.Closed)
            {
                //Conecta.
                conexao.Open();
            }

            return conexao;
        }

        //Desconecta do banco de dados.
        public void Desconectar()
        {
            //Se estiver conectado.. (isso é necessário pois só executará se estiver conectado, poupando "esforço")
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                //Desconecta.
                conexao.Close();
            }
        }
    }
}