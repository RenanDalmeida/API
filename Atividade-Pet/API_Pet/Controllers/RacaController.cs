using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_PetShop.Domains;
using API_PetShop.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_PetShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RacaController : ControllerBase
    {
        RacaRepository racaRepository = new RacaRepository();

        // GET: <RacaController>
        /// <summary>
        ///     Quando acessada a rota acima com o método GET, é exibido todas as raças cadastradas no banco de dados.
        /// </summary>
        /// <returns>Lista contendo todas as raças cadastradas no banco de dados.</returns>
        [HttpGet]
        public List<Raca> Get()
        {
            return racaRepository.Ler();
        }

        // GET <RacaController>/5
        /// <summary>
        ///     Quando acessada a rota acima com o método GET, é exibida a raça cadastrada no banco de dados que tenha o id especificado.
        /// </summary>
        /// <param name="id">Id da raça desejada.</param>
        /// <returns>Retorna a raça que tem o id especificado.</returns>
        [HttpGet("{id}")]
        public Raca Get(int id)
        {
            return racaRepository.BuscarPorId(id);
        }

        // POST <RacaController>
        /// <summary>
        ///     Quando acessada a rota acima com o método POST, é possível cadastrar uma raça no banco de dados a partir de um objeto em JSON inserido no body da requisição.
        /// </summary>
        /// <param name="raca">Raça inserida no body da requisição que será cadastrada no banco de dados.</param>
        /// <returns>Retorna a raça que foi cadastrada.</returns>
        [HttpPost]
        public Raca Post([FromBody] Raca raca)
        {
            return racaRepository.Cadastrar(raca);
        }

        // PUT <RacaController>/5
        /// <summary>
        ///     Quando acessada a rota acima com o método PUT, é possível alterar uma raça no banco de dados a partir de um objeto em JSON inserido no body da requisição e o Id da raça cadastrada no banco de dados a ser alterada.
        /// </summary>
        /// <param name="id">Id da raça a ser alterada.</param>
        /// <param name="racaAlterada">Raça já alterada, que irá substituir a raça a ser alterada.</param>
        /// <returns>Retorna a raça que já foi alterada e já está cadastrada no banco de dados.</returns>
        [HttpPut("{id}")]
        public Raca Put(int id, [FromBody] Raca racaAlterada)
        {
            return racaRepository.Alterar(id, racaAlterada);
        }

        // DELETE <RacaController>/5
        /// <summary>
        ///     Quando acessada a rota acima com o método DELETE, a raça que contém o id especificado será deletada do banco de dados.
        /// </summary>
        /// <param name="id">Id da raça a ser deletada.</param>
        /// <returns>Retorna a raça que foi deletada para uma melhor visualização e compreendimento do usuário.</returns>
        [HttpDelete("{id}")]
        public Raca Delete(int id)
        {
            return racaRepository.Excluir(id);
        }
    }
}