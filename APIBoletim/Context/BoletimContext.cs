using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APIBoletim.Context
{
    public class BoletimContext
    {
        // 1 - Instancio o meu objeto de conexão
        SqlConnection con = new SqlConnection();

        public BoletimContext()
        {
            // 2 - Defino os dados de conexão com meu servidor SQL
            con.ConnectionString = @"Data Source=N-1S-DEV-16\SQLEXPRESS;Initial Catalog=boletim;User ID=sa;Password=sa132;";
        }

        public SqlConnection Conectar()
        {
            // 3 - Verifico se a conexão está fechada para conectar ao banco
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        public void Desconectar()
        {
            // 4 - Verifico se a conexão está aberta para fechar a conexão
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
