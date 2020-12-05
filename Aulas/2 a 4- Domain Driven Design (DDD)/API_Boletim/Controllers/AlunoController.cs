using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Boletim.Domains;
using API_Boletim.Repositories;
using Microsoft.AspNetCore.Mvc;

//Você pode pensar.. eu poderia burlar o sistema dando um GET, por exemplo, no POST, já que as rotas são indênticas. Sim, você pode, por isso que você tem que especificar o que você quer fazer colocando o método. Por exemplo, para dar um GET você usa a mesma rota do POST, mas se você quiser mesmo dar um GET, mude o verbo para o GET.

namespace API_Boletim.Controllers
{
    //Isto é uma rota. Ao digitar o seguinte link no navegador: api/Aluno, você será redirecionado para cá. Ele automaticamnete vem para o controlador de Aluno.
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        //Instanciamos o AlunoRepositorie, onde teremos acesso ao banco de dados, e estando aqui no controller poderemos exibir o resultado.
        AlunoRepositorie alunoRepository = new AlunoRepositorie();

        // GET: api/<AlunoController>
        //Exibe os resultados do banco.
        [HttpGet]
        public List<Aluno> Get()
        {
            return alunoRepository.Ler();
        }

        // GET api/<AlunoController>/5
        //Exibe o aluno especificado.
        [HttpGet("{id}")] //O id será passado via URL. Onde está o api/controller? Por que aqui só tem o id? O api/controller está lá em cima, assim não precisa ser digitado toda vez.
        public Aluno Get(int id) //Tem que estar escrito igual na rota, aqui, e em baixo.
        {
            return alunoRepository.BuscarPorId(id);
        }

        // POST api/<AlunoController>
        //Cadastra um aluno.
        [HttpPost]
        public Aluno Post([FromBody] Aluno aluno) //Como temos que passar um objeto como argumento para a requisição e ainda não temos um frontend, temos que fazer pelo Postman, não pelo navegador. Coloque o objeto no body do Postman, na parte "raw". Coloque também o tipo de arquivo como JSON.
        {
            return alunoRepository.Cadastrar(aluno);
        }

        // PUT api/<AlunoController>/5
        // Atualiza um registro especificado.
        [HttpPut("{id}")]
        public Aluno Put(int id, [FromBody] Aluno aluno)
        {
            return alunoRepository.Alterar(id, aluno);
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            alunoRepository.Excluir(id);
        }
    }
}
