using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using API_Boletim.Context;
using API_Boletim.Domains;
using API_Boletim.Interfaces;

namespace API_Boletim.Repositories
{
    /*Aqui ficam todos os métodos que vão interagir com o banco.*/
    public class AlunoRepositorie : IAluno
    {
        //Chama-se o contexto para conectar o banco.
        BoletimContext conexao = new BoletimContext();

        //Chama-se o objeto que receberá e executará os comandos do banco (DDL etc).
        SqlCommand comando = new SqlCommand();

        //Lê todos os dados do banco de dados.
        public List<Aluno> Ler()
        {
            //Se conecta com o banco.
            comando.Connection = conexao.Conectar();

            //Prepara o código para ler todos os alunos.
            comando.CommandText = "SELECT * FROM Aluno";

            //Roda o comando e guarda em dados.
            SqlDataReader dados = comando.ExecuteReader();

            //Lista a ser retornada com todos os dados dos alunos.
            List<Aluno> alunos = new List<Aluno>();
            while (dados.Read()) //Enquanto tiver dados..
            {
                //Cria e adiciona objetos na lista de alunos, a ser retornada.
                alunos.Add(
                    new Aluno()
                    {
                        //O IdAluno (está no domínio) vai receber o idAluno do banco, que agora está na variável "dados". No GetValue, coloque a posição da coluna. Por exemplo, o idAluno está na coluna 0, por isso GetValue(0). Também, temos que converter para int, pois o resultado retornado é do tipo Object.
                        IdAluno = Convert.ToInt32(dados.GetValue(0)),
                        Nome = dados.GetValue(1).ToString(),
                        RA = dados.GetValue(2).ToString(),
                        Idade = Convert.ToInt32(dados.GetValue(3))
                    }
                );
            }

            //Fecha a conexão. Isso é sempre muito imporante.
            conexao.Desconectar();

            return alunos;
        }

        //Seleciona todos os alunos do banco de dados por um filtro.
        public Aluno BuscarPorId(int id)
        {
            comando.Connection = conexao.Conectar();

            //IMPORTANTE: Se você quiser colocar abaixo da seguinte forma: "SELECT * FROM Aluno WHERE IdAluno = " + id; vai funcionar.. só que colocar @id te protege contra ataques de SQL Injection.
            comando.CommandText = "SELECT * FROM Aluno WHERE IdAluno = @id";
            //Nós indicamos que o id mencionado no parâmetro do método BuscarPorId será o valor do @id no comando.
            comando.Parameters.AddWithValue("@id", id);

            SqlDataReader dados = comando.ExecuteReader();

            //Como só existe um aluno com o id, já instanciamos o objeto sem precisar o colocar no while.
            Aluno aluno = new Aluno();
            while(dados.Read())
            {
                aluno.IdAluno = Convert.ToInt32(dados.GetValue(0));
                aluno.Nome    = dados.GetValue(1).ToString();
                aluno.RA      = dados.GetValue(2).ToString();
                aluno.Idade   = Convert.ToInt32(dados.GetValue(3));
            }

            conexao.Desconectar();

            return aluno;
        }

        //Cadastra um aluno 
        public Aluno Cadastrar(Aluno aluno)
        {
            comando.Connection = conexao.Conectar();

            comando.CommandText = "INSERT INTO Aluno (Nome, RA, Idade)" +
                                  "VALUES" +
                                  "(@nome, @ra, @idade)";
            comando.Parameters.AddWithValue("@nome", aluno.Nome);
            comando.Parameters.AddWithValue("@ra", aluno.RA);
            comando.Parameters.AddWithValue("@idade", aluno.Idade);

            //Use o NonQuery para comandos DML
            comando.ExecuteNonQuery();

            conexao.Desconectar();

            return aluno;
        }

        //Altera um aluno.
        public Aluno Alterar(int id, Aluno aluno)
        {
            comando.Connection = conexao.Conectar();

            comando.CommandText = "UPDATE Aluno SET Nome = @nome, RA = @ra, Idade = @idade WHERE IdAluno = @id";
            comando.Parameters.AddWithValue("@nome", aluno.Nome);
            comando.Parameters.AddWithValue("@ra", aluno.RA);
            comando.Parameters.AddWithValue("@idade", aluno.Idade);
            comando.Parameters.AddWithValue("@id", id);

            comando.ExecuteNonQuery();

            return aluno;
        }

        public void Excluir(int id)
        {
            comando.Connection = conexao.Conectar();

            comando.CommandText = "DELETE FROM Aluno WHERE IdAluno = @id";
            comando.Parameters.AddWithValue("@id", id);

            //Use o NonQuery para comandos DML
            comando.ExecuteNonQuery();

            conexao.Desconectar();
        }
    }
}